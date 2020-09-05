using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroGun : MonoBehaviour
{
    public Camera cam;
    public GameObject firePoint;
    public LineRenderer lr;
    public float maximuLenght;
    

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, firePoint.transform.position);

        RaycastHit hit;
        var mousePos = Input.mousePosition;
        var rayMouse = cam.ScreenPointToRay(mousePos);

        if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maximuLenght)){
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else{
            var pos = rayMouse.GetPoint(maximuLenght);
            lr.SetPosition(1, pos);
        }
    }
}
