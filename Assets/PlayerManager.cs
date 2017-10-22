using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    PlayerStats[] players;
    int[] playerScores;
    Color[] playerColours;
    int numPlayers;

	// Use this for initialization
	void Start () {
        players = GetComponentsInChildren<PlayerStats>();
        numPlayers = players.Length;
        playerScores = new int[numPlayers];
        playerColours = new Color[numPlayers];

        // Get the player colours [Graham]
        for (int i = 0; i < numPlayers; i++)
        {
            playerColours[i] = players[i].colour;
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Update the score counters with the player scores [Graham]
        for (int i = 0; i < numPlayers; i++)
        {
            playerScores[i] = players[i].getScore();
        }
	}

    /// <summary>
    /// Get the number of players [Graham]
    /// </summary>
    /// <returns></returns>
    public int GetNumPlayers()
    {
        return numPlayers;
    }

    /// <summary>
    /// Get the array of player scores [Graham]
    /// </summary>
    /// <returns></returns>
    public int[] GetPlayerScores()
    {
        return playerScores;
    }

    /// <summary>
    /// Get the array of player colours [Graham]
    /// </summary>
    /// <returns></returns>
    public Color[] GetPlayerColours()
    {
        return playerColours;
    }
}
