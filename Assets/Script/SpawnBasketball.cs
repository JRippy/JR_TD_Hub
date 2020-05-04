using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SpawnBasketball : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform pointSpawn;

    void Start()
    {
        if (ballPrefab == null)
        {
            return;
        }
    }

        // Start is called before the first frame update
        public void OnPress(Hand hand)
    {
        Debug.Log("SteamVR Button pressed!");
    }

    public void OnCustomButtonPress()
    {
        Debug.Log("We pushed our custom button!");

        GameObject balloon = Instantiate(ballPrefab, pointSpawn.position, pointSpawn.rotation) as GameObject;

    }
}
