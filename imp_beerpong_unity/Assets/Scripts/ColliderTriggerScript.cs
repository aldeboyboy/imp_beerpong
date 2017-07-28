using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderTriggerScript : MonoBehaviour {

	public int pointsEnemy;
	public int pointsPlayer;
	public Text score;
	public ControllerManagerScript controllerManagerScript;
	public GameObject menu;
	public Image overlay;
	public GameObject cups_red;
	public GameObject cups_blue;

	// Use this for initialization
	void Start () {
		startGame();
		enemyThrow();
	}

	// Update is called once per frame
	void Update () {
		if (pointsPlayer > 9 || pointsEnemy > 9) {
			gameOver();
		}
		//score.text = controllerManagerScript.throwTimer.ToString();
	}

	void startGame() {
		menu.SetActive(false);
		pointsPlayer = 0;
		pointsEnemy = 0;
		Destroy(GameObject.Find("cups_red"));
		Destroy(GameObject.Find("cups_blue"));
		Object cupObject_red = Instantiate(cups_red, new Vector3(-0.15f, 0.2f, 1.0f), Quaternion.identity);
		Object cupObject_blue = Instantiate(cups_blue, new Vector3(0.15f, 0.2f, -1.0f), Quaternion.Euler(Vector3.up * 180));
		cupObject_blue.name = "cups_blue";
		cupObject_red.name = "cups_red";
	}

	void OnTriggerEnter(Collider collider) {
		GameObject colliderGameObject = collider.gameObject;
		GameObject cup = colliderGameObject.transform.parent.gameObject;
		Destroy (cup);
		Destroy (collider);
		pointsPlayer += 1;
		//score.text = controllerManagerScript.throwTimer.ToString();
		// Debug.Log (pointsPlayer);
	}

	public void enemyThrow() {
		//score.text = "Test";
		overlay.CrossFadeAlpha(255.0f, 1.0f, false);
		float p = Random.value;
		if (p < 0.4) {
			DestroyCup();
		}
	}

	void DestroyCup() {
		int cupNumber = Random.Range (0, 10);
		GameObject cupPlayer = GameObject.Find ("cup_blue_" + cupNumber);
		if (cupPlayer != null) {
			Destroy (cupPlayer);
			pointsEnemy += 1;
		}
	}

	void gameOver() {
		menu.SetActive(true);
		if (GvrController.ClickButton) {
      startGame();
		}
	}
}
