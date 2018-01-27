using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour {
    public int TurnCount { get; private set; }
    public Infectable.Alignment Turn { get; private set; }

    public Material player1Material;
    public Material player2Material;
    public GameObject sling;

    private Renderer[] slingRenderer;

    void Start() {
        slingRenderer = sling.GetComponentsInChildren<Renderer>();
        foreach (var rdr in slingRenderer) {
            rdr.material = player1Material;
        }
        
        Turn = Infectable.Alignment.Player1;
        BroadcastMessage("Player1Start");
    }

    private void ChangeTurn() {
        if (Turn == Infectable.Alignment.Player1) {
            Turn = Infectable.Alignment.Player2;
            BroadcastMessage("Player2Start");
            foreach (var rdr in slingRenderer) {
                rdr.material = player2Material;
            }
        } else {
            Turn = Infectable.Alignment.Player1;
            BroadcastMessage("Player1Start");
            foreach (var rdr in slingRenderer) {
                rdr.material = player1Material;
            }
        }
        ++TurnCount;
    }
}
