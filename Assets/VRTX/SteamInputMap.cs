using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Zinnia.Action;

public class SteamInputMap : BooleanAction
{
    [Tooltip("Button to start climbing (GrabGrip)")]
    public string SteamVRAction;
    public SteamVR_Action_Boolean GrabGrip = SteamVR_Input.GetBooleanAction("GrabGrip");
    public Hand hand;

    private SteamVR_Action_Boolean button;

    // Start is called before the first frame update
    void Start()
    {
        //button = SteamVR_Input.GetAction<SteamVR_Action_Boolean>(SteamVRAction);
        button = GrabGrip;
    }

    protected virtual void Update()
    {
        Receive(button.GetState(hand.handType));
    }
}
