// Copyright (c) 2017 Nathan Robert Yee

// This code is licensed under the MIT License. Refer to the LICENSE file for
// information regarding the license of this code.

// This code was tested in Unity 5.6.0f3.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using DanmakU;

public class PlayerMovementScript : MonoBehaviour {
	// These speed variables are meant to be set in the Unity editor.
	public float speedX = 3;
	public float speedY = 3;

	public int layer = 0;

	public Camera gameCamera;

	Rigidbody2D rb2D;

	private float cameraUpperBound = 0;
	private float cameraLowerBound = 0;

	private DanmakuEmitter bulletEmitter;

	void Start () {
		gameObject.layer = layer;//(1 << layer);
		bulletEmitter = gameObject.GetComponents<DanmakuEmitter>()[0];
		//There's a bug with enabled
		//bulletEmitter.enabled = false;
		print(bulletEmitter);
		//print("Player layer:"+gameObject.layer);
		// Ensure that a BoxCollider2D and Rigidbody2D exist on the
		// GameObject this script is attached to.
		Assert.IsNotNull (GetComponent<BoxCollider2D> ());
		Assert.IsNotNull (GetComponent<Rigidbody2D> ());

		rb2D = GetComponent<Rigidbody2D> ();

		// Set up the Rigidbody2D in case it isn't already
		// set up properly in the Unity editor.
		// The Rigidbody2D should be Dynamic, have a
		// Gravity Scale of 0 and have its Freeze Rotation
		// Constraint set to true.
		rb2D.bodyType = RigidbodyType2D.Dynamic;
		rb2D.gravityScale = 0;
		rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;

		//var vertExtent = Camera.main.camera.orthographicSize;
		float padding = 10;
		float cameraRadius = gameCamera.orthographicSize;  
		cameraUpperBound = gameCamera.transform.position.y;//center
		cameraLowerBound = cameraUpperBound + cameraRadius - padding;
		cameraUpperBound -= cameraRadius - padding;
		//print(GetComponents<Camera>().Length);
		//print(gameCamera);
	}

	void Update () {
		float nextX = rb2D.transform.position.x;
		float nextY = rb2D.transform.position.y;
		bool changedValue = false;
		if(Input.GetKey(KeyCode.Space)) {
			if(!bulletEmitter.isShooting)
				bulletEmitter.isShooting = true;
		} else {
			if(bulletEmitter.isShooting) {
				bulletEmitter.isShooting = false;
			}
		}
		// Get input.
		if (Input.GetKey (KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			nextY += speedY;
			changedValue = true;
		}
		if (Input.GetKey (KeyCode.A)  || Input.GetKey(KeyCode.LeftArrow)) {
			nextX -= speedX;
			changedValue = true;
		}
		if (Input.GetKey (KeyCode.S)  || Input.GetKey(KeyCode.DownArrow)) {
			nextY -= speedY;
			changedValue = true;
		}
		if (Input.GetKey (KeyCode.D)  || Input.GetKey(KeyCode.RightArrow)) {
			nextX += speedX;
			changedValue = true;
		}
		// Move the Rigidbody2D.
		//print("Next Pos: "+nextX+" "+nextY);
		//Magic hardcoded dimensions rip
		if(changedValue) {
			//x axis
			if(Mathf.Abs(nextX) >= 95) {
				nextX = rb2D.transform.position.x;
			}
			//y axis
			//print(cameraUpperBound+" "+cameraLowerBound+" "+nextY);
			if(nextY < cameraUpperBound || nextY > cameraLowerBound) {
				nextY = rb2D.transform.position.y;
			}
			rb2D.MovePosition(new Vector2(nextX, nextY));
		} else {
		}
	}
}