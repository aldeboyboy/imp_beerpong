using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderTriggerScript : MonoBehaviour {

	public int pointsEnemy;
	public int pointsPlayer;
	public Text ui;
	public ControllerManagerScript controllerManagerScript;
	public GameObject menu;
	public GameObject cups_red;
	public GameObject cups_blue;

	// Use this for initialization
	void Start () {
		startGame();
	}

	// Update is called once per frame
	void Update () {
		if (pointsPlayer > 9 || pointsEnemy > 9) {
			gameOver();
		}
	}

	void startGame() {
		menu.SetActive(false);
		ui.text = "It's your turn. Press button to pick up ball & release it to throw.";
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
	}

	public void enemyThrow() {
		ui.text = "Enemy is throwing…";
		float p = Random.value;
		if (p < 0.4) {
			DestroyCup();
		}
		ui.text = "It's your turn. Press button to pick up ball & release it to throw.";
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
