using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float rotSpeed = 0.1f;
    public GameObject targetObject;

    private Vector3 initialForward;
    private Vector3 offset;
    private Rigidbody targetRb;

    void Start() {
        initialForward = transform.forward;
        offset = transform.position - targetObject.transform.position;
        targetRb = targetObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        transform.position = offset + targetObject.transform.position;
        if (targetRb.velocity.sqrMagnitude > 0) {
            //transform.forward = Vector3.Lerp(transform.forward, targetRb.velocity.normalized, rotSpeed);
        } else {
            transform.forward = Vector3.Lerp(transform.forward, initialForward, rotSpeed);
        }
    }
}
