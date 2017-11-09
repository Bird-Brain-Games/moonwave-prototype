using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text playerScorePrefab;

    int[] playerScores;
    public Color[] playerColours;
    Text[] playerScoreText;
    int numPlayers;
    PlayerManager manager;
    
    // Use this for initialization
    void Start () {
        manager = GameObject.Find("Players").GetComponent<PlayerManager>();   // COULD BE RISKY
        numPlayers = manager.GetNumPlayers();
        playerScores = manager.GetPlayerScores();
        playerColours = manager.GetPlayerColours();


        // Initialize the UI elements for displaying the score [Graham]
        playerScoreText = new Text[numPlayers];
        for (int i = 0; i < numPlayers; i++)
        {
            playerScoreText[i] = Instantiate(playerScorePrefab, transform);
            //playerScoreText[i].color = playerColours[i];  // Broken for some reason [Graham]
            playerScoreText[i].text = playerScores[i].ToString();
        }
        
	}
	
	// Late update is called once per frame, after the other updates. Used for UI. [Graham]
	void LateUpdate () {
        playerScores = manager.GetPlayerScores();

        // Update the player texts
        for (int i = 0; i < numPlayers; i++)
        {
            playerScoreText[i].text = playerScores[i].ToString();
        }
    }
}
