using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    public float movementRange = 5f; // The range within which the character can move
    public float rotationSpeed = 5f; // The speed at which the character rotates

    private bool isIdle = true, isWalking = false;
    NavMeshAgent agent;
    public float health = 100;
    public float attackRange = 5f;
    public float aggroRange = 15f;
    public float attackCD = 4f;
    public float attackDamage = 30;

    float timePassed;
    float newDestinationCD = 0.5f;
    private float lastAttackTime;
    GameObject playerCharacter;
    public float maxDistance = 2.0f;
    private bool isDead = false;
    private Vector3 initialPosition;
    private Rigidbody rb;
    private Vector3 attackPosition;
    private Quaternion initialRotation;
    public LayerMask characterLayer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerCharacter = GameObject.FindWithTag("Character");
        agent = GetComponent<NavMeshAgent>();
        animator.SetBool("isDead", isDead);
        animator.SetBool("isWalking", isWalking);
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (!isDead)
        {
            HandleAttackBehavior();
            CheckInitialPotision();
        }
    }

    private void HandleAttackBehavior()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerCharacter.transform.position);

        if (distanceToPlayer <= aggroRange)
        {
            // Rotate enemy to face the player
            Vector3 direction = (playerCharacter.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            //Debug.Log(distanceToPlayer);
            // Check if the enemy is within attack range
            if (distanceToPlayer <= attackRange)
            {
                //agent.nextPosition = transform.position;.
                agent.destination = transform.position;
               // rb.constraints = RigidbodyConstraints.FreezeAll;
                animator.SetBool("isWalking", false);
                // Attack if enough time has passed since the last attack
                if (Time.time - lastAttackTime >= attackCD)
                {
                    AttackCharacter();
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                animator.SetBool("isWalking", true);

                // Move towards the player if not within attack range
                agent.SetDestination(playerCharacter.transform.position);
            }
        }
        else
        {
            // Return to initial position if player is out of aggro range
            agent.SetDestination(initialPosition);
        }

    }

    private void AttackCharacter()
    {
        if (!playerCharacter.GetComponent<CharacterCombat>().isDead)
        {
            // Trigger attack animation
            animator.SetTrigger("Attack");
            playerCharacter.GetComponent<CharacterCombat>().TakeDamage(attackDamage);
            Debug.Log("Enemy hit! Health: " + playerCharacter.GetComponent<CharacterCombat>().health
                + "Character Condition: " + playerCharacter.GetComponent<CharacterCombat>().isDead);
        }

    }

    public void TakeDamage(float damageAmount)
    {
        if (health > 0)
        {
            health -= damageAmount;
            animator.SetTrigger("isGettingDamage");
        }

        CheckDeath();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            isDead = true;
            isWalking = false;
            animator.SetBool("isWalking", isWalking);
            animator.SetBool("isDead", isDead);
        }
    }

    private void CheckInitialPotision()
    {
        float distanceToInitialPosition = Vector3.Distance(transform.position, initialPosition);

        if (distanceToInitialPosition <= 0.5f)
        {
            isWalking = false;
            animator.SetBool("isWalking", isWalking);
            transform.rotation = initialRotation;
        }
    }

    /*private void CheckIfCharacterKilled()
    {
        if (playerCharacter.GetComponent<CharacterCombat>().health < 0 && agent.destination != initialPosition)
        {
            isWalking = true;
            animator.SetBool("isWalking", isWalking);
            agent.SetDestination(initialPosition);
        } else
        {
            isWalking = false;
            animator.SetBool("isWalking", isWalking);
        }
    }*/

   /* private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }*/

    public float GetHealth()
    {
        return health;
    }

    public float GetAttackRange()
    {
        return attackRange;
    }

    public bool GetDeathCondition()
    {
        return isDead;
    }
}
