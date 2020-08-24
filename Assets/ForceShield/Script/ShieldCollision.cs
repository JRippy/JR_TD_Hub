using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollision : MonoBehaviour
{

    [SerializeField] string[] _collisionTag;
    public GameManagerDefender gmd;
    float hitTime;
    Material mat;

    //Retract Shield
    public float scaleRate = -0.1f;
    public float minScale = 0.01f;
    public float maxScale = 10.0f;

    void Start()
    {
        if (GetComponent<Renderer>())
        {
            mat = GetComponent<Renderer>().sharedMaterial;
        }

    }

    void Update()
    {

        if (hitTime > 0)
        {
            float myTime = Time.fixedDeltaTime * 1000;
            hitTime -= myTime;
            if (hitTime < 0)
            {
                hitTime = 0;
            }
            mat.SetFloat("_HitTime", hitTime);
        }

        if (gmd.healthCurrentShield <= 0)
        {
            ApplyScaleRate();
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < _collisionTag.Length; i++)
        {

            if (_collisionTag.Length > 0 || collision.transform.CompareTag(_collisionTag[i]))
            {
                //Debug.Log("hit");
                ContactPoint[] _contacts = collision.contacts;
                for (int i2 = 0; i2 < _contacts.Length; i2++)
                {
                    mat.SetVector("_HitPosition", transform.InverseTransformPoint(_contacts[i2].point));
                    hitTime = 500;
                    mat.SetFloat("_HitTime", hitTime);

                    Debug.Log("Shield Hit!");
                }

                Debug.Log("Shield Hit!");
                gmd.healthCurrentShield -= 5;
            }
        }
    }

    public void ApplyScaleRate()
    {
        //if we exceed the defined range then correct the sign of scaleRate.
        if (transform.localScale.x < minScale)
        {
            //scaleRate = Mathf.Abs(scaleRate);
            Destroy(gameObject);
        }
        //else if (transform.localScale.x > maxScale)
        //{
        //    scaleRate = -Mathf.Abs(scaleRate);
        //}
        transform.localScale += Vector3.one * scaleRate;
    }
}

