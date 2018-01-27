using UnityEngine.UI;
using UnityEngine;

public class TextController : MonoBehaviour {
    public GameObject textPrefab;
    public AudioClip turn1;
    public AudioClip turn2;
    private AudioSource source;

    void Start() {
        source = GetComponent<AudioSource>();
    }

    void Player1Start() {
        Instantiate(textPrefab, transform.GetChild(0)).GetComponent<Text>().text = "Player 1";
        source.clip = turn1;
        source.Play();
    }
    void Player2Start() {
        Instantiate(textPrefab, transform.GetChild(0)).GetComponent<Text>().text = "Player 2";
        source.clip = turn2;
        source.Play();
    }
}
