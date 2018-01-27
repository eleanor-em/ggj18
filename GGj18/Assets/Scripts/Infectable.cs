using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infectable : MonoBehaviour {
    public enum Alignment {
        None,
        Player1,
        Player2
    };

    public int maxInfectedAmount = 4;
    public GameObject pipPrefab;

    private int infectedAmount = 0;
    private Alignment alignment;

    public Vector3 pipOffset = Vector3.up;
    public float pipSeparation = 0.2f;
    private GameObject[] pips;

    void Start() {
        var size = pipPrefab.GetComponent<PipController>().finalScale;
        
        pips = new GameObject[maxInfectedAmount];
        for (int i = 0; i < maxInfectedAmount; ++i) {
            pips[i] = Instantiate(pipPrefab, transform);
            pips[i].transform.localPosition = pipOffset;

            // calculate position so the whole lot is centred
            // two cases -- odd vs even
            if (maxInfectedAmount % 2 == 0) {
                // even case
                pips[i].transform.position += Vector3.back * (pipSeparation / 2 + pipSeparation * (maxInfectedAmount / 2 - 1)
                                                            + size / 2 + size * (maxInfectedAmount / 2 - 1)
                                                            + i * (size + pipSeparation));
            } else {
                pips[i].transform.position += Vector3.back * ((size + pipSeparation) * (maxInfectedAmount / 2 + 1)
                                                             + i * (size + pipSeparation));
            }
        }
    }

    public void Infect(Alignment alignment = Alignment.Player1, int amount = 1) {
        if (this.alignment == Alignment.None) {
            this.alignment = alignment;
        }

        if (this.alignment == alignment) {
            for (int i = infectedAmount; i < maxInfectedAmount && i < infectedAmount + amount; ++i) {
                pips[i].SendMessage("Toggle");
            }
            infectedAmount += amount;
            if (infectedAmount >= maxInfectedAmount) {
                infectedAmount = maxInfectedAmount;
            }
        } else {
            for (int i = infectedAmount - 1; i >= 0 && i >= infectedAmount - amount; --i) {
                pips[i].SendMessage("Toggle");
            }
            infectedAmount -= amount;
            // change alignment
            if (infectedAmount < 0) {
                infectedAmount *= -1;
                this.alignment = alignment;
            }
            if (infectedAmount == 0) {
                this.alignment = Alignment.None;
            }
        }

        // update pips
        foreach (GameObject pip in pips) {
            pip.GetComponent<PipController>().SetAlignment(this.alignment);
        }
    }
}
