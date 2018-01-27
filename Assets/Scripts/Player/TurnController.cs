using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour {
    public int TurnCount { get; private set; }
    public bool GameOver { get; private set; }
    public Infectable.Alignment Turn { get; private set; }

    public Material player1Material;
    public Material player2Material;
    public GameObject sling;
    public GameObject turnText;
    public GameObject endGamePrefab;
    public GameObject gameEnderPrefab;
    public int maxTurns = 10;

    private Renderer[] slingRenderer;

    void Start() {
        GameOver = false;
        turnText.GetComponent<Text>().text = "Turn " + (TurnCount + 1) + "/" + maxTurns;
        slingRenderer = sling.GetComponentsInChildren<Renderer>();
        foreach (var rdr in slingRenderer) {
            rdr.material = player1Material;
        }
        
        Turn = Infectable.Alignment.Player1;
        BroadcastMessage("Player1Start");
    }

    private void ChangeTurn() {
        if (!GameOver) {
            if (Turn == Infectable.Alignment.Player1) {
                Turn = Infectable.Alignment.Player2;
                BroadcastMessage("Player2Start");
                foreach (var rdr in slingRenderer) {
                    rdr.material = player2Material;
                }
            }
            else {
                ++TurnCount;
                if (TurnCount >= maxTurns) {
                    GameEnd();
                }
                else {
                    turnText.GetComponent<Text>().text = "Turn " + (TurnCount + 1) + "/" + maxTurns;
                    Turn = Infectable.Alignment.Player1;
                    BroadcastMessage("Player1Start");
                    foreach (var rdr in slingRenderer) {
                        rdr.material = player1Material;
                    }
                }
            }
        }
    }


    private void GameEnd() {
        var infectables = FindObjectsOfType<Infectable>();
        int score = 0;
        foreach (var infectable in infectables) {
            score += infectable.Score;
        }

        var text = "";
        if (score > 0) {
            text = "Player 1 Wins!";
        } else if (score < 0) {
            text = "Player 2 Wins!";
        } else {
            text = "Tie!";
        }

        Instantiate(endGamePrefab, transform.GetChild(3).GetChild(0)).GetComponent<Text>().text = text;
        Instantiate(gameEnderPrefab);
        GameOver = true;
    }
}
