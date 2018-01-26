using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject targetObject;
    private Vector3 offset;

    void Start() {
        offset = transform.position - targetObject.transform.position;
    }

    void FixedUpdate() {
        transform.position = offset + targetObject.transform.position;
    }
}
