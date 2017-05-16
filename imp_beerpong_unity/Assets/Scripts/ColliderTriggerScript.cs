using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriggerScript : MonoBehaviour {

	int points;

	// Use this for initialization
	void Start () {
		points = 0;
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
		Debug.Log (points);
	}
}