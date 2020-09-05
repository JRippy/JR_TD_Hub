using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GunLowPoly : MonoBehaviour
{
    public SteamVR_Action_Boolean fireAction;
    public float speed = 40;
	public float weaponRange = 20f;
	public GameObject bullet;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;
	public Transform gunEnd;

	public ParticleSystem muzzleFlash;
    public ParticleSystem cartridgeEjection;

    public GameObject metalHitEffect;
    public GameObject stoneHitEffect;
    public GameObject waterLeakEffect;
    public GameObject woodHitEffect;

    private Interactable interactable;

    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    void Update()
    {
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            if (fireAction[source].stateDown)
            {
                muzzleFlash.Play();
                cartridgeEjection.Play();

				Vector3 rayOrigin = gunEnd.position;
				RaycastHit hit;
				if (Physics.Raycast(rayOrigin, gunEnd.forward, out hit, weaponRange))
				{
					HandleHit(hit);
				}

                Fire();


			}
        }
    }

    public void Fire()
    {
        GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
        audioSource.PlayOneShot(audioClip);
        Destroy(spawnedBullet, 2);

    }


	void HandleHit(RaycastHit hit)
	{
		if (hit.collider.sharedMaterial != null)
		{
			string materialName = hit.collider.sharedMaterial.name;
			Debug.Log(materialName);

			switch (materialName)
			{
				case "Metal":
					SpawnDecal(hit, metalHitEffect);
					break;
				//case "Sand":
				//	SpawnDecal(hit, sandHitEffect);
					//break;
				case "Stone":
					SpawnDecal(hit, stoneHitEffect);
					break;
				case "WaterFilled":
					SpawnDecal(hit, waterLeakEffect);
					SpawnDecal(hit, metalHitEffect);
					break;
				case "Wood":
					SpawnDecal(hit, woodHitEffect);
					break;
				//case "Meat":
				//	SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
				//	break;
				//case "Character":
				//	SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
				//	break;
				//case "WaterFilledExtinguish":
				//	SpawnDecal(hit, waterLeakExtinguishEffect);
				//	SpawnDecal(hit, metalHitEffect);
				//	break;
			}
		}
	}

	void SpawnDecal(RaycastHit hit, GameObject prefab)
	{
		GameObject spawnedDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
		spawnedDecal.transform.SetParent(hit.collider.transform);
	}
}
