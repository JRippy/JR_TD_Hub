using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    public float timeCounter = 0;

    public float speed;
    public float width;
    public float height;

    // Start is called before the first frame update
    void Start()
    {
        speed = 3;
        width = 4;
        height = 4;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * width;
        float y = Mathf.Sin(timeCounter) * height;
        float z = 10;

        transform.position = new Vector3(x, y, z);
    }
}
