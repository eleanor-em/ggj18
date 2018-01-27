using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    public AudioClip stretch1;
    public AudioClip stretch2;
    public AudioClip twang;

    private AudioSource source;

    void Start () {
        source = GetComponent<AudioSource>();
	}

    void Player1Start() {
    }
    void Player2Start() {
    }
    void Stretch() {
        if (Random.Range(0, 2) == 0) {
            source.clip = stretch1;
        } else {
            source.clip = stretch2;
        }
        source.Play();
    }
    void DetachCow() {
        source.clip = twang;
        source.Play();
    }
}
