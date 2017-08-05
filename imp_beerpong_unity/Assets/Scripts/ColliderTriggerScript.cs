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
		// Wenn ein Spieler alle Becher des Gegners vernichtet hat, ist das Spiel vorbei
		if (pointsPlayer > 9 || pointsEnemy > 9) {
			gameOver();
		}
	}

	// Initialisiert das Spiel bei Start bzw. Neustart
	void startGame() {
		menu.SetActive(false);
		ui.text = "It's your turn. Press button to pick up ball & release it to throw.";

		// Punktestände werden zurückgesetzt
		pointsPlayer = 0;
		pointsEnemy = 0;

		// Verbleibende Becher werden zerstört (bei Neustart nötig)
		Destroy(GameObject.Find("cups_red"));
		Destroy(GameObject.Find("cups_blue"));

		// Neue Becher werden erstellt
		Object cupObject_red = Instantiate(cups_red, new Vector3(-0.15f, 0.2f, 1.0f), Quaternion.identity);
		Object cupObject_blue = Instantiate(cups_blue, new Vector3(0.15f, 0.2f, -1.0f), Quaternion.Euler(Vector3.up * 180));
		cupObject_blue.name = "cups_blue";
		cupObject_red.name = "cups_red";
	}

	// Wenn der eigene Wurf ein Treffer war...
	void OnTriggerEnter(Collider collider) {

		// Collider im Becher
		GameObject colliderGameObject = collider.gameObject;

		// Collider sein Vater
		GameObject cup = colliderGameObject.transform.parent.gameObject;

		// Becher und dazugehöriger Collider werden entfernt
		Destroy (cup);
		Destroy (collider);

		// Eigener Punktestand wird erhöht
		pointsPlayer += 1;
	}


	// Wurf des Gegners wird simuliert
	public void enemyThrow() {
		float p = Random.value;

		// Wahrscheinlichkeit, dass der Gegner überhaupt einen Becher trifft, liegt bei 40%.
		// Basiert natürlich auf Science ;)
		// Quelle: http://www.askmen.com/news/entertainment/the-real-odds-of-making-a-beer-pong-shot.html
		if (p < 0.4) {
			DestroyCup();
		}

		ui.text = "It's your turn. Press button to pick up ball & release it to throw.";
	}

	// Bei einem Treffer des Gegners wird ein Becher entfernt
	void DestroyCup() {

		// Zufälliger Becher wird ausgewählt
		int cupNumber = Random.Range (0, 10);
		GameObject cupPlayer = GameObject.Find ("cup_blue_" + cupNumber);

		// Wenn der ausgewählte Becher existiert, wird er entfernt und der Punktestand des Gegners erhöht
		if (cupPlayer != null) {
			Destroy (cupPlayer);
			pointsEnemy += 1;
		}
	}

	// Wenn das Spiel vorbei ist…
	void gameOver() {
		// Game Over Menü wird eingeblendet
		menu.SetActive(true);

		// Mit einem ButtonClick kann ein neues Spiel gestartet werden
		if (GvrController.ClickButton) {
      startGame();
		}
	}
}
