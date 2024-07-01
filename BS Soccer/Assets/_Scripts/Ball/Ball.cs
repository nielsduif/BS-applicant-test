using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private TextMeshPro ballSpeed;
    private int currentFrame;

    public static Ball Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetPosition();
    }

    private void FixedUpdate()
    {
        if (FramesAvailable())
        {
            SetPosition();
            currentFrame++;
        }
    }

    private void SetPosition()
    {
        if (currentFrame < DataParser.Instance.frames.Count)
        {
            DataParser.DataFrame frame = DataParser.Instance.frames[currentFrame];
            transform.position = Util.Float3ToVector(frame.Ball.Position);

            UpdateSpeedText(frame.Ball.Speed);
        }
    }

    private void UpdateSpeedText(float _speed)
    {
        ballSpeed.text = $"{Mathf.Ceil(_speed)}";
    }

    private bool FramesAvailable()
    {
        return (currentFrame < DataParser.Instance.frames.Count);
    }
}
