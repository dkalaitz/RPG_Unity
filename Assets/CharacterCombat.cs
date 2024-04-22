using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{

    private Animator animator;
    
    public float damage = 20;
    public float health = 100;
    public float attackRange = 2.5f;
    public float attackCD = 1.0f;
    public LayerMask enemyLayer;
    private float lastAttackTime; // Time when the last attack occurred
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time - lastAttackTime >= attackCD)
        {
            animator.SetTrigger("Attacking");
            PerformAttack();

            // Update the last attack time
            lastAttackTime = Time.time;

        }
    }



    private void PerformAttack()
    {

        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, attackRange, enemyLayer))
        {

            Enemy enemy = hit.collider.GetComponent<Enemy>();
            //attackRange = enemy.attackRange;
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                if (enemy.GetHealth() > 0)
                {
                    //Debug.Log("Enemy hit! Name: " + enemy.name + ", Health: " + enemy.GetHealth() +
                    //    ", Is Dead: " + enemy.GetDeathCondition());
                }
                else
                {
                   // Debug.Log("Enemy hit! Name: " + enemy.name + ", Health: " + enemy.GetHealth()
                    //    + ", Is Dead: " + enemy.GetDeathCondition());
                }
            }
        }

    }

    public void TakeDamage(float damageAmount)
    {
        if (health > 0)
        {
            health -= damageAmount;
        }
        CheckCharacterDeath();
    }

    private void CheckCharacterDeath()
    {
        if (health <= 0)
        {
            isDead = true;
            animator.SetBool("isMoving", false);
            animator.SetBool("isDead", isDead);
        }
    }




}
