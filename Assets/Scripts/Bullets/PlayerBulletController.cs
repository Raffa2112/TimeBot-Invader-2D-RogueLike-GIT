using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 5f;

    private Rigidbody2D bulletRigidBody;
    [SerializeField] GameObject BulletImpactEffect;

    [SerializeField] int damageAmount = 10;

  


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
        Instantiate(BulletImpactEffect.transform, transform.position, transform.rotation);
        //impactRffect.GetComponent<ImpactEffectDestroyer>().Destroy();

        if (collision.CompareTag("Enemy"))
            collision.GetComponent<EnemyController>().DamageEnemy(damageAmount);                   

        Destroy(gameObject);
    }
}

