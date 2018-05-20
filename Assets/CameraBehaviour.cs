using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	public GameObject target;
	// Use this for initialization
	void Start () {
		target = GameObject.Find("Rocket");
	}

	// Update is called once per frame
	void Update () {
		transform.LookAt(target.transform.position, target.GetComponent<RocketBehaviour>().getRocketOrientation());
	}
}
