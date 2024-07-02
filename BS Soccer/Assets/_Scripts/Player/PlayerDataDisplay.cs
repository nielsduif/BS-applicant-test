using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDataDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro jerseyNumber, speedNumber;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private Transform playerSelectionCircle;

    private void Start()
    {
        SetPlayerSelection(false);
    }

    public void SetJerseyNumber(int _number)
    {
        jerseyNumber.text = $"{_number}";
    }

    public void UpdateSpeedText(float _speed)
    {
        speedNumber.text = $"{Mathf.Ceil(_speed)}";
    }

    private void OnMouseDown()
    {
        CameraManager.Instance.SetOrigin(cameraPosition, cameraPosition.transform.position);
        CanvasManager.Instance.SetPlayerCanvasActive(true);
    }

    private void OnMouseEnter()
    {
        SetPlayerSelection(true);
    }

    private void OnMouseExit()
    {
        SetPlayerSelection(false);
    }

    private void SetPlayerSelection(bool _active)
    {
        playerSelectionCircle.gameObject.SetActive(_active);
    }
}