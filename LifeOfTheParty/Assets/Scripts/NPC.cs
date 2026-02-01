using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // STATE MACHINE
    [SerializeField] private GameObject crowdRef;
    [SerializeField] private float npcSpeed = 5.0f;
    [SerializeField] private float npcRepelSpeed = 10.0f;
    [SerializeField] private float npcRepelDuration = 0.15f;
    [SerializeField] private float npcAttractForce = 1f;
    [SerializeField] private float npcFreezeTime = 1f;


    public PlayerAwarenessController playerAwarenessController;
    private Rigidbody2D rb;
    private Vector2 targetDirection;
    public Player player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        player = FindFirstObjectByType<Player>();
    }

        private void FixedUpdate()
    {
        if (player.GetBarrierActive())
        {
            UpdateTargetDirection();
            SetVelocity();
        }
        else if (player.GetRepelActive())
        {
            UpdateTargetDirection();
            StartCoroutine(Knockback());
        }
    } 

    public void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void UpdateTargetDirection()
    {
        if (playerAwarenessController.awareOfPlayer)
        {
            targetDirection = playerAwarenessController.directionToPlayer;
        }
        else
        {
            targetDirection = Vector2.zero;
        }
    }

    private void SetVelocity()
    {
        if (targetDirection == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * npcSpeed;
        }
    }
    // Default behaviour: stick to black of CROWDREF, move slightly ish
    // Repel behaviour

    private IEnumerator Knockback()
    {
        rb.velocity = new Vector2(-targetDirection.x, -targetDirection.y) * npcRepelSpeed;
        yield return new WaitForSeconds(npcRepelDuration);
    }
    // Freeze Behaviour, ignore black and stay still for x seconds (use GAMEMANAGER)
}
