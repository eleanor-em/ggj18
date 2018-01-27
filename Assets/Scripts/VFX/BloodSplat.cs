using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplat : MonoBehaviour {
    public GameObject decalPrefab;
    public int maxImpacts = 3;
    public float offset = 0.15f;
    public float timeInterval = 0.5f;

    private int impacts = 0;
    private bool canSplat = true;

    void ResetCow() {
        impacts = 0;
    }


    void OnCollisionEnter(Collision other) {
        if (canSplat && impacts++ < maxImpacts) {
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
