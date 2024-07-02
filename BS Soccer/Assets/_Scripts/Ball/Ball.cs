using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Ball manager class with one instance
/// </summary>
public class Ball : Singleton<Ball>
{
    [SerializeField] private TextMeshPro ballSpeed;

    private void Start()
    {
        SetPosition();
    }

    private void FixedUpdate()
    {
        if (FrameManager.Instance.FramesAvailable())
        {
            SetPosition();
        }
    }

    /// <summary>
    /// Pulling the ball position from the data with the correct gameframe and applying to the transform
    /// Also updates the data representation for the ball speed
    /// </summary>
    private void SetPosition()
    {
        DataParser.DataFrame frame = DataParser.Instance.frames[FrameManager.Instance.CurrentFrame];
        transform.position = Util.Float3ToVector(frame.Ball.Position);

        UpdateSpeedText(frame.Ball.Speed);
    }

    private void UpdateSpeedText(float _speed)
    {
        ballSpeed.text = $"{Mathf.Ceil(_speed)}";
    }
}
