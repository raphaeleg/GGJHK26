using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    private Rigidbody2D rb;
    public PlayerAwarenessController playerAwarenessController;
    private Vector2 targetDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }



    private void FixedUpdate()
    {
        UpdateTargetDirection();
        SetVelocity();
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
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * speed;
        }
    }

}
