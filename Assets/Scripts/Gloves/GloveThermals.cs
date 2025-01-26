using System;
using Thermals;
using UnityEngine;
using UnityEngine.Serialization;

public class GloveThermals : MonoBehaviour
{
    [SerializeField] private GloveNetworkClient client;
    [SerializeField] private ThermalBody thermalBody;
    [SerializeField] private float normalTemperatureRange = 0f;

    [SerializeField]
    private GloveNetworkClient.TemperatureState temperatureState = GloveNetworkClient.TemperatureState.Off;

    public GloveNetworkClient.TemperatureState TemperatureState => temperatureState;

    private void Update()
    {
        UpdateTemperature();
    }

    private void UpdateTemperature()
    {
        if (!thermalBody)
            return;

        var temp = thermalBody.Temperature;
        if (Mathf.Abs(temp) < normalTemperatureRange)
        {
            SetGloveTemperatureState(GloveNetworkClient.TemperatureState.Off);
        }
        else
        {
            SetGloveTemperatureState(temp > 0
                ? GloveNetworkClient.TemperatureState.Hot
                : GloveNetworkClient.TemperatureState.Cold);
        }
    }

    private void SetGloveTemperatureState(GloveNetworkClient.TemperatureState newState)
    {
        if (newState == temperatureState)
            return;

        temperatureState = newState;

        if (client)
        {
            client.SendTemperature(temperatureState);
        }
    }
}