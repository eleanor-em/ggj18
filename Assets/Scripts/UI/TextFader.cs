using UnityEngine;
using UnityEngine.UI;

public class TextFader : MonoBehaviour {
    public float fadeSpeed = 0.05f;
    public float timeToFade = 2f;

    private Text text;
    private float alpha = 1;

    private float time = 0;
	
    void Start() {
        text = GetComponent<Text>();
    }

	void Update () {
        time += Time.deltaTime;
        if (time > timeToFade) {
            alpha -= fadeSpeed * Time.deltaTime;
            if (alpha < 0) {
                Destroy(gameObject);
            }

            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        }
	}
}
