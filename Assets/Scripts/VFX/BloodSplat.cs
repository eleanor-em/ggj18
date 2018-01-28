using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplat : MonoBehaviour {
    public GameObject decalPrefab;
    public int maxImpacts = 3;
    public float offset = 0.15f;
    public float timeInterval = 0.5f;
    public AudioClip splat;
    public float minMooDelay = 1;
    public float maxMooDelay = 4;
    public AudioClip[] distressedMoos;
    public GameObject gibletPrefab;

    private AudioSource source;
    private int impacts = 0;
    private bool canSplat = true;

    void Start() {
        source = GetComponent<AudioSource>();
    }

    void ResetCow() {
        impacts = 0;
    }

    IEnumerator Moo() {
        yield return new WaitForSeconds(Random.Range(minMooDelay, maxMooDelay));
        if (impacts == 0) {
            source.clip = distressedMoos[Random.Range(0, distressedMoos.Length)];
            source.Play();
        }
    }

    void DetachCow() {
        StartCoroutine(Moo());
    }

    void OnCollisionEnter(Collision other) {
        if (impacts == 0) {
            Instantiate(gibletPrefab).transform.position = transform.position;
        }

        if (canSplat && impacts++ < maxImpacts) {
            source.clip = splat;
            source.Play();

            var decal = Instantiate(decalPrefab);
            var contact = other.contacts[0];
            decal.transform.position = contact.point;
            decal.transform.forward = -contact.normal;
            decal.transform.position -= decal.transform.forward * offset;

            StartCoroutine(SplatTimer());
        }   
    }

    IEnumerator SplatTimer() {
        canSplat = false;
        yield return new WaitForSeconds(timeInterval);
        canSplat = true;
    }
}
