using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private int rotationSpeed = 2;

    private void FixedUpdate()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        Quaternion rotationToBall = Quaternion.LookRotation(Ball.Instance.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToBall, rotationSpeed * Time.deltaTime);
    }
}
