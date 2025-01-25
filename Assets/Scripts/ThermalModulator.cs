using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using DefaultNamespace;

public class ThermalModulator : MonoBehaviour
{
    
    public NetworkingManager networkingManager;

    private XRBaseInteractable xRBaseInteractable;

    public RestrictedThermalRange OjectThermalFloat; 
    // Start is called before the first frame update
    void Start()
    {
        xRBaseInteractable = GetComponent<XRBaseInteractable>();
        xRBaseInteractable.hoverEntered.AddListener(OnHoverEnter);
        xRBaseInteractable.hoverExited.AddListener(OnHoverExit);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // int hadeness 0 is right and 1 is left
    public void OnHoverEnter(HoverEnterEventArgs args)
    {


        var handedness = args.interactorObject.handedness;
        var intHandedness = (handedness == InteractorHandedness.Right) ? 1 : 0;

        if (OjectThermalFloat.Value > 0)
            networkingManager.MakeHot(intHandedness);
        else
            networkingManager.MakeCold(intHandedness);
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        var handedness = args.interactorObject.handedness;
        var intHandedness = (handedness == InteractorHandedness.Right) ? 1 : 0;

        networkingManager.MakeOff(intHandedness);


    }
}
