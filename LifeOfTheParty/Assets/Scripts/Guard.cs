using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
