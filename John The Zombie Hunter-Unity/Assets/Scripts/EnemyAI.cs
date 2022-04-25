/**** 
 * Created by: Qadeem Qureshi
 * Date Created: April 23, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 23, 2022
 * 
 * Description: Handles the navigation and enemy behavior
****/

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))] // Force AI pathing on entity

public class EnemyAI: MonoBehaviour
{
    public float attackDistance = 2f; // How far does the AI reach
    public float movementSpeed = .01f; // How fast it moves
    public int npcHP = 100; // Total health
    public int npcDamage = 5; // Damage it deals
    public float destoryEnemyAfter = 10; // Apply animation and delete after this time
    
    [HideInInspector]
    public Transform playerTransform; // Set this from the enemy spawner module
    [SerializeField] private Animator m_animator = null; // animation controller associated with zombies

    private NavMeshAgent agent; // Pathing module
    public EnemySpawner spawner; // The parent spawner
    public HealthBar healthBar; // 3D health bar script

    // Start is called before the first frame update
    // Init our navmesh agent
    // Set our distances
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance;
        agent.speed = movementSpeed;

        //Set Rigidbody to Kinematic to prevent hit register bug
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    // Update is called once per frame
    // Check for attacking and route the Agent to the new position of player
    // Apply sounds as needed
    void FixedUpdate()
    {
        if (npcHP == 0) return;

        if (Random.value < .03 && Vector3.Distance(playerTransform.position, transform.root.position) < 5)
            GetComponent<EnemyAI_SoundController>().ApplySearchSound();

        m_animator.SetBool("Attack", false);

        agent.destination = playerTransform.position;
        Vector3 relativePos = agent.steeringTarget - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePos);

        if (Vector3.Distance(playerTransform.position, transform.root.position) > attackDistance)
            m_animator.SetFloat("MoveSpeed", movementSpeed * 1.5f);
        else
            m_animator.SetBool("Attack", true);
    }

    // Called from the animation NOT from code. Timed to be synchronous to action.
    void EndAttack()
    {
        playerTransform.gameObject.GetComponent<PlayerManager>().ApplyDamage(npcDamage);
        GetComponent<EnemyAI_SoundController>().ApplyAttackSound();
    }

    // External call to apply damage to our entity
    // Adjusts the healthbar above the AI
    public void ApplyDamage(int points)
    {
        npcHP -= points;
        healthBar.SetHealth(npcHP);

        if (npcHP <= 0)
        {
            healthBar.Disable();
            m_animator.SetBool("Dead", true);
            agent.isStopped = true;
            npcHP = 0;
            Destroy(transform.root.gameObject, destoryEnemyAfter);
            transform.root.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            GameManager.GM.UpdateScore(1);
            GetComponent<EnemyAI_SoundController>().ApplyDamageSound();
        }
    }

    // Tell our spawner that we lost an agent after death
    private void OnDestroy()
    {
        spawner.UpdateCounter();
    }
}