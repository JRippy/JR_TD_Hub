using UnityEngine;
using System.Collections;
using Valve.VR;

public class WandController : MonoBehaviour
{
    public float MaxLength;
    public float PerIncrement;
    public EVRButtonId WandButton = EVRButtonId.k_EButton_SteamVR_Trigger;

    // a reference to the action
    public SteamVR_Action_Boolean clickAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    // a reference to the hand
    public SteamVR_Input_Sources handType;

    SteamVR_TrackedObject trackedObj;
    //CapsuleCollider stickCollider;
    float minLength;
    bool isExpanding = false;

    void Start()
    {
        trackedObj = GetComponentInParent<SteamVR_TrackedObject>();
        //stickCollider = GetComponent<CapsuleCollider>();
        minLength = transform.localScale.y;

        clickAction.AddOnStateDownListener(TriggerDown, handType);
        clickAction.AddOnStateUpListener(TriggerUp, handType);

    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is up");
        isExpanding = true;
    }
    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down");
        isExpanding = false;
    }

    void FixedUpdate()
    {
        float newLength;
        float currLength = transform.localScale.y;
        if (isExpanding)
        {
            // make wand longer
            newLength = Mathf.Min(currLength + PerIncrement * Time.deltaTime, MaxLength);
        }
        else
        {
            // shrink to min
            newLength = Mathf.Max(currLength - PerIncrement * Time.deltaTime, minLength);
        }

        // update scale, position, and collider size
        if (newLength != currLength)
        {
            var newScale = transform.localScale;
            newScale.y = newLength;
            transform.localScale = newScale;
            transform.localPosition = new Vector3(0, 0, transform.localScale.y);
            //stickCollider.height = newLength;
        }
    }
}
