using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ThermalModulator : MonoBehaviour
{
    
    private XRBaseInteractable xRBaseInteractable;

    public RestrictedThermalRange OjectThermalFloat; 
    // Start is called before the first frame update
    void Start()
    {
        xRBaseInteractable = GetComponent<XRBaseInteractable>();
        xRBaseInteractable.hoverEntered.AddListener(OnHoverEnter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnHoverEnter(HoverEnterEventArgs args){


        var interactor = args.interactorObject;

        if (interactor is NearFarInteractor nearFar){
        
         Debug.Log("Interacting with RadiateThermalEnergy with nearFar handedness: " + nearFar.handedness);

        }

    }



}
