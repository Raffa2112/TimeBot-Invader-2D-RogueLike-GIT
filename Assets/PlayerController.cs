using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] int movementSpeed;
    private Vector2 movementInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        //transform.position = transform.position + new Vector3(0.1f, 1f, 0f);
        //transform.position += new Vector3(movementInput.x,movementInput.y, 0f) * movementSpeed * Time.deltaTime;
        playerRigidbody.velocity = movementInput * movementSpeed;
    }
}
