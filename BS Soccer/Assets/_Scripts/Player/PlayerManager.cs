using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Player keeper, fieldplayer;
    public static List<Player> players { get; private set; } = new List<Player>();
    [SerializeField] Transform playerParent;

    private void Start()
    {
        foreach (DataParser.Person player in DataParser.Instance.frames[0].Persons)
        {
            Instantiate(fieldplayer.prefab, new Vector3(player.Position[0], player.Position[1], player.Position[2]), Quaternion.identity, playerParent);
        }
    }
}
