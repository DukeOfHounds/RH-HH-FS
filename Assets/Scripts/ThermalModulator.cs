using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.Serialization;

public class ThermalModulator : MonoBehaviour
{
    
    [FormerlySerializedAs("gloveClient")] [FormerlySerializedAs("networkingManager")] public GloveNetworkClient gloveNetworkClient;

    private XRBaseInteractable xRBaseInteractable;

    public RestrictedThermalRange OjectThermalFloat; 

    private bool isWarmingRightHand = false;
    private bool isWarmingLeftHand = false;
    private bool isSelecting = false;



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
        // intHadeness 0 is right and 1 is left
        var intHandedness = (handedness == InteractorHandedness.Right) ? 1 : 0;

        if ((intHandedness == 0 && isWarmingRightHand) || (intHandedness ==1 && isWarmingLeftHand))
            return;

        Debug.Log("making hand +" + handedness + "temp of" + OjectThermalFloat.Value);


        if (OjectThermalFloat.Value > 0)
            gloveNetworkClient.MakeHot(intHandedness);
        else
            gloveNetworkClient.MakeCold(intHandedness);
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        if (isSelecting) return;

        var handedness = args.interactorObject.handedness;
        // intHadeness 0 is right and 1 is left
        var intHandedness = (handedness == InteractorHandedness.Right) ? 1 : 0;

        gloveNetworkClient.MakeOff(intHandedness);


    }

    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        
        var handedness = args.interactorObject.handedness;
        // intHadeness 0 is right and 1 is left
        var intHandedness = (handedness == InteractorHandedness.Right) ? 1 : 0;

        isSelecting = true;

        if (OjectThermalFloat.Value > 0){
            gloveNetworkClient.MakeHot(intHandedness);
            Debug.Log("select hot");
        }
        else
            gloveNetworkClient.MakeCold(intHandedness);
    }
    public void OnSelectExit(SelectExitEventArgs args)
    {
        var handedness = args.interactorObject.handedness;
        var intHandedness = (handedness == InteractorHandedness.Right) ? 1 : 0;

        isSelecting = false;

        if (intHandedness == 0)
            isWarmingRightHand = false;
        else
            isWarmingLeftHand =false;

        gloveNetworkClient.MakeOff(intHandedness);
    }
}
