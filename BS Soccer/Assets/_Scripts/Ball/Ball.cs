using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private int currentFrame;
    private void FixedUpdate()
    {
        DataParser.DataFrame frame = DataParser.Instance.frames[currentFrame];
        transform.position = Util.Float3ToVector(frame.Ball.Position);
        currentFrame++;
    }
}
