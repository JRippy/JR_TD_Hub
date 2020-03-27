using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public bool focusObjectif;
    public GameObject objectif;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (focusObjectif)
        {
            if (objectif != null)
            {
                transform.LookAt(objectif.transform);
            }

        }
        
    }
}
