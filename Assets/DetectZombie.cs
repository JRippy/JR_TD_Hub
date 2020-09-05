using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZombie : MonoBehaviour
{
    public BoxCollider box;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            animator.SetBool("character_nearby", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            animator.SetBool("character_nearby", false);
        }
    }
}
