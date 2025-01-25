using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public struct RestrictedThermalRange
{   
    [SerializeField]
    private float _thermalValue;

    public float Value
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

    public RestrictedThermalRange(float value)
    {
        if (value < -1 || value > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Value must be between -1 and 1.");
        }
        _thermalValue = value;
    }

    public override string ToString() => _thermalValue.ToString();
}