using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace Thermals
{
    /// <summary>
    /// Calculates temperature for a body which is influenced by thermal sources.
    /// </summary>
    public class ThermalBody : MonoBehaviour
    {
        [SerializeField] private float temperature;
        [SerializeField] private float decay = 1f;
        [SerializeField] private List<BaseThermalSource> thermalSources;

        public float Temperature => temperature;

        private void Awake()
        {
            thermalSources.AddRange(FindObjectsOfType<BaseThermalSource>());
        }

        private void FixedUpdate()
        {
            UpdateTemperature(Time.fixedDeltaTime);
        }

        public void AddThermalSource(BaseThermalSource thermalSource)
        {
            thermalSources.Add(thermalSource);
        }

        public void RemoveThermalSource(BaseThermalSource thermalSource)
        {
            thermalSources.Remove(thermalSource);
        }

        public void RemoveAllThermalSources()
        {
            thermalSources.Clear();
        }

        private void UpdateTemperature(float deltaTime)
        {
            var position = transform.position;
            var sourcePower = thermalSources.Where(thermalSource =>  thermalSource).Sum(thermalSource =>
                thermalSource.Process(position));
            var temperatureDelta = sourcePower * deltaTime;
            temperature += temperatureDelta;

            // decay temperature
            temperature *= 1f - Mathf.Min(decay * deltaTime, 1f);
        }
    }
}