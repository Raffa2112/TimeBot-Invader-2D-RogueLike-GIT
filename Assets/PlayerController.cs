using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int movementSpeed;
    private Vector2 movementInput;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hey Im in start being called once");   
    }

    // Update is called once per frame
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        Debug.Log ("X: "+ movementInput.x);
        Debug.Log ("Y: "+ movementInput.y);
        Debug.Log ("this is the Vector2: " + movementInput);
       
    }
}
