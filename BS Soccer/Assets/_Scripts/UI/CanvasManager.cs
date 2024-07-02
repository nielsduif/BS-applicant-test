using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updating different elements of the UI with the data from the dataset
/// </summary>
public class CanvasManager : Singleton<CanvasManager>
{
    [SerializeField] private TextMeshProUGUI frameTextField;
    [SerializeField] private Slider frameSlider;
    [SerializeField] private Transform playerSelectionPanel;

    private void Start()
    {
        frameSlider.maxValue = DataParser.Instance.frames.Count;    //adjusting the maxsize to make the slider work correct
        SetPlayerCanvasActive(false);
    }

    private void FixedUpdate()
    {
        if (FrameManager.Instance.FramesAvailable())
        {
            DataParser.DataFrame frame = DataParser.Instance.frames[FrameManager.Instance.CurrentFrame];
            frameTextField.text = $"{frame.FrameCount}";            //actual frame in data
            frameSlider.value = FrameManager.Instance.CurrentFrame; //making the slider move during playtime according to the actual data
        }
    }

    //Connection between the actual slider in the UI, updating the new current frame
    public void UpdateFrames()
    {
        FrameManager.Instance.SetCurrentFrame((int)frameSlider.value);
    }

    /// <summary>
    /// Function to disable or enable the playercanvas
    /// </summary>
    /// <param name="_active">True turns on the object, false turns it off</param>
    public void SetPlayerCanvasActive(bool _active)
    {
        playerSelectionPanel.gameObject.SetActive(_active);
    }
}