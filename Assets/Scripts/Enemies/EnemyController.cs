using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float enemySpeed;

    private Rigidbody2D enemyRigidbody;

    [SerializeField] float playerChaseRange;

    private Vector3 directionToMoveIn;

    Transform PlayerToChase;                                      

    // Start is called before the first frame update
    void Start()
    {
        PlayerToChase = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
      if(Vector3.Distance(transform.position, PlayerToChase.position)<playerChaseRange)
        {
            Debug.Log("Player is in chase range");
        }
      else
        {
            Debug.Log("Player is out of chase Range");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange);
    }
}
