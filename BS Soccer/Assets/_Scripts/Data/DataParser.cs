using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that filters out all variables from the raw data file, creating classes with the specific variables stored inside of them
/// Used https://jsoneditoronline.org/ to easily show all the stored content
/// AnimationContext and Posse
/// </summary>
public class DataParser : MonoBehaviour
{
    public class Player
    {
        public int ID;
        public float Timestamp;
        public Vector3 Position;
        public float Speed;
        public int TeamSide;
        public int JerseyNumber;
        public PlayerContext playerContext;
    }

    public class PlayerContext
    {
        public bool HasBallPosession;
        public int PlayerState;
        public float MovementOrientation;
    }

    public class Ball
    {
        public int ID;
        public float TimeStamp;
        public Vector3 Position;
        public float Speed;
        public int TeamSide;
        public int JerseyNumber;
        public TrackableBallContext ballContext;
    }

    public class TrackableBallContext
    {
        public int BallState;
        public int Possesion;
    }

    public class GameClockContext
    {
        public int Period;
        public int Minute;
        public int Second;
        public int InjuryTime;
    }

    public class MathScoreContext
    {
        public int HomeSore;
        public int AwayScore;
    }
}
