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

    private bool isWarmingRightHand = false;
    private bool isWarmingLeftHand = false;



    // Start is called before the first frame update
    void Start()
    {
        xRBaseInteractable = GetComponent<XRBaseInteractable>();
        xRBaseInteractable.hoverEntered.AddListener(OnHoverEnter);
        xRBaseInteractable.hoverExited.AddListener(OnHoverExit);

        xRBaseInteractable.selectEntered.AddListener(OnSelectEnter);
        xRBaseInteractable.selectExited.AddListener(OnSelectExit);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoverEnter(HoverEnterEventArgs args)
    {

        var handedness = args.interactorObject.handedness;
        // intHadeness 1 is right and 1 is left
        var intHandedness = (handedness == InteractorHandedness.Right) ? 1 : 0;

        if ((intHandedness == 1 && isWarmingRightHand) || (intHandedness == 0 && isWarmingLeftHand))
            return;

        Debug.Log("making hand +" + handedness + "temp of" + OjectThermalFloat.Value);


        if (OjectThermalFloat.Value > 0)
            networkingManager.MakeHot(intHandedness);
        else
            networkingManager.MakeCold(intHandedness);
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        var handedness = args.interactorObject.handedness;
        // intHadeness 1 is right and 1 is left
        var intHandedness = (handedness == InteractorHandedness.Right) ? 1 : 0;

        networkingManager.MakeOff(intHandedness);


    }

    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        
        var handedness = args.interactorObject.handedness;
        // intHadeness 1 is right and 1 is left
        var intHandedness = (handedness == InteractorHandedness.Right) ? 1 : 0;

        if (intHandedness == 1)
            isWarmingRightHand = true;
        else
            isWarmingLeftHand =true;

        if (OjectThermalFloat.Value > 0){
            networkingManager.MakeHot(intHandedness);
            Debug.Log("select hot");
        }
        else
            networkingManager.MakeCold(intHandedness);
    }
    public void OnSelectExit(SelectExitEventArgs args)
    {
        var handedness = args.interactorObject.handedness;
        var intHandedness = (handedness == InteractorHandedness.Right) ? 1 : 0;

        if (intHandedness == 1)
            isWarmingRightHand = false;
        else
            isWarmingLeftHand =false;

        networkingManager.MakeOff(intHandedness);
    }
}
