using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{

    private Animator animator;
    [SerializeField] float damage = 20;
    //private int health = 100;
    [SerializeField] float attackRange = 2.5f;
    [SerializeField] float attackCD = 1.0f;
    public LayerMask enemyLayer;
    private bool hasAttacked;
    private float lastAttackTime; // Time when the last attack occurred


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
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
        // Detect enemies within attack range using sphere overlap
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, 2f, enemyLayer);
        // Check if any enemies are detected
        if (hitEnemies.Length > 0)
        {
            // Get the first collider detected
            Collider firstEnemyCollider = hitEnemies[0];

            // Assuming enemies have an Enemy script with TakeDamage method
            Enemy enemy = firstEnemyCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Enemy hit! Name: " + firstEnemyCollider.name + ", Health: " + enemy.getHealth());
                enemy.TakeDamage(damage);
            }
        }

    }


}
