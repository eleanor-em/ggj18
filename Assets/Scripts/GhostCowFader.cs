using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCowFader : MonoBehaviour {
    public float alphaStepSize = 0.05f;
    public float alpha = 0.3f;

    private Material material;

    void Start() {
        material = GetComponent<Renderer>().material;
    }

    private void Fade() {
        alpha -= alphaStepSize;
        if (alpha <= 0) {
            Destroy(gameObject);
        } else {
            material.color = new Color(1, 1, 1, alpha);
        }
    }
}
