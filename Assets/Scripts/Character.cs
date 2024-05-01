using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{

    public int level = 1;
    private int previousLevel = 1;
    public float health;
    private float fullHealth = 50f;
    public float damage = 5;
    public bool isDead = false;
    private Enemy enemy;
    public ProgressBar Pb;
    private Animator animator;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        StartCoroutine(HealOverTime());
        health = fullHealth;
        Pb.BarValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        CheckCharacterDeath();
        UpdateAttributes();
        if (isDead)
        {
            Revive();
        }
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
                health = Mathf.Clamp(health, 0, fullHealth);
            }
        }
    }


    private void CheckHealth()
    {
        if (health < 0)
        {
            health = 0;
        }
        Pb.BarValue = ((float)Math.Round((health / fullHealth) * 100, 2));
    }

    private void CheckCharacterDeath()
    {
        if (health <= 0)
        {
            isDead = true;
            animator.SetBool("isMoving", false);
            animator.SetBool("isDead", true);
        }
    }

    private void Revive()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("isDead", false);
            animator.SetBool("isMoving", false);
            transform.position = initialPosition;
            health = fullHealth;
            isDead = false;
        }
    }

    private void UpdateAttributes()
    {
        if (level == previousLevel + 1)
        {
            damage += 10;
            fullHealth += 20;
            previousLevel++;
        }
    }
}
