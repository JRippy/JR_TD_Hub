using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerDefender : MonoBehaviour
{
    public int healthMaxGenerator = 100;
    public int healthCurrentGenerator = 0;
    public int healthMaxShield = 100;
    public int healthCurrentShield = 0;

    public ProtectionBar protec;
    public HealthBar healthBar;
    public ProtectionBar protec2;
    public HealthBar healthBar2;

    //Attack generator
    public GameObject[] zombies;
    private bool shieldDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        protec.SetMaxHealth(healthMaxShield);
        protec2.SetMaxHealth(healthMaxShield);
        healthCurrentShield = healthMaxShield;

        healthBar.SetMaxHealth(healthMaxGenerator);
        healthBar2.SetMaxHealth(healthMaxGenerator);
        healthCurrentGenerator = healthMaxGenerator;
    }

    void Update()
    {
        protec.SetHealth(healthCurrentShield);
        protec2.SetHealth(healthCurrentShield);
        healthBar.SetHealth(healthCurrentGenerator);
        healthBar2.SetHealth(healthCurrentGenerator);

        if (healthCurrentShield <= 0 && shieldDestroyed == false)
        {
            attackGenerator();
        }
    }

    void attackGenerator()
    {
        zombies = GameObject.FindGameObjectsWithTag("Zombie");

        foreach (GameObject z in zombies)
        {
            if (z.GetComponent<Enemy>() != null)
            {
                Enemy e = z.GetComponent<Enemy>();
                if (e.isAttacking())
                {
                    e.zombieEndAttack();
                }
                
            }
        }

        shieldDestroyed = true;
    }
}
