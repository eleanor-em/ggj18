using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimator : MonoBehaviour {
    public float period = 1f;

    private float time = 0;
    private RectTransform rect;

    void Start() {
        rect = GetComponent<RectTransform>();
        rect.localScale = Vector3.zero;
    }

    private float Signal(float x) {
        var z = 10 * x / period;
        return 1 / (1 + Mathf.Exp(2 * Mathf.PI - z)) + Mathf.Exp(-(z - 2 * Mathf.PI) * (z - 2 * Mathf.PI));
    }

    void Update() {
        time += Time.deltaTime;
        rect.localScale = Vector3.one * Signal(time);
    }
}
