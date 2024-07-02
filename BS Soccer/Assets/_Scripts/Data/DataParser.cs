using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Script that filters out all variables from the raw data file, creating classes with the specific variables stored inside of them
/// Used https://jsoneditoronline.org/ to easily show all the stored content
/// </summary>
public class DataParser : Singleton<DataParser>
{
    /// <summary>
    /// Basic classes holding all the values provided in the rawdata
    /// </summary>
    [System.Serializable]
    public class Person
    {
        public int Id;
        public float Timestamp;
        public float[] Position;                //cant use vector3 since it can't convert
        public float Speed;
        public int TeamSide;
        public int JerseyNumber;
        public int AnimationContext;
        public PersonContext PersonContext;
    }
   
    [System.Serializable]
    public class PersonContext
    {
        public float? MovementOrientation;      //only type of optional data
        public bool HasBallPosession;
        public int PlayerState;
    }
    
    [System.Serializable]
    public class Ball
    {
        public int ID;
        public float TimeStamp;
        public float[] Position;
        public float Speed;
        public int TeamSide;
        public int JerseyNumber;
        public TrackableBallContext TrackableBallContext;
    }
    
    [System.Serializable]
    public class TrackableBallContext
    {
        public int BallState;
        public int Possesion;
    }
   
    [System.Serializable]
    public class GameClockContext
    {
        public int Period;
        public int Minute;
        public int Second;
        public int InjuryTime;
    }
    
    [System.Serializable]
    public class MatchScoreContext
    {
        public int HomeSore;
        public int AwayScore;
    }
   
    /// <summary>
    /// Class that holds all information of a frame, with the connection to the underlaying classes
    /// </summary>
    [System.Serializable]
    public class DataFrame
    {
        public int FrameCount;
        public float TimestampUTC;
        public List<Person> Persons = new List<Person>();
        public Ball Ball;
        public GameClockContext GameClockContext;
        public MatchScoreContext MatchScoreContext;
    }
    
    [SerializeField] private string fileName = "Applicant-test.idf";
    [HideInInspector] public List<DataFrame> frames { get; private set; } = new List<DataFrame>();

    /// <summary>
    /// Singleton, making sure only one instance is present
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        LoadData();
    }

    /// <summary>
    /// Loads the file based on location and adds the values for each frame to a list
    /// </summary>
    private void LoadData()
    {
        string path = $"{Application.dataPath}/StreamingAssets/{fileName}";

        StreamReader sr = new StreamReader(path);
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            DataFrame frame = JsonUtility.FromJson<DataFrame>(line);
            frames.Add(frame);
        }
        sr.Close();
    }
}