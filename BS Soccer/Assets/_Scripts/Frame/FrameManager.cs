using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        AdjustFrameLogic();
    }

    public bool FramesAvailable()
    {
        return (CurrentFrame < DataParser.Instance.frames.Count);
    }

    public void SetCurrentFrame(int _value)
    {
        CurrentFrame = _value;
    }

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

    private void SetGameMode(PlayMode _playMode)
    {
        playMode = _playMode;
    }

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
        previousMode = playMode;
        SetGameMode(PlayMode.Restart);
    }
}