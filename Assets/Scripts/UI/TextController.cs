using UnityEngine.UI;
using UnityEngine;

public class TextController : MonoBehaviour {
    public GameObject textPrefab;

    void Player1Start() {
        Instantiate(textPrefab, transform.GetChild(0)).GetComponent<Text>().text = "Player 1";
    }
    void Player2Start() {
        Instantiate(textPrefab, transform.GetChild(0)).GetComponent<Text>().text = "Player 2";
    }
}
