using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private int rotationSpeed = 2;
    private Transform parent;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        parent = transform.parent;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        Quaternion rotationToBall = Quaternion.LookRotation(Ball.Instance.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToBall, rotationSpeed * Time.deltaTime);
    }

    public void SetOrigin(Transform _parent, Vector3 _position)
    {
        transform.parent = _parent;
        transform.position = _position;
    }

    public void SetOrigin()
    {
        transform.parent = parent;
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
