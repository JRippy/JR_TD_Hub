/* SceneHandler.cs*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class SceneHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    public Material newMaterialRef;
    public Transform sp1;
    public Transform sp2;
    public Transform sp3;
    public Transform sp4;

    Text txt;

    //
    //Distance Grab
    public Transform snapTo;
    private Rigidbody body;
    private bool wasKinetic;
    private bool wasGravity;
    private Transform objToGrab;
    public float snapTime = 2;

    private float dropTimer;
    private Interactable interactable;
    private Transform transfoSnapTo;
    //

    //------Test--------
    // a reference to the action
    public SteamVR_Action_Boolean SphereOnOff;
    // a reference to the hand
    public SteamVR_Input_Sources handType;
    //reference to the sphere
    public GameObject Sphere;
    //--------------------

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    //-----------------Test----------------------
    void Start()
    {
        //SphereOnOff.AddOnStateDownListener(TriggerDown, handType);
        //SphereOnOff.AddOnStateUpListener(TriggerUp, handType);
    }

    //To review :: Add input
    //
    //public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    //{
    //    Debug.Log("Trigger is up");
    //    Sphere.GetComponent<MeshRenderer>().enabled = false;
    //}
    //public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    //{
    //    Debug.Log("Trigger is down");
    //    Sphere.GetComponent<MeshRenderer>().enabled = true;
    //}




    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Cube")
        {
            Debug.Log("Cube was clicked");
            //Renderer rend = e.target.GetComponent<Renderer>();
            e.target.GetComponent<Renderer>().material = newMaterialRef;
        }
        else if (e.target.name == "Button")
        {
            Debug.Log("Button was clicked");
            //var rot = e.target.rotation;
            //rot.x += Time.deltaTime * 10;
            //e.target.rotation = rot;

            e.target.GetComponentInChildren<Text>().text = "Clicked";
            e.target.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
        }
        else if (e.target.name == "Flower" || e.target.name == "Flower(1)" || e.target.name == "Flower(2)" || e.target.name == "Flower(3)")
        {
            Debug.Log("Flower was clicked");
            sp1.position = transform.position;
            sp2.position = transform.position;
            sp3.position = transform.position;
            sp4.position = transform.position;

        }
        else if (e.target.name != null && e.target.GetComponent<Interactable>() != null)
        {
            Debug.Log("Click actif");
            objToGrab = e.target.GetComponent<Transform>();
            interactable = e.target.GetComponent<Interactable>();
            body = e.target.GetComponent<Rigidbody>();

            Debug.Log(body.isKinematic + " " + body.useGravity);
            wasKinetic = body.isKinematic;
            wasGravity = body.useGravity;
            transfoSnapTo = snapTo;

            Debug.Log(snapTo.position);
            //transfoSnapTo.position = snapTo.position;
            //transfoSnapTo.rotation = snapTo.rotation;
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {


        //if (e.target.name == "Cube")
        //{
        //    Debug.Log("Cube was entered");
        //}
        //else if (e.target.name == "Button")
        //{
        //    Debug.Log("Button was entered");
        //}

    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        //if (e.target.name == "Cube")
        //{
        //    Debug.Log("Cube was exited");
        //}
        //else if (e.target.name == "Button")
        //{
        //    Debug.Log("Button was exited");
        //}
    }

    private void FixedUpdate()
    {
        bool used = false;
        if (interactable != null)
        {
            used = interactable.attachedToHand;
        }
            

        if (body != null)
        {
            if (used)
            {
                //body.isKinematic = false;
                //Debug.Log("Is used!");
                body.useGravity = wasGravity;
                body.isKinematic = wasKinetic;
                dropTimer = -1;
            }
            //else if (interactable.hoveringHand != null)
            //{
            //    dropTimer = -1;

            //    StartCoroutine(StopObject());
            //}
            else
            {
                dropTimer += Time.deltaTime / (snapTime / 2);

                body.isKinematic = dropTimer > 1;

                if (dropTimer > 1)
                {
                    //transform.parent = snapTo;
                    objToGrab.position = transfoSnapTo.position;
                    objToGrab.rotation = transfoSnapTo.rotation;
                    dropTimer = -1;

                    //Debug.Log("Dropt Timer > 1");
                    StartCoroutine(SuspendObject());
                }
                else
                {
                    float t = Mathf.Pow(35, dropTimer);
                    // t = 10000.04f;

                    body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, Time.fixedDeltaTime * 4);
                    if (body.useGravity)
                        body.AddForce(-Physics.gravity);

                    objToGrab.position = Vector3.Lerp(objToGrab.position, transfoSnapTo.position, Time.fixedDeltaTime * t * 3);
                    objToGrab.rotation = Quaternion.Slerp(objToGrab.rotation, transfoSnapTo.rotation, Time.fixedDeltaTime * t * 2);
                }

            }
        }
    }

    IEnumerator SuspendObject()
    {
        Debug.Log("Gravity desactive");
        body.useGravity = false;
        body.isKinematic = false;

        yield return new WaitForSeconds(1);

        if (body != null)
        {
            Debug.Log("Gravity active");
            Debug.Log(wasGravity);
            Debug.Log(wasKinetic);
            body.useGravity = wasGravity;
            body.isKinematic = wasKinetic;
            objToGrab = null;
            interactable = null;
            body = null;
        }


    }

    IEnumerator StopObject()
    {
        body.useGravity = false;
        body.isKinematic = false;

        yield return new WaitForSeconds(1);

        if (body != null)
        {

            body.useGravity = wasGravity;
            body.isKinematic = wasKinetic;

        }

        objToGrab = null;
        interactable = null;
        body = null;

    }
}