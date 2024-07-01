using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player keeper, fieldplayer;
    [SerializeField] Transform playerParent;
    private List<GameObject> playerObjects = new List<GameObject>();
    private int currentFrame = 0;

    private void Start()
    {
        CreatePlayers();
    }

    private void CreatePlayers()
    {
        foreach (DataParser.Person person in DataParser.Instance.frames[0].Persons)
        {
            GameObject player = Instantiate(fieldplayer.prefab, new Vector3(person.Position[0], person.Position[1], person.Position[2]), Quaternion.identity, playerParent);
            player.name = $"Player {person.JerseyNumber}";
            playerObjects.Add(player);
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < playerObjects.Count - 1; i++)
        {
            DataParser.DataFrame frame = DataParser.Instance.frames[currentFrame];
            Vector3 newPosition = new Vector3(frame.Persons[i].Position[0], frame.Persons[i].Position[1], frame.Persons[i].Position[2]);
            playerObjects[i].transform.position = newPosition;
        }
        currentFrame++;
    }
}