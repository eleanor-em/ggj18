using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingPieceController : MonoBehaviour {
    public Vector3 fixedPosDir;
    public GameObject thrownObject;
    public float stretchSpeed = 0.5f;
    public float bodgeFactor = 0.8f;

    private bool attached = true;
    private Vector3 initialPos;
    private Vector3 initialScale;
    private Vector3 stickRelativePos;
    private Vector3 objInitialPos;

    private Vector3 lastShotPos;

	void Start() {
        initialPos = transform.position;
        objInitialPos = thrownObject.transform.position;
        initialScale = transform.localScale;
        stickRelativePos = fixedPosDir * (initialScale.z / 2 - initialScale.x / 2);
    }
	
	void FixedUpdate() {
        if (attached) {
            var objPos = thrownObject.transform.position;
            var objDisp = objPos - objInitialPos;

            // object is stuck in the plane for simplicity
            var desiredYAngle = Mathf.Atan(objDisp.x / (objDisp.z + (objInitialPos.z - initialPos.z) * 2))
                              * Mathf.Rad2Deg;
            var desiredXAngle = -Mathf.Atan(objDisp.y / (objDisp.z + (objInitialPos.z - initialPos.z) * 2))
                              * Mathf.Rad2Deg;
            transform.position = initialPos + stickRelativePos;

            transform.rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
            transform.position += transform.rotation * -fixedPosDir * transform.localScale.z / 2;

            var targetScale = new Vector3(initialScale.x, initialScale.y, Vector3.Distance(initialPos + stickRelativePos, objPos)) * bodgeFactor;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, stretchSpeed);
        } else {
            // TEMPORARY
            transform.position = Vector3.Lerp(transform.position, initialPos, stretchSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, stretchSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, stretchSpeed);
        }
	}

    private void ResetCow() {
        attached = true;
    }
    private void DetachCow() {
        attached = false;
    }
}
