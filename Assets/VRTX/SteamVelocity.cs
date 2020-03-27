using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Tracking.Velocity;
using Valve.VR.InteractionSystem;

public class SteamVelocity : VelocityTracker
{
    [Tooltip("The Hand GameObject used by SteamVR")]
    public Hand trackedGameObject;

    protected override Vector3 DoGetAngularVelocity()
    {
        return trackedGameObject.GetTrackedObjectAngularVelocity();
    }

    protected override Vector3 DoGetVelocity()
    {
        return trackedGameObject.GetTrackedObjectVelocity();
    }
}
