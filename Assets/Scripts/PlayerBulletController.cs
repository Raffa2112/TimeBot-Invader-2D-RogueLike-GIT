using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 5f;

    private Rigidbody2D bulletRigidBody;
    [SerializeField] GameObject BulletImpactEffect;



    // Start is called before the first frame update
    void Start()
    {
        bulletRigidBody = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        bulletRigidBody.velocity = transform.right * bulletSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(BulletImpactEffect, transform.position, transform.rotation);
        Destroy(gameObject);  
    }
}

