using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotBall : MonoBehaviour
{
    public bool launch = false;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Frond")
        {
            launch = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        launch = false;
    }
}
