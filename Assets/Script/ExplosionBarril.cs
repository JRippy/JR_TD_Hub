using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBarril : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;

    public GameObject explosionEffect;
    float countDown;
    bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        countDown = delay;    
    }

    // Update is called once per frame
    void Update()
    {
        //countDown -= Time.deltaTime;
        //if (countDown <= 0f && !hasExploded)
        //{
        //    Exploded();
        //    hasExploded = true;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet" && !hasExploded)
        //if (collision.gameObject.tag == "bullet" && countDown <= 0f && !hasExploded)
        {
            Exploded();
            hasExploded = true;
        }
    }

    void Exploded()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearObject in colliders)
        {
            Rigidbody rb = nearObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        Destroy(gameObject);
    }
}
