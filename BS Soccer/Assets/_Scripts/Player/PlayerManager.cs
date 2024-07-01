using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player[] playerTypes;
    [SerializeField] Transform playerParent;
    private Dictionary<int, GameObject> playerID = new Dictionary<int, GameObject>();
    [SerializeField] private int[] goalKeeperID = { 1, 124 };
    private int currentFrame = 0;

    private void Start()
    {
        CreatePlayers();
    }

    private void CreatePlayers()
    {
        foreach (DataParser.Person person in DataParser.Instance.frames[0].Persons)
        {
            GameObject player = Instantiate(PickPersonAsset(person), Util.Float3ToVector(person.Position), Quaternion.identity, playerParent);
            player.name = $"Player {person.JerseyNumber}";
            player.AddComponent<PlayerData>().data = person;
            playerID.Add(person.Id, player);
        }
    }

    private void FixedUpdate()
    {
        if (currentFrame < DataParser.Instance.frames.Count)
        {
            for (int i = 0; i < playerID.Count - 1; i++)
            {
                DataParser.DataFrame frame = DataParser.Instance.frames[currentFrame];
                Vector3 newPosition = Util.Float3ToVector(frame.Persons[i].Position);
                playerID[frame.Persons[i].Id].transform.position = newPosition;
            }
        }
        currentFrame++;
    }

    /// <summary>
    /// Picks the correct linked prefab based on the scriptable object type
    /// playerTypes[0] is assigned to refs
    /// playerTypes[1] is assigned to field players
    /// playerTypes[2] is assigned to goalies
    /// the prefabs[n] is the teamside, either 0 or 1 since there is no team for refs
    /// </summary>
    /// <param name="_person"></param>
    /// <returns></returns>
    private GameObject PickPersonAsset(DataParser.Person _person)
    {
        if (IsGoalkeeper(_person.Id))
        {
            return playerTypes[1].prefabs[_person.TeamSide - 1];
        }
        else
        {
            return playerTypes[_person.TeamSide > 0 ? 2 : 0].prefabs[_person.TeamSide > 0 ? _person.TeamSide - 1 : 0];
        }
    }

    private bool IsGoalkeeper(int _id)
    {
        return goalKeeperID.Contains(_id);
    }
}