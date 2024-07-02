using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI frameTextField;
    [SerializeField] private Slider frameSlider;

    private void Start()
    {
        frameSlider.maxValue = DataParser.Instance.frames.Count;
    }

    private void FixedUpdate()
    {
        if (FrameManager.Instance.FramesAvailable())
        {
            DataParser.DataFrame frame = DataParser.Instance.frames[FrameManager.Instance.CurrentFrame];
            frameTextField.text = $"{frame.FrameCount}";
            frameSlider.value = FrameManager.Instance.CurrentFrame;
        }
    }

    public void UpdateFrames()
    {
        FrameManager.Instance.SetCurrentFrame((int)frameSlider.value);
    }
}