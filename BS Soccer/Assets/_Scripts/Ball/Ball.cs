using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
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

    private void FixedUpdate()
    {
        DataParser.DataFrame frame = DataParser.Instance.frames[currentFrame];
        transform.position = Util.Float3ToVector(frame.Ball.Position);
        currentFrame++;
    }
}
