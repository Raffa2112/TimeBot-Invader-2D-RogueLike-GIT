using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRigidbody;

    [SerializeField] Transform weaponsArm;
    private Camera mainCamera;

    [SerializeField] int movementSpeed;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;

    [SerializeField] bool isWeaponAutomatic;

    [SerializeField] float timeBetweenShots;

    private float shotCounter = 0;

    private Vector2 movementInput;

    private Animator playerAnimator;

    //Dashing
    private float currentMovementSpeed;
    private bool canDash;
    [SerializeField] float dashSpeed = 20f, dashLength = 0.5f, dashCooldown = 1f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;



        playerAnimator = GetComponent<Animator>();
        shotCounter = timeBetweenShots;
        currentMovementSpeed = movementSpeed;
        canDash = true;      
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoving();

        PointingGunAtMouse();

        AnimatingThePlayer();

        PlayerShooting();

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            currentMovementSpeed = dashSpeed;
            canDash = false;

            StartCoroutine(DashCooldownCounter());

            StartCoroutine(DashLengthCounter());

            playerAnimator.SetTrigger("Dash");
            //acces the Player Health Handeler and make sure hes invinceble for a while
        }
        
    }
    IEnumerator DashCooldownCounter()
    {
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
    IEnumerator DashLengthCounter()
    {
        yield return new WaitForSeconds(dashLength);

        currentMovementSpeed = movementSpeed;

    }

    private void AnimatingThePlayer()
    {
        if (movementInput != Vector2.zero)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }
    }

    private void PointingGunAtMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        weaponsArm.rotation = Quaternion.Euler(0, 0, angle);

        if (mousePosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            weaponsArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            weaponsArm.localScale = Vector3.one;

        }
    }

    private void PlayerShooting()

    {
        if(!canDash) { return; } //{return;} means get out of method

        if (Input.GetMouseButtonDown(0) && !isWeaponAutomatic)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }

        if (Input.GetMouseButton(0) && isWeaponAutomatic)
        {
            shotCounter -= Time.deltaTime; //shotCounter - Time.deltaTime;
        }
        if (shotCounter <= 0)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            shotCounter = timeBetweenShots;
        }
    }

    private void PlayerMoving()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");


        movementInput.Normalize();

        // transform. position = transform. position + new Vector3(0.1f, . If, Of);
        // transform. position += new Vector3(movementInput.x, movementlnput.y, Of) * movementSpeed * Time.de1taTime;

        playerRigidbody.velocity = movementInput * currentMovementSpeed;
    }
}

