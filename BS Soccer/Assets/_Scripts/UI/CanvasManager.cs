using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI frameTextField;
    private int currentFrame;

    private void FixedUpdate()
    {
        if (currentFrame < DataParser.Instance.frames.Count)
        {
            DataParser.DataFrame frame = DataParser.Instance.frames[currentFrame];
            frameTextField.text = $"{frame.FrameCount}";
            currentFrame++;
        }
    }
}
