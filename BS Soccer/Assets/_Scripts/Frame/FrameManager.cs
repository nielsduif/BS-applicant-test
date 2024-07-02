using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Time manager for the entire game, based on the frames provided in the data
/// Handles pausing, playing and restarting the data simulation
/// </summary>
public class FrameManager : Singleton<FrameManager>
{
    public int CurrentFrame { get; private set; } = 0;      //frames index
    private PlayMode playMode = PlayMode.Play;
    private PlayMode previousMode;

    public enum PlayMode
    {
        Play,
        Pause,
        Restart
    }

    private void FixedUpdate()
    {
        if (FramesAvailable())
        {
            AdjustFrameLogic();
        }
    }

    /// <summary>
    /// Check if there is still data available to display
    /// </summary>
    /// <returns>If the currentFrame is lower than the total frames in the data</returns>
    public bool FramesAvailable()
    {
        return (CurrentFrame < DataParser.Instance.frames.Count);
    }

    /// <summary>
    /// Function to manipulate the current frame, making it possible to scroll through the data
    /// </summary>
    public void SetCurrentFrame(int _value)
    {
        CurrentFrame = _value;
    }

    /// <summary>
    /// Simple switch with the PlayMode variables 
    /// Play increases CurrentFrame
    /// Pause breaks/pauses the loop
    /// Restart sets the game to the first frame and applies the previous selected PlayMode
    /// </summary>
    private void AdjustFrameLogic()
    {
        switch (playMode)
        {
            case PlayMode.Play:
                CurrentFrame++;
                break;
            case PlayMode.Pause:
                break;
            case PlayMode.Restart:
                CurrentFrame = 0;
                playMode = previousMode;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Changes the actual PlayMode enum
    /// </summary>
    /// <param name="_playMode">Play, Pause or Restart whereafter Play or Pause is set</param>
    private void SetGameMode(PlayMode _playMode)
    {
        playMode = _playMode;
    }

    //Functions for UI buttons
    public void SetPlay()
    {
        SetGameMode(PlayMode.Play);
    }
    public void SetPause()
    {
        SetGameMode(PlayMode.Pause);
    }
    public void SetRestart()
    {
        previousMode = playMode;    //saves the current mode to apply afterwards, since restarting only happens one frame
        SetGameMode(PlayMode.Restart);
    }
}