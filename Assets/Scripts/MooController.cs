using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooController : MonoBehaviour {
    public AudioClip[] calmMoos;
    public float minDelay = 2;
    public float maxDelay = 10;
    public GameObject cow;
    private AudioSource source;
    
    void Start () {
        source = GetComponent<AudioSource>();
        StartCoroutine(Moo());
	}
	
    IEnumerator Moo() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            if (cow.GetComponent<CowController>().Attached) {
                source.clip = calmMoos[Random.Range(0, calmMoos.Length)];
                source.Play();
            }
        }
    }
}
