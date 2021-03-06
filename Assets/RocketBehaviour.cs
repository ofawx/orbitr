﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour {

	public GameObject moon;
	Rigidbody moonRB;
	Rigidbody selfRB;
	Transform nozzle;
	GameObject plume;

	// Use this for initialization
	void Start () {
		moon = GameObject.Find("Moon");
		moonRB = moon.GetComponent<Rigidbody>();
		selfRB = GetComponent<Rigidbody>();
		nozzle = transform.Find("Nozzle");
		plume = GameObject.Find("Plume");
	}

	// Update is called once per frame
	void Update () {
	}

	// Physics Update
	void FixedUpdate () {
		// Gravitational Force
		Vector3 displacement = moonRB.position - selfRB.position;
		float distance = displacement.magnitude;
		float force = 6.674e-5f * moonRB.mass * selfRB.mass / (distance*distance);
		selfRB.AddForce(force * displacement.normalized);

		// Get orientation unit Vector3
		Vector3 orientation = getRocketOrientation();

		// Rocket Motor
		// Get thrust angle
		Vector3 gimbal = Vector3.zero;
		if (Input.GetKey(KeyCode.W)) {
			gimbal += transform.right;
		}
		if (Input.GetKey(KeyCode.A)) {
			gimbal += transform.forward;
		}
		if (Input.GetKey(KeyCode.S)) {
			gimbal += transform.right * -1;
		}
		if (Input.GetKey(KeyCode.D)) {
			gimbal += transform.forward * -1;
		}

		Vector3 thrustDirection = (getRocketOrientation() + 0.2f * gimbal).normalized;
		nozzle.rotation = Quaternion.LookRotation(-thrustDirection);

		if (Input.GetKey(KeyCode.Space)) {
			selfRB.AddForceAtPosition(10000 * thrustDirection, nozzle.position);
			plume.GetComponent<Renderer>().enabled = true;
		} else {
			plume.GetComponent<Renderer>().enabled = false;
		}
	}

	public Vector3 getRocketOrientation () {
		Vector3 orientation = (selfRB.position - nozzle.position);
		return orientation.normalized;
	}
}
