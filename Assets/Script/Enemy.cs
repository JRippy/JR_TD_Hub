using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int healthBar = 100;
    public Animator animator;
    public BoxCollider hand1;
    public BoxCollider hand2;

    private Transform target;
    private int waypointIndex = 0;
    private bool moving = false;
    private bool attack = false;
    private bool paralyse = false;
    private bool hurt = false;
    private bool dead = false;
    private bool invul = false;
    private Vector3 h1;
    private Vector3 h2;
    //private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.points[0];
        h1 = hand1.size;
        h2 = hand2.size;
        //rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetBool("Hurt", false);
        //animator.SetBool("Attack", false);
        //animator.SetBool("Electrocuted", false);

        if (!animator.GetBool("Electrocuted") && !animator.GetBool("Dead") && !animator.GetBool("Hurt") && !animator.GetBool("Attack"))
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            animator.SetFloat("Speed", Mathf.Abs(dir.normalized.x + dir.normalized.z));




            //// Determine which direction to rotate towards
            //Vector3 targetDirection = target.position - transform.position;

            //// The step size is equal to speed times frame time.
            //float singleStep = speed * Time.deltaTime;

            //// Rotate the forward vector towards the target direction by one step
            //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            //// Draw a ray pointing at our target in
            //Debug.DrawRay(transform.position, newDirection, Color.red);

            //// Calculate a rotation a step closer to the target and applies rotation to this object
            //transform.rotation = Quaternion.LookRotation(newDirection);

            //Vector3 targetDirection = new Vector3(target.position.x, this.transform.position.y, target.position.z);
            //transform.LookAt(targetDirection);
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }


        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNextWaypoint();
            Vector3 targetDirection = new Vector3(target.position.x, this.transform.position.y, target.position.z);
            transform.LookAt(targetDirection);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //print(other.tag);
        //Debug.Log("Collision Detected");
        //switch (other.tag)
        //{
        //    case "ElectroBall":
        //        print "Hello";

        //    case "Laser":
        //        print "Hello";
        //}
        if (other.tag == "ElectroBall")
        {
            print("ElectroBall");
            zombieHurt();

        }
        else if (other.tag == "Laser")
        {
            //print("Laser");
            zombieHurt();
        }
        else if (other.tag == "ElectricWall")
        {
            //print("Laser");
            zombieElectrocut();
        }
        else if (other.tag == "Shield" || other.tag == "Generator")
        {
            //rigidbody.freezeRotation = true;
            zombieAttack();

        }
        else if (other.tag == "ShieldDestroyed")
        {
            //rigidbody.constraints = RigidbodyConstraints.
            //rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            zombieEndAttack();

        }
        //else if (other.tag == "Base")
        //{
        //    print("Base");
        //    zombieAttack();
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit");
        //Debug.Log(other.tag);
        //Debug.Log(other.gameObject.name);
        if (other.tag == "Shield")
        {
            //zombieEndAttack();
            //rigidbody.freezeRotation = true;
            //other.gameObject.SetActive(false);

        }
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            zombieAttack();
            moving = false;
            //Destroy(gameObject);
            return;
        }

        waypointIndex++;
        moving = true;
        target = Waypoints.points[waypointIndex];
    }

    private void zombieAttack()
    {
        attack = true;
        moving = false;
        hand1.size = new Vector3(h1.x - 0.5f, h1.y, h1.z);
        hand2.size = new Vector3(h1.x + 0.3f, h1.y, h1.z);
        animator.SetBool("Attack", true);
    }

    public void zombieEndAttack()
    {
        attack = false;
        moving = true;
        hand2.size = new Vector3(h1.x, h1.y, h1.z);

        animator.SetBool("Attack", false);
    }

    private void zombieHurt()
    {
        //animator.SetTrigger("Zombie Reaction Hit");

        animator.SetBool("Hurt", true);
        moving = false;

        if (invul != true)
        {
            healthBar -= 50;
        }

        if (healthBar <= 0)
        {
            if (!animator.GetBool("Dead"))
            {
                animator.SetBool("Dead", true);
                animator.SetTrigger("Dead 0");
            }else
            {
                Debug.Log("Already Dead!");
            }

        }
        else
        {
            //if (animator.GetBool("Electrocuted") != true)
            //{
                animator.SetTrigger("Hit");
                //invul = true;
            //}
            
        }
    }

    private void zombieElectrocut()
    {
        animator.SetBool("Electrocuted", true);
        moving = false;

        animator.SetTrigger("Electrocut");
        //if (healthBar <= 0)
        //{
        //    animator.SetBool("Dead", true);
        //}
    }

    public void DestroyZombie()
    {
        Destroy(gameObject);
    }

    public void EndHurt()
    {
        animator.SetBool("Hurt", false);
    }

    void EndParalyse()
    {
        Debug.Log("End Paralyse");
        animator.SetBool("Electrocuted", false);
        animator.SetTrigger("StandUp"); 
    }

    public bool isAttacking()
    {
        return attack;
    }
}
