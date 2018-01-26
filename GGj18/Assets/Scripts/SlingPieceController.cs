using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingPieceController : MonoBehaviour {
    public Vector3 fixedPosDir;
    public GameObject thrownObject;
    public float stretchSpeed = 0.5f;

    private bool attached = true;
    private Vector3 initialPos;
    private Vector3 initialScale;
    private Vector3 stickRelativePos;

	void Start() {
        initialPos = transform.position;
        initialScale = transform.localScale;
        stickRelativePos = fixedPosDir * (initialScale.z / 2 - initialScale.x / 2);
    }
	
	void FixedUpdate() {
        if (attached) {
            var objPos = thrownObject.transform.position;
            var objDisp = objPos - thrownObject.GetComponent<CowController>().InitialPos;

            // object is stuck in the plane for simplicity
            // bad hack, sorry
            var desiredYAngle = Mathf.Asin(objDisp.x / transform.localScale.z)
                             * -Mathf.Sign(stickRelativePos.z)
                             * Mathf.Rad2Deg;
            var desiredXAngle = Mathf.Atan(objDisp.y / transform.localScale.z)
                             * Mathf.Sign(stickRelativePos.z)
                             * Mathf.Rad2Deg;
            transform.position = initialPos + stickRelativePos;

            transform.rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
            transform.position += transform.rotation * -fixedPosDir * transform.localScale.z / 2;

            var targetScale = new Vector3(initialScale.x, initialScale.y, Vector3.Distance(initialPos + stickRelativePos, objPos));
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
