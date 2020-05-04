using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class slingshot : MonoBehaviour
{
    public Interactable frond;
    public GameObject bullet;
    public Transform barrelPivot;
    public Transform frondPivot;
    public float shootingSpeed = 1;
    public AudioClip elastic;
    //public Collider collider;

    private Animator animator;
    private Interactable interactable;
    private Transform pointSpawnSmoke;
    private ParticleSystem instantiatedObj;
    private GameObject rubberStreched;
    private bool elasticStrech = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        interactable = GetComponent<Interactable>();
        rubberStreched = GameObject.FindWithTag("FireBall");
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    if (frond.attachedToHand == null)
    //    {
    //        //SteamVR_Input_Sources source = interactable.attachedToHand.handType;

    //        if (rubberStreched.GetComponent<shotBall>().launch)
    //        {
    //            Fire();
    //        rubberStreched.GetComponent<shotBall>().launch = false;
    //        }

    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {

        if (frond.attachedToHand == null && elasticStrech == true)
        {
            if (other.name == "frond")
            {
                Fire();
            }

        }

        elasticStrech = false;

    }

    private void OnTriggerExit(Collider other)
    {
        if (frond.attachedToHand != null)
        {
            elasticStrech = true;
        }
    }

    void Fire()
    {
        Rigidbody bulletrb = Instantiate(bullet, barrelPivot.position, barrelPivot.rotation).GetComponent<Rigidbody>();
        //bulletrb.velocity = barrelPivot.forward * shootingSpeed;
        bulletrb.velocity = (barrelPivot.position - frondPivot.position) * shootingSpeed;

        //instantiatedObj = Instantiate(smokeGun, pointSpawnSmoke);
        //Destroy(instantiatedObj, 1);

        RaycastHit hit;

        if (Physics.Raycast(barrelPivot.transform.position, barrelPivot.transform.forward, out hit, Mathf.Infinity))
        {
            //Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));  
        }
    }
}