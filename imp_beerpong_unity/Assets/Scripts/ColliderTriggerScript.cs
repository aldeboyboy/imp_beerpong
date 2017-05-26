﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderTriggerScript : MonoBehaviour {

	int pointsRed;
	int pointsBlue;
	public Text score;
	public Image overlay;

	// Use this for initialization
	void Start () {
		pointsBlue = 0;
		pointsRed = 0;
		score.text = "Score: " + pointsBlue.ToString();
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider collider) {
		GameObject colliderGameObject = collider.gameObject;
		GameObject cup = colliderGameObject.transform.parent.gameObject;
		Destroy (cup);
		Destroy (collider);
		pointsBlue += 1;
		score.text = "Score: " + pointsBlue.ToString();
		Debug.Log (pointsBlue);
	}

	public void enemyThrow() {
		overlay.CrossFadeAlpha(255.0f, 1.0f, false);
		float p = Random.value;
		if (p < 0.1) {
			DestroyCup();
		}
	}

	void DestroyCup() {
		pointsRed += 1;
		int cupNumber = Random.Range (0, 10);
		GameObject cupBlue = GameObject.Find ("cup_blue_" + cupNumber);
		Destroy (cupBlue);
	}
}
