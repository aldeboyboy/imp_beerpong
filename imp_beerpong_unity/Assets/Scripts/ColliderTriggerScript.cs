using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderTriggerScript : MonoBehaviour {

	int points;
	public Text score;

	// Use this for initialization
	void Start () {
		points = 0;
		score.text = "Score: " + points.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider collider) {
		GameObject colliderGameObject = collider.gameObject;
		GameObject cup = colliderGameObject.transform.parent.gameObject;
		Destroy (cup);
		Destroy (collider);
		points += 1;
		score.text = "Score: " + points.ToString();
		Debug.Log (points);
	}
}