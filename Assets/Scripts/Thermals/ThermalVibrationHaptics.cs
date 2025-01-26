using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

namespace Thermals
{
    public class ThermalVibrationHaptics : MonoBehaviour
    {
        [SerializeField] private ThermalBody thermalBody;
        [SerializeField] private HapticImpulsePlayer hapticPlayer;
        [SerializeField] private float cubicAmplitude;
        [SerializeField] private float maxTemperature;
        [SerializeField] private float hapticAmplitude;
        

        private void Update()
        {
            UpdateHaptics(Time.deltaTime);
        }

        private void UpdateHaptics(float deltaTime)
        {
            if (!(thermalBody && hapticPlayer))
                return;

            var temp = thermalBody.Temperature;
            var period = Mathf.Max(0, cubicAmplitude * Mathf.Pow(-temp + maxTemperature, 3));
            var shouldTick = period == 0 || Time.time % period + deltaTime > period;
            if (shouldTick)
            {
                hapticPlayer.SendHapticImpulse(hapticAmplitude, deltaTime);
            }
        }
    }
}