using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLine : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject endPoint;
    public LineRenderer lr;

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, firePoint.transform.position);
        lr.SetPosition(1, endPoint.transform.position);
    }
}
