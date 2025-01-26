using System;
using UnityEngine;

namespace Thermals
{
    public class ThermalDisplay : MonoBehaviour
    {
        [SerializeField] private ThermalBody[] thermalBodies;
        [SerializeField] private float normalTemperatureRange; 

        private void OnGUI()
        {
            var style = new GUIStyle(GUI.skin.label)
            {
                richText = true
            };
            GUILayout.BeginVertical("Box");
            foreach (var thermalBody in thermalBodies)
            {
                var temp = thermalBody.Temperature;

                var textColor = Color.white;
                if (Mathf.Abs(temp) >= normalTemperatureRange)
                {
                    textColor = temp > 0 ? Color.red : Color.blue;
                }
                
                var htmlColor = ColorUtility.ToHtmlStringRGB(textColor);
                GUILayout.Label($"{thermalBody.gameObject.name}: <b><color=#{htmlColor}>{thermalBody.Temperature:0.##}</color></b>", style);
            }
            GUILayout.EndVertical();
        }
    }
}