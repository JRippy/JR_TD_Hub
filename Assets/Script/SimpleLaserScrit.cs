using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SimpleLaserScrit : MonoBehaviour
{
    public SteamVR_Action_Boolean fireAction;
    public Transform turrel;
    public GameObject laserPrefab;
    public GameObject electroBallPrefab;
    public Transform firePoint;
    public float shootingSpeed = 1;

    //Fire rate
    public GameObject tmpTarget;
    [SerializeField] float bullet_reload = 0.5f;
    Coroutine firing_coroutine;
    bool can_shoot = true;
    bool is_shooting = false;
    [SerializeField] Vector3 targetPoint;

    private GameObject spawnedLaser;
    private HashSet<GameObject> enemies = new HashSet<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        spawnedLaser = Instantiate(laserPrefab, firePoint.transform) as GameObject;
        //DisableLaser();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //EnableLaser();

            //Rigidbody bulletrb = Instantiate(electroBallPrefab, firePoint.transform.position, firePoint.transform.rotation).GetComponent<Rigidbody>();
            //bulletrb.velocity = firePoint.transform.forward * shootingSpeed;
        }

        //if (Input.GetMouseButton(0))
        //{
        //    UpdateLaser();
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    DisableLaser();
        //}

        ShootEnemyInRange();
        //FireLaserBuggy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Debug.Log("Zombie found");
            enemies.Add(other.gameObject);
            Debug.Log(enemies.Count);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Debug.Log("Zombie exit");
            enemies.Remove(other.gameObject);
        }
    }

    void ShootEnemyInRange()
    {
        GameObject goc = FindClosestEnemy();

        if (enemies.Count != 0 && goc != null)
        {
            turrel.LookAt(goc.transform);

            //Test
            FireLaserBuggy();
        }

    }

    private void FireLaserBuggy()
    {
        if (can_shoot && !is_shooting)
        {
            is_shooting = true;
            firing_coroutine = StartCoroutine(FireContinuously());
        }
    }
    IEnumerator FireContinuously()
    {
        while (true)
        {
            //test
            RaycastHit hit;

            if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(firePoint.forward), out hit, Mathf.Infinity))
            {
                targetPoint = hit.point;
            }

            ////tmp test
            //targetPoint = tmpTarget.transform.position;
            
            
            
            GameObject bullet = GameObject.Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
            //Rigidbody bulletrb = Instantiate(laserPrefab, firePoint.position, firePoint.rotation).GetComponent<Rigidbody>();
            //bulletrb.velocity = firePoint.forward * shootingSpeed;

            bullet.GetComponent<LaserBullet>().GetPoint(targetPoint);
            yield return new WaitForSeconds(bullet_reload);
        }
    }

    void EnableLaser()
    {
        spawnedLaser.SetActive(true);
    }

    void DisableLaser()
    {
        spawnedLaser.SetActive(false);
    }

    //void UpdateLaser()
    //{
    //    if (firePoint != null)
    //    {
    //        spawnedLaser.transform.position = firePoint.transform.position;
    //    }
    //}

    GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
