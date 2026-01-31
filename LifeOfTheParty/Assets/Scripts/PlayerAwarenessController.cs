using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool awareOfPlayer;

    public Vector2 directionToPlayer;

    public Player player;

    [SerializeField]
    private float playerAwarenessDistance;

    public float speed = 5f;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>();
    }

    private void Update()
    {
        Vector2 enemyToPlayerVector = player.transform.position - this.transform.position;
        directionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= playerAwarenessDistance)
        {
            awareOfPlayer = true;
        }
        else
        {
            awareOfPlayer = false;
        }
    }
}
