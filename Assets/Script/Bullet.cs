using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem
{
	public class Bullet : MonoBehaviour
	{
		public ParticleSystem bulletTrace;

		// Start is called before the first frame update
		void Start()
		{
			if (bulletTrace != null)
			{
				bulletTrace.Play();
			}
		}

		// Update is called once per frame
		void Update()
		{

		}

		void OnCollisionEnter(Collision collision)
		{
			//if (inFlight)
			//{
				Rigidbody rb = GetComponent<Rigidbody>();
				bool hitBalloon = collision.collider.gameObject.GetComponent<Balloon>() != null;

				if (bulletTrace != null)
				{
					bulletTrace.Stop(true);
				}

				if (hitBalloon)
				{
					collision.collider.gameObject.SendMessageUpwards("ApplyDamage", SendMessageOptions.DontRequireReceiver);
				}
			//}
		}


	}

}