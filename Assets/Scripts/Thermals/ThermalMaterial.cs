using System;
using UnityEngine;

namespace Thermals
{
    public class ThermalMaterial : MonoBehaviour
    {
        [SerializeField] private ThermalBody thermalBody;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Gradient temperatureGradient;
        [SerializeField] private float gradientRange;

        private void Update()
        {
            UpdateMaterial();
        }

        private void UpdateMaterial()
        {
            if (!(thermalBody && meshRenderer))
                return;
            
            var t = Mathf.InverseLerp(-gradientRange, gradientRange, thermalBody.Temperature);
            t = Mathf.Clamp(t, 0f, 1f);

            var color = temperatureGradient.Evaluate(t);
            meshRenderer.material.color = color;
        }
    }
}