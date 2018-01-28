using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float panSpeed = 0.1f;
    public float cowFollowSpeed = 2f;
    public GameObject targetObject;
    public Vector3 mapViewPosition;
    public Vector3 mapViewRotation;

    private Quaternion initialRotation;
    private Vector3 offset;

    private Vector3 targetPos;
    private Quaternion targetRot;

    private bool mapView = false;

    void Start() {
        offset = transform.position - targetObject.transform.position;
        initialRotation = transform.localRotation;
    }

    void Update() {
        if (Input.GetButtonDown("MapView")) {
            if (!mapView) {
                targetPos = mapViewPosition;
                targetRot = Quaternion.Euler(mapViewRotation);
            }
            mapView = !mapView;
        }
    }

    void FixedUpdate() {
        if (mapView) {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, panSpeed * Time.fixedDeltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, panSpeed * Time.fixedDeltaTime);
        } else {
            targetPos = offset + targetObject.transform.position;
            targetRot = initialRotation;
            transform.position = Vector3.Lerp(transform.position, targetPos, cowFollowSpeed * Time.fixedDeltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, cowFollowSpeed * Time.fixedDeltaTime);
        }
    }
}
