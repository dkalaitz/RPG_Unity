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
    [SerializeField] float health = 100;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;
    [SerializeField] float attackCD = 4f;

    float timePassed;
    float newDestinationCD = 0.5f;

    GameObject playerCharacter;
    private Transform targetPosition; // The position the character is moving towards
    public float maxDistance = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isIdle", isIdle);
        playerCharacter = GameObject.FindWithTag("Character");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        animator.SetBool("isWalking", isWalking && !isIdle);
        animator.SetBool("isIdle", isIdle && !isWalking);

        if (timePassed >= attackCD)
        {
            if (Vector3.Distance(playerCharacter.transform.position, transform.position) <= aggroRange)
            {
                animator.SetTrigger("isAttacking");
                timePassed = 0;
            }
        }
        timePassed += Time.deltaTime;

        if (newDestinationCD <= 0 && Vector3.Distance(playerCharacter.transform.position, transform.position) <= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(playerCharacter.transform.position);
        }
        newDestinationCD -= Time.deltaTime;
        transform.LookAt(playerCharacter.transform);
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
            animator.SetBool("isDead", true);
        }
    }

    public void Attack()
    {
        animator.SetTrigger("isAttacking"); // Set the animation parameter
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

    public float getHealth()
    {
        return health;
    }
}
