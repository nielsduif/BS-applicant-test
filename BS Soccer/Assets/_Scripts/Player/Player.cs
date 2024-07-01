using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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