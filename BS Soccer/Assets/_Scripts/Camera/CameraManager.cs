using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private int rotationSpeed = 2;
    private Transform parent;
    private Vector3 startPosition;
    private Quaternion startRotation;
    [SerializeField] private KeyCode zoomIn = KeyCode.Q, zoomOut = KeyCode.W, resetZoom = KeyCode.E;
    [SerializeField] int zoomSpeed = 10;

    private void Start()
    {
        parent = transform.parent;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        LookAtTarget();
        HandleZoom();
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

    private void HandleZoom()
    {
        if (Input.GetKey(zoomIn))
        {
            ApplyZoom(Vector3.forward);
        }
        else if (Input.GetKey(zoomOut))
        {
            ApplyZoom(Vector3.back);
        }
        else if (Input.GetKey(resetZoom))
        {
            SetOrigin();
            CanvasManager.Instance.SetPlayerCanvasActive(false);
        }
    }

    private void ApplyZoom(Vector3 _direction)
    {
        transform.Translate(_direction * zoomSpeed * Time.deltaTime);
    }
}
