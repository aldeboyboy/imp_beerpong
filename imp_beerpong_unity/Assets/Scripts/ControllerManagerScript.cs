using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class ControllerManagerScript : MonoBehaviour {
 
    public GameObject ball;
	public GameObject pointer;

	public Rigidbody rb;

	private bool pickedUp = false;

	public Vector3 force;
 
    void Start () {
    }
 
    void Update () {
 
        if (GvrController.ClickButton) {
			PickUp();
			pickedUp = true;
		}
		if (!GvrController.ClickButton && pickedUp){
            ThrowBall ();
			pickedUp = false;
		}
 
    }
 
    void ThrowBall () {
		rb = ball.GetComponent<Rigidbody>();
		force = ball.transform.forward * 0.6f;
		rb.AddForce(force);
    }

	void PickUp() {
		rb = ball.GetComponent<Rigidbody>();
		rb.velocity = new Vector3(0f, 0f, 0f);
		Vector3 pos = pointer.transform.position;
		Quaternion rot = Quaternion.identity;
		ball.transform.SetPositionAndRotation (pos, rot);
	}
}