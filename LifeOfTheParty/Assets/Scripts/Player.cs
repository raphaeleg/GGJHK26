using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed = 5.0f;

    private Vector2 movement;
    private Vector2 moveDirection;
    
    private bool isDashUnlocked = false;
    private bool isBarrierUnlocked = false;
    private bool isRepelUnlocked = false;
    private TrailRenderer trail;
    public Animator animator;

    public GameManager gameManager;
    public InventoryManager inventoryManager;

    public SpriteLibraryAsset[] spriteLibraryAssetArray;
    public SpriteLibrary spriteLibrary;
    private int spriteLibraryInt;

    public bool isDead;

    [Header("Dash")]
    [SerializeField] public float dashSpeed = 10.0f;
    [SerializeField] public float dashDuration = 1.0f;
    [SerializeField] public float dashCooldown = 5.0f;
    bool isDashing;

    [Header("Barrier")]
    [SerializeField] public float barrierDuration = 3.0f;
    [SerializeField] public float barrierCooldown = 5.0f;
    bool isBarrierActive = false;

    [Header("Repel")]
    [SerializeField] public float repelDuration = 3.0f;
    [SerializeField] public float repelCooldown = 5.0f;
    bool isRepelActive = false;
    

    private void Awake()
    {
        trail = GetComponentInChildren<TrailRenderer>();
        trail.enabled = false;
        gameManager = FindFirstObjectByType<GameManager>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        spriteLibrary = GetComponent<SpriteLibrary>();
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        moveDirection = new Vector2(movement.x, movement.y);
        
        if (Input.GetKeyDown(KeyCode.Space) && isDashUnlocked) {
            Debug.Log("Space down");
                StartCoroutine(DashMask());
        }
        if (Input.GetKeyDown(KeyCode.J) && isBarrierUnlocked) {
                StartCoroutine(BarrierMask());
        }
        if (Input.GetKeyDown(KeyCode.K) && isRepelUnlocked) {
                StartCoroutine(RepelMask());
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
       string collider = collision.gameObject.tag;
        switch (collider)
        {
            case "Guard":
            Debug.Log("Caught!");
            break; 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string collider = other.gameObject.tag;
        switch (collider)
        {
            case "DashMask":
            isDashUnlocked = true;
            inventoryManager.ShowDashMaskIcon();
            Debug.Log("Unlocked Dash Mask!");

            Destroy(other.gameObject);
            break;

            case "BarrierMask":
            isBarrierUnlocked = true;
            inventoryManager.ShowBarrierMaskIcon();
            Debug.Log("Unlocked Barrier Mask!");

            Destroy(other.gameObject);
            break;

            case "RepelMask":
            isRepelUnlocked = true;
            inventoryManager.ShowRepelMaskIcon();
            Debug.Log("Unlocked Repel Mask!");

            Destroy(other.gameObject);
            break;

            case "Hands":
            Debug.Log("Caught!");
            if (isDead)
                {
                    break;
                }
            isDead = true;
            gameManager.GameOver();
            break;
        }
    }

    private IEnumerator DashMask()
    {
        isDashing = true;
        trail.enabled = true;
        this.gameObject.layer = LayerMask.NameToLayer("IgnoreNPCs");
        spriteLibraryInt = 1;
        spriteLibrary.spriteLibraryAsset = spriteLibraryAssetArray[spriteLibraryInt];
        rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        trail.enabled = false;
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private IEnumerator BarrierMask()
    {
        isBarrierActive = true;
        Debug.Log("Barrier Active!");
        spriteLibraryInt = 2;
        spriteLibrary.spriteLibraryAsset = spriteLibraryAssetArray[spriteLibraryInt];
        yield return new WaitForSeconds(barrierDuration);
        isBarrierActive = false;
    }

    public bool GetBarrierActive()
    {
        return isBarrierActive;
    }

    private IEnumerator RepelMask()
    {
        isRepelActive = true;
        Debug.Log("Repel Active!");
        spriteLibraryInt = 3;
        spriteLibrary.spriteLibraryAsset = spriteLibraryAssetArray[spriteLibraryInt];
        yield return new WaitForSeconds(repelDuration);
        isRepelActive = false;
    }

    public bool GetRepelActive()
    {
        return isRepelActive;
    }
}
