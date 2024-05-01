using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{

    private Animator animator;

    public float attackRange = 2.5f;
    public float attackCD = 1.0f;
    public LayerMask enemyLayer;
    private float lastAttackTime; // Time when the last attack occurred
    private Character character;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!character.isDead)
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
            if (enemy != null)
            {
                enemy.TakeDamage(character.damage);
                if (enemy.health > 0)
                {
                    Debug.Log("Enemy hit! Name: " + enemy.name + ", Health: " + enemy.health +
                        ", Is Dead: " + enemy.isDead);
                }
                else
                {
                    Debug.Log("Enemy hit! Name: " + enemy.name + ", Health: " + enemy.health
                        + ", Is Dead: " + enemy.isDead);
                }
            }
        }

    }

    public void TakeDamage(float damageAmount)
    {
        if (character.health > 0)
        {
            character.health -= damageAmount;
        }
    }




}
