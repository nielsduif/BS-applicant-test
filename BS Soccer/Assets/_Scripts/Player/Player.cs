using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player scriptable object with all player prefabs, being all field players, goalies and 
/// </summary>
[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player")]
public class Player : ScriptableObject
{
    public GameObject[] prefabs;
    public PlayerType playerType;
}

public enum PlayerType
{
    Goalkeeper,
    Fieldplayer,
    Referee
}