using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float enemySpeed;

    private Rigidbody2D enemyRigidbody;

    [SerializeField] float playerChaseRange;
    [SerializeField] float playerKeepChaseRange;

    public bool isChasing;

    private Vector3 directionToMoveIn;

    Transform PlayerToChase;                                      

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        PlayerToChase = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerKeepChaseRange);

    }
}
