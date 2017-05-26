using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControllerManagerScript : MonoBehaviour {

  public GameObject ball;
	public GameObject pointer;
	public Rigidbody rb;
	private bool pickedUp = false;
	public Vector3 force;
  public ColliderTriggerScript colliderTriggerScript;
  public float time;
  public float timer;
  public bool timerStarted;

  void Start () {
  }

  void Update () {

    colliderTriggerScript.score.text = "pickedUp: " + pickedUp + " timerStarted: " + timerStarted;

    if(timerStarted) {
      timer = Time.time - time;

      if(timer >= 5.0f) {
        colliderTriggerScript.enemyThrow();
        time = Time.time;
        timerStarted = false;
      }
    }

    if (GvrController.ClickButton) {
      if (!(timerStarted)) {
        PickUp();
  			pickedUp = true;
      }
		}

		if (!GvrController.ClickButton && pickedUp){
      ThrowBall();
      time = Time.time;
      timerStarted = true;
			pickedUp = false;
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
