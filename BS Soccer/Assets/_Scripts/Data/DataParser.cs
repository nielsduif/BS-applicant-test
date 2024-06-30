using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Script that filters out all variables from the raw data file, creating classes with the specific variables stored inside of them
/// Used https://jsoneditoronline.org/ to easily show all the stored content
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

    public class MatchScoreContext
    {
        public int HomeSore;
        public int AwayScore;
    }

    public class DataFrame
    {
        public int FrameCount;
        public float TimestampUTC;
        public List<Player> players;
        public Ball ball;
        public GameClockContext gameClockContext;
        public MatchScoreContext matchScoreContext;
    }

    public static DataParser Instance { get; private set; }
    [SerializeField] private string fileName = "Applicant-test.idf";
    public List<DataFrame> frames = new List<DataFrame>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        string path = $"{Application.dataPath}/Resources/{fileName}";

        StreamReader sr = new StreamReader(path);
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            print(line);
        }
    }
}
