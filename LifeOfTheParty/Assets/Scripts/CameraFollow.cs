using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTarget;

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(0f, playerTarget.position.y, -10f);

        transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
    }
}
