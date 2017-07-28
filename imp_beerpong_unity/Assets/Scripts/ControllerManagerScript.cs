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
  public float startTime;
  public float time;
  public float throwTimer;
  public bool isThrown;
  public float shake;
  public float shakeTimer;
  public CameraShake cameraShake;

  void Start () {
    if (colliderTriggerScript.pointsEnemy > 0) {
      shake = Random.Range(1, ((1/colliderTriggerScript.pointsEnemy) * 40));
    }
    startTime = Time.time;

  }

  void Update () {
    shakeTimer = Time.time - startTime;
    if (colliderTriggerScript.pointsEnemy > 0) {
      if (shakeTimer >= shake) {
        startTime = Time.time;
        shake = Random.Range(1, 4);;
        cameraShake.shakeDuration = 0.5f;
      }
    }

    // colliderTriggerScript.score.text = "pickedUp: " + pickedUp + " isThrown: " + isThrown;

    if(isThrown) {
      throwTimer = Time.time - time;

      if(throwTimer >= 5.0f) {
        colliderTriggerScript.enemyThrow();
        time = Time.time;
        isThrown = false;
      }
    }

    if (GvrController.ClickButton) {
      if (!(isThrown)) {
        PickUp();
  			pickedUp = true;
      }
		}

		if (!GvrController.ClickButton && pickedUp){
      ThrowBall();
      time = Time.time;
      isThrown = true;
			pickedUp = false;
		}
  }

  /* void startTimer(Time time) {
    float throwTimer = Time.time - time;
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
