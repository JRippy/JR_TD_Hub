using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Gun : MonoBehaviour
{
    public SteamVR_Action_Boolean fireAction;
    public GameObject bullet;
    public Transform barrelPivot;
    public float shootingSpeed = 1;
    public GameObject muzzleFlash;
    public ParticleSystem smokeGun;
    public AudioClip shoot;
    public GameObject impactEffect;

    private Animator animator;
    private Interactable interactable;
    private Transform pointSpawnSmoke;
    private ParticleSystem instantiatedObj;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        muzzleFlash.SetActive(false);
        interactable = GetComponent<Interactable>();
        smokeGun.Stop();
        //pointSpawnSmoke = smokeGun.transform;
        //pointSpawnSmoke.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            if (fireAction[source].stateDown)
            {
                //pointSpawnSmoke.position = smokeGun.transform.position;
                Fire();
            }
            else
            {
                muzzleFlash.SetActive(false);
            }
        }
    }

    void Fire()
    {
        Debug.Log("Fire");
        Rigidbody bulletrb = Instantiate(bullet, barrelPivot.position, barrelPivot.rotation).GetComponent<Rigidbody>();
        bulletrb.velocity = barrelPivot.forward * shootingSpeed;
        muzzleFlash.SetActive(true);
        Invoke("StopFire", 0.5f);

        //instantiatedObj = Instantiate(smokeGun, pointSpawnSmoke);
        //Destroy(instantiatedObj, 1);

        AudioSource.PlayClipAtPoint(shoot, barrelPivot.position);

        RaycastHit hit;

        if (Physics.Raycast(barrelPivot.transform.position, barrelPivot.transform.forward, out hit, Mathf.Infinity))
        {
            //Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));  
        }
    }

    void StopFire()
    {
        muzzleFlash.SetActive(false);
    }
}