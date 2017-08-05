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
  public Text ui;

  void Start () {
    startTime = Time.time;
  }

  void Update () {

    // Wenn der Ball gerade geworfen wurde…
    if(isThrown) {
      throwTimer = Time.time - time;

      // …wird nach 1 Sekunde der Text angezeigt, dass der Gegner dran ist
      if(throwTimer >= 1.0f) {
        colliderTriggerScript.ui.text = "Opponent is throwing…";
      }

      // …wird nach 5 Sekunden der Wurf des Gegners simuliert und die Steuerung wieder freigegeben
      if(throwTimer >= 5.0f) {
        colliderTriggerScript.enemyThrow();
        time = Time.time;
        isThrown = false;
      }
    }

    // Wenn der Button geklickt wird und gerade nicht geworfen wurde, wird der Ball aufgehoben
    if (GvrController.ClickButton) {
      if (!(isThrown)) {
        PickUp();
  			pickedUp = true;
      }
		}

    // Wenn der Ball in der Hand ist und der Button losgelassen wird, wird geworfen
		if (!GvrController.ClickButton && pickedUp){
      ThrowBall();
      time = Time.time;
      isThrown = true;
			pickedUp = false;
		}
  }

  // Camera Shake
  void CameraShake() {
    shakeTimer = Time.time - startTime;

    // Wenn mind. 1 eigener Becher getroffen wurde, fängt die Kamera an zu wackeln
    if (colliderTriggerScript.pointsEnemy > 0) {
      if (shakeTimer >= shake) {
        startTime = Time.time;

        // Je höher die Punktezahl des Gegners, desto kürzer werden die Abstände zwischen zwei Wacklern
        shake = Random.Range(1, ((1/colliderTriggerScript.pointsEnemy) * 40));
        cameraShake.shakeDuration = 0.5f;
      }
    }
  }

  // Ball wird aufgehoben
  void PickUp() {
    rb = ball.GetComponent<Rigidbody>();
    rb.velocity = new Vector3(0f, 0f, 0f);
    Vector3 pos = pointer.transform.position;
    Quaternion rot = Quaternion.identity;
    ball.transform.SetPositionAndRotation (pos, rot);
  }

  // Ball wurd geworfen
  void ThrowBall () {
		rb = ball.GetComponent<Rigidbody>();
		force = ball.transform.forward * 0.6f;
		rb.AddForce(force);
  }
}
