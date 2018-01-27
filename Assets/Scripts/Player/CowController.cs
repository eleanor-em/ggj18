using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CowController : MonoBehaviour {
    public GameObject ghostCowPrefab;
    public float minVelocity;
    public float translateAmount = 0.5f;
    public float pullbackAmount = 1.5f;
    public float pullbackSpeed = 0.1f;
    public float minPullbackRatio = 0.2f;
    public float maxZStretch = 0.9f;
    public float maxYStretch = 0.9f;
    public float stiffness = 100f;

    public float pullbackPeriod = 1;
    public float postHitDrag = 0.5f;
    public float killHeight = -5f;
    private float initialDrag;
    public Vector3 InitialPos { get { return initialPos; } }

    private Vector3 initialPos;
    private Rigidbody rb;
    private bool attached = true;
    private bool resettable = false;
    private TurnController turnController;
    
    private List<GameObject> player1GhostCows = new List<GameObject>();
    private List<GameObject> player2GhostCows = new List<GameObject>();

    void Start() {
        initialPos = transform.position;
        turnController = GetComponent<TurnController>();
        rb = GetComponent<Rigidbody>();
        initialDrag = rb.drag;
    }

    void OnCollisionEnter(Collision other) {
        var infectable = other.gameObject.GetComponent<Infectable>();
        if (infectable != null) {
            infectable.Infect(GetComponent<TurnController>().Turn);
        }

        rb.drag = postHitDrag;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            NextTurn();
        }

        if (attached) {
            float dz = Input.GetAxis("Horizontal"),
                  dy = Input.GetAxis("Vertical");
            // object is stuck in plane for simplicity
            transform.position += translateAmount * Time.deltaTime * (Vector3.forward * dz + Vector3.up * dy);

            transform.position = new Vector3(initialPos.x + Mathf.Abs(pullbackAmount * Mathf.Sin(Time.time * Mathf.PI * 2 / pullbackPeriod)),
                                             transform.position.y,
                                             transform.position.z);

            // check if we've just released the slingshot
            if (Input.GetButtonDown("Fire1")) {
                // make sure we pulled it back enough
                if (Vector3.Distance(initialPos, transform.position) > pullbackAmount * minPullbackRatio) {
                    // SPRING
                    transform.parent.BroadcastMessage("DetachCow", SendMessageOptions.DontRequireReceiver);

                    // hooke's law baby
                    var dx = transform.position - initialPos;
                    rb.AddForce(-stiffness * dx);
                }
            }
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
        } else {
            if (resettable && (rb.velocity.magnitude < minVelocity || transform.position.y < killHeight)) {
                NextTurn();
            }
        }
    }

    private void NextTurn() {
        SendMessage("ChangeTurn");
        transform.parent.BroadcastMessage("ResetCow", SendMessageOptions.DontRequireReceiver);
    }
    
    private void ResetCow() {
        transform.rotation = Quaternion.identity;
        transform.position = initialPos;

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.drag = initialDrag;

        attached = true;
        resettable = false;

        if (turnController.Turn == Infectable.Alignment.Player1) {
            foreach (var cow in player1GhostCows) {
                if (cow != null) {
                    cow.SetActive(true);
                }
            }
            foreach (var cow in player2GhostCows) {
                if (cow != null) {
                    cow.SetActive(false);
                }
            }
        } else {
            foreach (var cow in player2GhostCows) {
                if (cow != null) {
                    cow.SetActive(true);
                }
            }
            foreach (var cow in player1GhostCows) {
                if (cow != null) {
                    cow.SetActive(false);
                }
            }
        }
    }

    private void DetachCow() {
        attached = false;
        rb.useGravity = true;

        var model = GetComponentInChildren<Renderer>().transform;

        var list = (turnController.Turn == Infectable.Alignment.Player1) ? player1GhostCows : player2GhostCows;
        foreach (var cow in list) {
            if (cow != null) {
                cow.SendMessage("Fade");
            }
        }
        list.Add(Instantiate(ghostCowPrefab, transform.position + model.localPosition, Quaternion.identity));
        StartCoroutine(BecomeResettable());
    }

    private IEnumerator BecomeResettable() {
        yield return new WaitForSeconds(0.5f);
        resettable = true;
    }
}
