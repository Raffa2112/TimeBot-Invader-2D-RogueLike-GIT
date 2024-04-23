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

    [SerializeField] GameObject deathSplatter;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        PlayerToChase = FindObjectOfType<PlayerController>().transform;

        enemyAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
      if(Vector3.Distance(transform.position, PlayerToChase.position)<playerChaseRange)
        {
            directionToMoveIn = PlayerToChase.position - transform.position;
            isChasing = true;
        }
        else if (Vector3.Distance(transform.position, PlayerToChase.position) < playerKeepChaseRange && isChasing)
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

        if (directionToMoveIn != Vector3.zero)
        {
            enemyAnimator.SetBool("isWalking", true);
        }
        else
        {
            enemyAnimator.SetBool("isWalking", false);
        }

        if (PlayerToChase.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }


    }   public void DamageEnemy(int damageTaken)
    {
        enemyHealth -= damageTaken;
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

    }
}
