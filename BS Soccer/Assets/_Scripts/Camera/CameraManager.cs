using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera manager rotating the camera in the right position, where the user is able to move it forwards and backwards
/// </summary>
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
        // filling variables at start for the reset functionality
        parent = transform.parent;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Camera can still move even without the game data being "empty"
    private void FixedUpdate()
    {
        LookAtTarget();
        HandleZoom();
    }

    /// <summary>
    /// Calculating the rotation to the ball object, and applying to this transform being since the camera is attached
    /// </summary>
    private void LookAtTarget()
    {
        Quaternion rotationToBall = Quaternion.LookRotation(Ball.Instance.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToBall, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Repositioning of the camera in the hierarchy to make third person view on players possible
    /// </summary>
    /// <param name="_parent">The new parent object where the camera will be a child of</param>
    /// <param name="_position">The position where the camera is placed at</param>
    public void SetOrigin(Transform _parent, Vector3 _position)
    {
        transform.parent = _parent;
        transform.position = _position;
    }

    /// <summary>
    /// Default camera reset to origin
    /// </summary>
    public void SetOrigin()
    {
        transform.parent = parent;
        transform.position = startPosition;
        transform.rotation = startRotation;
    }

    /// <summary>
    /// Simply checks for input keys and applies zoom in the specific direction
    /// Also toggles the canvas on reset 
    /// </summary>
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

    /// <summary>
    /// Moves the object in the direction vector, with the zoomspeed multiplied over time
    /// </summary>
    /// <param name="_direction">Zoom direction</param>
    private void ApplyZoom(Vector3 _direction)
    {
        transform.Translate(_direction * zoomSpeed * Time.deltaTime);
    }
}
