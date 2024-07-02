using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
