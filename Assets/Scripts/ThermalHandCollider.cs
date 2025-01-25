using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThermalHandCollider : MonoBehaviour
{
    
    public RestrictedThermalRange CurrentOutpurThermalFloat;

    // Start is called before the first frame update

    void OnCollisionEnter(Collision collision){
        ThermalModulator thermalModulator = collision.gameObject.GetComponent<ThermalModulator>();
        Debug.Log("Collision with: " + collision.gameObject.name + " with a temperature of: " + thermalModulator.OjectThermalFloat);
    }
    
    
}
