using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : Singleton<CanvasManager>
{
    [SerializeField] private TextMeshProUGUI frameTextField;
    [SerializeField] private Slider frameSlider;
    [SerializeField] private Transform playerSelectionPanel;

    private void Start()
    {
        frameSlider.maxValue = DataParser.Instance.frames.Count;
        SetPlayerCanvasActive(false);
    }

    private void FixedUpdate()
    {
        if (FrameManager.Instance.FramesAvailable())
        {
            DataParser.DataFrame frame = DataParser.Instance.frames[FrameManager.Instance.CurrentFrame];
            frameTextField.text = $"{frame.FrameCount}";        //actual frame in data
            frameSlider.value = FrameManager.Instance.CurrentFrame;
        }
    }

    public void UpdateFrames()
    {
        FrameManager.Instance.SetCurrentFrame((int)frameSlider.value);
    }

    public void SetPlayerCanvasActive(bool _active)
    {
        playerSelectionPanel.gameObject.SetActive(_active);
    }
}