using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {
    public GameObject pestilence;
    public GameObject pixieDust;
    public float stillRate = 1f;
    public float flyingRate = 5.75f;

    private ParticleSystem particles;

    void Player1Start() {
        pixieDust.transform.parent.gameObject.SetActive(false);
        pestilence.transform.parent.gameObject.SetActive(true);
        particles = pestilence.GetComponent<ParticleSystem>();
    }

    void Player2Start() {
        pixieDust.transform.parent.gameObject.SetActive(true);
        pestilence.transform.parent.gameObject.SetActive(false);
        particles = pixieDust.GetComponent<ParticleSystem>();
    }

    void ResetCow() {
        var em = particles.emission;
        em.rateOverTime = stillRate;
    }

    void DetachCow() {
        var em = particles.emission;
        em.rateOverTime = flyingRate;
    }
}
