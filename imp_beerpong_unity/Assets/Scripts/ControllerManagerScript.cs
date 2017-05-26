using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControllerManagerScript : MonoBehaviour {

  public GameObject ball;
	public GameObject pointer;
	public Rigidbody rb;
	private bool pickedUp = false;
	public Vector3 force;
  public ColliderTriggerScript script;
  public float time;
  public float timer;
  public bool timerStarted;

  void Start () {
  }

  void Update () {

    if(timerStarted) {
      timer = Time.time - time;
    }
    if (GvrController.ClickButton) {
			PickUp();
			pickedUp = true;
		}

		if (!GvrController.ClickButton && pickedUp){
      ThrowBall();
      time = Time.time;
      timerStarted = true;
			pickedUp = false;
		}

    if(timer >= 10.0f) {
      script.enemyThrow();
      timer = 0.0f;
    }
  }

  /* void startTimer(Time time) {
    float timer = Time.time - time;
  } */

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
