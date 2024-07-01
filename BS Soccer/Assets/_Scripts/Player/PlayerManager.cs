using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Contols the spawning, movement and rotation of all players
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player[] playerTypes;
    [SerializeField] Transform playerParent;
    private Dictionary<int, PlayerDataDisplay> playerID = new Dictionary<int, PlayerDataDisplay>();
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
            PlayerDataDisplay playerDataDisplay = player.GetComponent<PlayerDataDisplay>();
            playerDataDisplay.SetJerseyNumber(person.JerseyNumber);
            playerID.Add(person.Id, playerDataDisplay);
        }
    }

    private void FixedUpdate()
    {
        if (FramesAvailable())
        {
            SetTransforms();
            currentFrame++;
        }
    }

    private void SetTransforms()
    {
        for (int i = 0; i < playerID.Count - 1; i++)
        {
            DataParser.DataFrame frame = DataParser.Instance.frames[currentFrame];
            PlayerDataDisplay player = playerID[frame.Persons[i].Id];

            Vector3 newPosition = Util.Float3ToVector(frame.Persons[i].Position);
            player.transform.position = newPosition;

            Vector3 lookDir = Ball.Instance.transform.position - player.transform.position;
            lookDir.y = 0; // freezes looking up and down
            if (lookDir != Vector3.zero)
            {
                player.transform.localRotation = Quaternion.LookRotation(lookDir);
            }

            player.UpdateSpeedText(frame.Persons[i].Speed);
        }
    }

    private bool FramesAvailable()
    {
        return (currentFrame < DataParser.Instance.frames.Count);
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