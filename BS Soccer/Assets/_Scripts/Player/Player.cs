using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player")]
public class Player : ScriptableObject
{
    public GameObject prefab;
    public PlayerType playerType;
    public Transform top, legs;
}

public enum PlayerType
{
    Goalkeeper,
    Fieldplayer
}
