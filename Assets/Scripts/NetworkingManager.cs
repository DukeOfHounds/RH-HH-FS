using System;
using System.Net.Sockets;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
    public class NetworkingManager : MonoBehaviour
    {
        [SerializeField] private string serverIp;
        [SerializeField] private int serverPort;
        
        private UdpClient _udpClient;
        
        private void Start()
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

        public void MakeHot(int handeness)
        {
            SendMsg($"HOT {handeness}\n");
        }

        public void MakeCold(int handeness)
        {
            SendMsg($"COLD {handeness}\n");
        }

        public void MakeOff(int handeness)
        {
            SendMsg($"OFF {handeness}\n");
        }
    }
}