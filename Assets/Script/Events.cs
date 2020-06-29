using UnityEngine;
using Valve.VR.InteractionSystem;

public class Events : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform pointSpawn;

    public void OnPress(Hand hand)
    {
        Debug.Log("SteamVR Button pressed!");

        if (ballPrefab == null)
        {
            return;
        }
        else
        {
            GameObject balloon = Instantiate(ballPrefab, pointSpawn.position, pointSpawn.rotation) as GameObject;
        }
        
    }

    //No VR button
    public void OnCustomButtonPress()
    {
        Debug.Log("We pushed our custom button!");

        if (ballPrefab == null)
        {
            return;
        }
        else
        {
            GameObject balloon = Instantiate(ballPrefab, pointSpawn.position, pointSpawn.rotation) as GameObject;
        }
    }
}