using UnityEngine;

public class PipController : MonoBehaviour {
    public float finalScale = 0.5f;
    private float initialScale;
    private float targetScale;

    public float shiftSpeed = 0.1f;
    public float inactiveAlpha = 0.5f;
    private float targetAlpha = 0.5f;
    private float alpha = 0.5f;

    public Material player1Material;
    public Material player2Material;
    private Material initialMaterial;

    private bool active = false;

    new private Renderer renderer;

    void Start() {
        initialScale = transform.localScale.x;
        targetScale = initialScale;

        renderer = GetComponent<Renderer>();
        initialMaterial = renderer.material;
    }

    public void SetAlignment(Infectable.Alignment alignment) {
        switch (alignment) {
            case Infectable.Alignment.None:
                renderer.material = initialMaterial;
                break;
            case Infectable.Alignment.Player1:
                renderer.material = player1Material;
                break;
            case Infectable.Alignment.Player2:
                renderer.material = player2Material;
                break;
        }
    }

    void Update() {
        alpha = Mathf.Lerp(alpha, targetAlpha, shiftSpeed * Time.deltaTime);
        var col = renderer.material.color;
        renderer.material.color = new Color(col.r, col.g, col.b, alpha);

        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * targetScale, shiftSpeed * Time.deltaTime);
    }

    private void Toggle() {
        if (active) {
            targetScale = initialScale;
            active = false;
            targetAlpha = inactiveAlpha;
        } else {
            targetScale = finalScale;
            active = true;
            targetAlpha = 1;
        }
    }
}
