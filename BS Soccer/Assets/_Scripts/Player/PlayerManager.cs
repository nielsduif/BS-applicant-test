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
    private Dictionary<int, PlayerDataDisplay> playerID = new Dictionary<int, PlayerDataDisplay>(); //id connected to player object
    [SerializeField] private int[] goalKeeperID = { 1, 124 };

    private void Start()
    {
        CreatePlayers();
    }

    /// <summary>
    /// Instantiates prefab gameobjects for all persons in the dataset
    /// Adjust their name accordingly with the jerseynumber
    /// Adds the instantiated object to a dictionary
    /// </summary>
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
        if (FrameManager.Instance.FramesAvailable())
        {
            SetTransforms();
        }
    }

    /// <summary>
    /// Applies the transform data to the matched player based on the id in the dictionary
    /// Also makes the model rotate towards the ball position
    /// Sets the speedtext of the player to the one from the data
    /// </summary>
    private void SetTransforms()
    {
        for (int i = 0; i < playerID.Count - 1; i++)
        {
            DataParser.DataFrame frame = DataParser.Instance.frames[FrameManager.Instance.CurrentFrame];
            PlayerDataDisplay player = playerID[frame.Persons[i].Id]; // get the correct player based upon the data id instead of index of a list

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

    /// <summary>
    /// Picks the correct linked prefab based on the scriptable object type
    /// playerTypes[0] is assigned to refs
    /// playerTypes[1] is assigned to field players
    /// playerTypes[2] is assigned to goalies
    /// the prefabs[n] is the teamside, either 0 or 1 since there is no team for refs
    /// </summary>
    /// <param name="_person">Person data from the raw dataset</param>
    /// <returns></returns>
    private GameObject PickPersonAsset(DataParser.Person _person)
    {
        if (IsGoalkeeper(_person.Id))   //manual check for goalkeepers on ids, to assign the correct prefab
        {
            return playerTypes[1].prefabs[_person.TeamSide - 1];
        }
        else
        {
            return playerTypes[_person.TeamSide > 0 ? 2 : 0].prefabs[_person.TeamSide > 0 ? _person.TeamSide - 1 : 0];
        }
    }

    /// <summary>
    /// Compares the given id with the set goalies ids if the player indeed is a goalie
    /// </summary>
    /// <param name="_id">id to match with keepers id</param>
    /// <returns></returns>
    private bool IsGoalkeeper(int _id)
    {
        return goalKeeperID.Contains(_id);
    }
}