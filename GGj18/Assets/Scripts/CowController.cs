using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CowController : MonoBehaviour {
    public GameObject ghostCowPrefab;
    public float translateAmount = 0.5f;
    public float pullbackAmount = 1.5f;
    public float pullbackSpeed = 0.1f;
    public float minPullbackRatio = 0.2f;
    public float maxZStretch = 0.9f;
    public float maxYStretch = 0.9f;
    public float stiffness = 100f;

    public Vector3 InitialPos { get { return initialPos; } }

    private Vector3 initialPos;
    private Vector3 targetPos;
    private Rigidbody rb;
    private bool attached = true;

    private List<GameObject> ghostCows = new List<GameObject>();

    void Start() {
        initialPos = transform.position;
        targetPos = initialPos;
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other) {
        var infectable = other.gameObject.GetComponent<Infectable>();
        if (infectable != null) {
            Debug.Log("hit!");
            infectable.Infect();
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            transform.parent.BroadcastMessage("ResetCow", SendMessageOptions.DontRequireReceiver);
        }
    }

	void FixedUpdate () {
        // Temporary buttons
        float dz = Input.GetAxis("Horizontal"),
              dy = Input.GetAxis("Vertical");
        // object is stuck in plane for simplicity
        transform.position += translateAmount * Time.fixedDeltaTime * (Vector3.forward * dz + Vector3.up * dy);

        if (Input.GetButton("Fire1")) {
            // find the pullback vector
            targetPos = transform.position;
            // reset the x coordinate
            targetPos.x = initialPos.x;
            targetPos += Vector3.right * pullbackAmount;
        } else {
            targetPos = initialPos;
        }

        // check if we've just released the slingshot
        if (attached && Input.GetButtonUp("Fire1")) {
            // make sure we pulled it back enough
            if (Vector3.Distance(initialPos, transform.position) > pullbackAmount * minPullbackRatio) {
                // SPRING
                transform.parent.BroadcastMessage("DetachCow", SendMessageOptions.DontRequireReceiver);

                // hooke's law baby
                var dx = transform.position - initialPos;
                rb.AddForce(-stiffness * dx);
            }
        }

        if (attached) {
            // clamp position
            if (transform.position.z - initialPos.z > maxZStretch) {
                transform.position = new Vector3(transform.position.x, transform.position.y, initialPos.z + maxZStretch);
            }
            if (transform.position.z - initialPos.z < -maxZStretch) {
                transform.position = new Vector3(transform.position.x, transform.position.y, initialPos.z - maxZStretch);
            }
            if (transform.position.y - initialPos.y > maxYStretch) {
                transform.position = new Vector3(transform.position.x, initialPos.y + maxYStretch, transform.position.z);
            }
            if (transform.position.y - initialPos.y < -maxYStretch) {
                transform.position = new Vector3(transform.position.x, initialPos.y - maxYStretch, transform.position.z);
            }

            // e l a s t i c i t y
            transform.position = Vector3.Lerp(transform.position, targetPos, pullbackSpeed * Time.fixedDeltaTime);
        }
	}
    
    private void ResetCow() {
        transform.rotation = Quaternion.identity;
        transform.position = initialPos;
        rb.useGravity = false;
        attached = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void DetachCow() {
        attached = false;
        rb.useGravity = true;

        foreach (var cow in ghostCows) {
            if (cow != null) {
                cow.SendMessage("Fade");
            }
        }
        ghostCows.Add(Instantiate(ghostCowPrefab, transform.position, Quaternion.identity));
    }
}
