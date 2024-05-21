using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float enemySpeed;

    [SerializeField] int enemyHealth = 100;

    private Rigidbody2D enemyRigidbody;

    [SerializeField] float playerChaseRange;
    [SerializeField] float playerKeepChaseRange;

    public bool isChasing;

    private Vector3 directionToMoveIn;

    Transform PlayerToChase;
    private Animator enemyAnimator;

    //Attack
    [SerializeField] bool meleeAttacker;

    [SerializeField] GameObject enemyProjectile;
    [SerializeField] Transform firePosition;

    [SerializeField] float timeBetweenShots;
    private bool readyToShoot;

    [SerializeField] GameObject deathSplatter;
    [SerializeField] GameObject damageEffect;
    [SerializeField] float shootingRange;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        PlayerToChase = FindObjectOfType<PlayerController>().transform;

        enemyAnimator = GetComponentInChildren<Animator>();
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update ()
    {
        EnemyChasingPlayer();

        AnimatingTheEnemy();
        EnemyFacingPlayerDirection();

        EnemyThrowingSwords();

    }

    private void EnemyChasingPlayer()
    {
        if (Vector3.Distance(transform.position, PlayerToChase.position) < playerChaseRange)
        {
            directionToMoveIn = PlayerToChase.position - transform.position; //Player Chase range(RED)
            isChasing = true; //getting the direction of Enemy chase
        }
        else if (Vector3.Distance(transform.position, PlayerToChase.position) < playerKeepChaseRange && isChasing) //Player keep Chase range(YELLOW)
        {
            directionToMoveIn = PlayerToChase.position - transform.position;
        }

        else
        {
            directionToMoveIn = Vector3.zero;
            isChasing = false;
        }
        directionToMoveIn.Normalize();
        enemyRigidbody.velocity = directionToMoveIn * enemySpeed;
    }

    private void EnemyFacingPlayerDirection()
    {
        if (PlayerToChase.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one; //Enemy turns to face in direction of Player
        }
    }

    private void AnimatingTheEnemy()
    {
        if (directionToMoveIn != Vector3.zero)
        {
            enemyAnimator.SetBool("isWalking", true);
        }
        else
        {
            enemyAnimator.SetBool("isWalking", false); //Access to animations
        }
    }

    private void EnemyThrowingSwords()
    {
        if (!meleeAttacker &&
                    readyToShoot &&
                    Vector3.Distance(PlayerToChase.transform.position, transform.position) < shootingRange)
        {

            readyToShoot = false;
            StartCoroutine(FireEnemyProjectile());

        }
        // Throwing swords

    }

    IEnumerator FireEnemyProjectile()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        Instantiate(enemyProjectile, firePosition.position, firePosition.rotation);
        readyToShoot = true;
    }

    public void DamageEnemy(int damageTaken)
    {
        enemyHealth -= damageTaken;

            Instantiate(damageEffect, transform.position, transform.rotation);

        if(enemyHealth <= 0)
        {
            Instantiate(deathSplatter, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }


        


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerKeepChaseRange);
     
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }
}
