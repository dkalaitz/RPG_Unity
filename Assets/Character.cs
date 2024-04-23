using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public int level = 1;
    public float health = 100;
    public float damage = 20;
    public bool isDead = false;
    private Enemy enemy;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        StartCoroutine(HealOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();
    }

    IEnumerator HealOverTime()
    {
        while (true)
        {
            // Wait for 2 seconds
            yield return new WaitForSeconds(2f);

            // Check if the character is not dead and enemy is not in aggro range
            if (!enemy.isInAggroRange || enemy.isDead)
            {
                // Heal the character by 10
                health += 10;

                // Ensure health does not exceed maximum
                health = Mathf.Clamp(health, 0, 100);
            }
        }
    }


    private void checkHealth()
    {
        if (health < 0)
        {
            health = 0;
        }
    }
}
