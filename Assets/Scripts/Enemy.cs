using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private float rotationSpeed = 5f;
    public ProgressBar pbUI;
    private GameObject pbObject;


    private bool isWalking = false;
    NavMeshAgent agent;
    public float health = 100;
    private float attackRange = 4.5f;
    private float aggroRange = 15f;
    private float attackCD = 4f;
    public float attackDamage = 30;

    private float lastAttackTime;
    GameObject playerCharacter;
    public bool isDead = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public LayerMask characterLayer;
    public int level = 3;

    public bool isInAggroRange = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerCharacter = GameObject.FindWithTag("Character");
        agent = GetComponent<NavMeshAgent>();
        animator.SetBool("isDead", isDead);
        animator.SetBool("isWalking", isWalking);
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        StartCoroutine(HealOverTime());
        pbObject = GameObject.Find("UI Enemy HealthBar");
        pbObject.SetActive(true);
    }

    private void Update()
    {
        if (!isDead)
        {
            HandleAttackBehavior();
            CheckInitialPotision();
            HandleHealthBarVisibility();
        }
        CheckHealth();
    }

    private void HandleAttackBehavior()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerCharacter.transform.position);

        if (distanceToPlayer <= aggroRange)
        {
            isInAggroRange = true;
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
            isInAggroRange = false;

            // audioObject.GetComponent<Audio>().StopBattleAudio();
            // Return to initial position if player is out of aggro range
            agent.SetDestination(initialPosition);
        }

    }

    private void AttackCharacter()
    {
        if (!playerCharacter.GetComponent<Character>().isDead)
        {
            // Trigger attack animation
            animator.SetTrigger("Attack");
            playerCharacter.GetComponent<CharacterCombat>().TakeDamage(attackDamage);
           // Debug.Log("Enemy hit! Health: " + playerCharacter.GetComponent<Character>().health
             //   + "Character Condition: " + playerCharacter.GetComponent<Character>().isDead);
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
            pbObject.SetActive(false);
            animator.SetBool("isWalking", isWalking);
            animator.SetBool("isDead", isDead);
            GameObject.Find("InstructionsUI").GetComponent<InstructionsUI>().showBossInstruction = false;
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

    private void CheckHealth()
    {
        pbUI.BarValue = health;

        if (health < 0)
        {
            health = 0;
        }
    }

    IEnumerator HealOverTime()
    {
        while (true)
        {
            // Wait for 2 seconds
            yield return new WaitForSeconds(2f);

            // Check if the character is not dead and enemy is not in aggro range
            if (!isInAggroRange)
            {
                // Heal the character by 10
                health += 10;

                // Ensure health does not exceed maximum
                health = Mathf.Clamp(health, 0, 100);
            }
        }
    }

    private void HandleHealthBarVisibility()
    {
        if (isInAggroRange)
        {
            pbObject.SetActive(true);
        }
        else
        {
            pbObject.SetActive(false);
        }
    }
}
