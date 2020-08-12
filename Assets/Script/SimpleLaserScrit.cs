using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SimpleLaserScrit : MonoBehaviour
{
    public SteamVR_Action_Boolean fireAction;
    public GameObject laserPrefab;
    public GameObject electroBallPrefab;
    public GameObject firePoint;
    public float shootingSpeed = 1;

    private GameObject spawnedLaser;

    // Start is called before the first frame update
    void Start()
    {
        spawnedLaser = Instantiate(laserPrefab, firePoint.transform) as GameObject;
        DisableLaser();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EnableLaser();

            //Rigidbody bulletrb = Instantiate(electroBallPrefab, firePoint.transform.position, firePoint.transform.rotation).GetComponent<Rigidbody>();
            //bulletrb.velocity = firePoint.transform.forward * shootingSpeed;
        }

        if (Input.GetMouseButton(0))
        {
            UpdateLaser();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DisableLaser();
        }
    }

    void EnableLaser()
    {
        spawnedLaser.SetActive(true);
    }

    void DisableLaser()
    {
        spawnedLaser.SetActive(false);
    }

    void UpdateLaser()
    {
        if (firePoint != null)
        {
            spawnedLaser.transform.position = firePoint.transform.position;
        }
    }
}
