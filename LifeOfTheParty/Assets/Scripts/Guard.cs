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
        // RotateTowardsTarget();
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

    // private void RotateTowardsTarget()
    // {
    //     if (targetDirection == Vector2.zero)
    //     {
    //         return;
    //     }
    //     Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
    //     Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    //     rb.SetRotation(rotation);
    // }

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
