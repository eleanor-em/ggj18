using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
    public AudioSource source;
    public float minDelay = 20;
    public float maxDelay = 30;
	void Start () {
        source = GetComponent<AudioSource>();
	}

    IEnumerator Music() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            source.Play();
        }
    }
}
