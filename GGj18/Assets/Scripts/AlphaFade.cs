using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaFade : MonoBehaviour {
    public float alphaReduceSpeed = 0.05f;
    new private Renderer renderer;
    private float alpha = 1;

    void Start() {
        renderer = GetComponent<Renderer>();
    }

    void Update() {
        alpha -= alphaReduceSpeed * Time.deltaTime;
        renderer.material.color = new Color(1, 1, 1, alpha);

        if (alpha <= 0) {
            Destroy(gameObject);
        }
    }
}
