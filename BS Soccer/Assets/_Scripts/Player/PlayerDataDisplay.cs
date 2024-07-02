using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Class to update the worldspace text elements 
/// Also handles clicking on the player, where the camera placement will be changed
/// </summary>
public class PlayerDataDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro jerseyNumber, speedNumber;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private Transform playerSelectionCircle;

    private void Start()
    {
        SetPlayerSelection(false);  //panel off on start
    }

    /// <summary>
    /// Directly edits the text element of the players jersey
    /// Only needs to be called at start since the jersey number wont change
    /// </summary>
    /// <param name="_number">Change the jerseynumber to</param>
    public void SetJerseyNumber(int _number)
    {
        jerseyNumber.text = $"{_number}";
    }

    /// <summary>
    /// Change speed text attached to the player
    /// Should be called for every frame
    /// </summary>
    /// <param name="_speed">Change speed text to</param>
    public void UpdateSpeedText(float _speed)
    {
        speedNumber.text = $"{Mathf.Ceil(_speed)}";
    }

    // Changed the camera to be set to the players' cameraposition transform, making a third person possible
    private void OnMouseDown()
    {
        CameraManager.Instance.SetOrigin(cameraPosition, cameraPosition.transform.position);
        CanvasManager.Instance.SetPlayerCanvasActive(true);
    }

    //Shows a selection image when hovering over the player
    private void OnMouseEnter()
    {
        SetPlayerSelection(true);
    }

    //Disables the selection image when the mouse doesnt hover over the player anymore
    private void OnMouseExit()
    {
        SetPlayerSelection(false);
    }

    /// <summary>
    /// Changes the state of the image game object with SetActive
    /// </summary>
    /// <param name="_active">Whether the object should be on/off respectively with true/false</param>
    private void SetPlayerSelection(bool _active)
    {
        playerSelectionCircle.gameObject.SetActive(_active);
    }
}