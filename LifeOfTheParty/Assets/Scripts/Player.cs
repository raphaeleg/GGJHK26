using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed = 5.0f;

    private Vector2 movement;
    private Vector2 moveDirection;
    private bool isDashUnlocked = false;
    private TrailRenderer trail;

    [Header("Dash")]
    [SerializeField] public float dashSpeed = 10.0f;
    [SerializeField] public float dashDuration = 1.0f;
    [SerializeField] public float dashCooldown = 5.0f;
    bool isDashing;

    private void Awake()
    {
        trail = GetComponentInChildren<TrailRenderer>();
        trail.enabled = false;
    }

    private void Start()
    {

    } 

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(movement.x, movement.y);
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && isDashUnlocked) {
                StartCoroutine(DashMask());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string collider = other.gameObject.tag;
        switch (collider)
        {
            case "Guard":
            Debug.Log("Caught!");
            break;

            case "DashMask":
            isDashUnlocked = true;
            Debug.Log("Unlocked Dash Mask!");

            Destroy(other.gameObject);
            break;
        }
    }

    private IEnumerator DashMask()
    {
        isDashing = true;
        trail.enabled = true;
        this.gameObject.layer = LayerMask.NameToLayer("IgnoreNPCs");
        rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        trail.enabled = false;
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
