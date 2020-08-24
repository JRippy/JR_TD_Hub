using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    public GameManagerDefender gmd;
    public GameObject spark1;
    public GameObject spark2;
    public GameObject spark3;

    // Update is called once per frame
    void Update()
    {
        if (gmd.healthCurrentGenerator < 100)
        {
            spark1.SetActive(true);
        }

        if (gmd.healthCurrentGenerator < 65)
        {
            spark2.SetActive(true);
        }

        if (gmd.healthCurrentGenerator < 25)
        {
            spark3.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.CompareTag("ZombieHand") || collision.transform.CompareTag("Zombie"))
        {
            Debug.Log("Generator Hit!");
            gmd.healthCurrentGenerator -= 5;
        }
        
    }
}
