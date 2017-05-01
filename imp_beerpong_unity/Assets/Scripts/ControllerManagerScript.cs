using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class ControllerManagerScript : MonoBehaviour {
 
    public GameObject ball;
	public GameObject pointer;

	private bool pickedUp = false;
 
    void Start () {
    }
 
    void Update () {
 
        if (GvrController.ClickButton) {
			BallInHand();
			pickedUp = true;
		}
		if (!GvrController.ClickButton && pickedUp){
            ThrowBall ();
			pickedUp = false;
		}
 
    }
 
    void ThrowBall () {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
		rb.AddForce(ball.transform.forward * 500);
    }

	void BallInHand() {
		Vector3 pos = pointer.transform.position;
		Quaternion rot = pointer.transform.rotation;
		ball.transform.SetPositionAndRotation (pos, rot);
	}
}