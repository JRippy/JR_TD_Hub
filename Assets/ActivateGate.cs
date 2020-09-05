using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGate : MonoBehaviour
{
    public GameObject Wall;
    public float FireRate = 1.0f;

    private float NextFire;
    private bool activationGate = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") &&  Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;

            if (!Wall.activeSelf)
            {
                Wall.SetActive(true);
            }
            else
            {
                Wall.SetActive(false);
            }
        }

        activationG();
    }

    void activationG()
    {
        if (activationGate && Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;
            activationGate = false;

            if (!Wall.activeSelf)
            {
                Wall.SetActive(true);
            }
            else
            {
                Wall.SetActive(false);
            }
        }
    }

    public void SetActivTrue()
    {
        activationGate = true;
    }
}
