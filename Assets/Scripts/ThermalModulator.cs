using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThermalModulator : MonoBehaviour
{
    [System.Serializable]
    public struct RestrictedThermalRange
    {   
        [SerializeField]
        private double _thermalValue;

        public double Value
        {
            get => _thermalValue;
            set
            {
                if (value < -1 || value > 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be between -1 and 1.");
                }
                _thermalValue = value;
            }
        }

        public RestrictedThermalRange(double value)
        {
            if (value < -1 || value > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be between -1 and 1.");
            }
            _thermalValue = value;
        }

        public override string ToString() => _thermalValue.ToString();
    }

    
    
    public RestrictedThermalRange OjectThermalValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
