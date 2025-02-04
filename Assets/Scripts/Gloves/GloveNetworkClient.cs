using System;
using System.Net.Sockets;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GloveNetworkClient : MonoBehaviour
{
    [SerializeField] private string serverIp;
    [SerializeField] private int serverPort;

    private UdpClient _udpClient;

    private void Awake()
    {
        _udpClient = new UdpClient();
        _udpClient.Connect(serverIp, serverPort);

        StartHeartbeat();
    }

    private async void StartHeartbeat()
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));

            // We have G
            SendMsg("G");
        }
    }

    private void SendMsg(string msg)
    {
        var encoded = Encoding.UTF8.GetBytes(msg);
        _udpClient.SendAsync(encoded, encoded.Length);
    }

    public void SendTemperature(TemperatureState temperatureState)
    {
        SendMsg(temperatureState switch
        {
            TemperatureState.Off => "O",
            TemperatureState.Cold => "C",
            TemperatureState.Hot => "H",
            _ => throw new ArgumentOutOfRangeException(nameof(temperatureState), temperatureState, null)
        });
    }

    // int hadeness 0 is right and 1 is left
    public void MakeHot(int handeness)
    {
        SendMsg("H");
    }

    public void MakeCold(int handeness)
    {
        SendMsg("C");
    }

    public void MakeOff(int handeness)
    {
        SendMsg("O");
    }

    private void OnDisable()
    {
        SendMsg("O");
    }

    public enum TemperatureState
    {
        Off,
        Cold,
        Hot
    }
}