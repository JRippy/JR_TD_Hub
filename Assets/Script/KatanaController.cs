using UnityEngine;
using System.Collections;
using Valve.VR;

public class KatanaController : MonoBehaviour
{
    // a reference to the action
    public SteamVR_Action_Boolean clickAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    // a reference to the hand
    public SteamVR_Input_Sources handType;

    public EVRButtonId sliceModeButton;
    public float CutPlaneSize = 1f;

    SteamVR_TrackedObject trackedObj;
    Collider bladeCollider;
    Vector3 collisionEnterPos, collisionExitPos;

    void Start()
    {
        trackedObj = GetComponentInParent<SteamVR_TrackedObject>();
        bladeCollider = GetComponentInChildren<Collider>();


        clickAction.AddOnStateDownListener(TriggerDown, handType);
        clickAction.AddOnStateUpListener(TriggerUp, handType);
        bladeCollider.isTrigger = true;
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is up");
        bladeCollider.isTrigger = true;
    }
    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down");
        bladeCollider.isTrigger = false;
    }

    void Update()
    {
        Debug.DrawLine(bladeCollider.transform.position, bladeCollider.transform.position + bladeCollider.transform.up * 1f, Color.red); // towards tip
        Debug.DrawLine(bladeCollider.transform.position, bladeCollider.transform.position + bladeCollider.transform.forward * 1f, Color.blue); // down
    }

    void OnTriggerEnter(Collider other)
    {
        collisionEnterPos = transform.position;
    }

    void OnTriggerExit(Collider other)
    {
        collisionExitPos = transform.position;
        CreateCutPlane(collisionEnterPos, collisionExitPos, bladeCollider.transform.up);
    }

    private void CreateCutPlane(Vector3 startPos, Vector3 endPos, Vector3 forward)
    {
        Vector3 center = Vector3.Lerp(startPos, endPos, .5f);
        Vector3 cut = (endPos - startPos).normalized;
        Vector3 fwd = forward.normalized;
        Vector3 normal = Vector3.Cross(fwd, cut).normalized;

        GameObject goCutPlane = new GameObject("CutPlane", typeof(BoxCollider), typeof(Rigidbody), typeof(SplitterSingleCut));

        goCutPlane.GetComponent<Collider>().isTrigger = true;
        Rigidbody bodyCutPlane = goCutPlane.GetComponent<Rigidbody>();
        bodyCutPlane.useGravity = false;
        bodyCutPlane.isKinematic = true;

        Transform transformCutPlane = goCutPlane.transform;
        transformCutPlane.position = center;
        transformCutPlane.localScale = new Vector3(CutPlaneSize, .01f, CutPlaneSize);
        transformCutPlane.up = normal;
        float angleFwd = Vector3.Angle(transformCutPlane.forward, fwd);
        transformCutPlane.RotateAround(center, normal, normal.y < 0f ? -angleFwd : angleFwd);
    }
}
