using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackgroundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var position = gameObject.transform.position;
		position.y += 0.5f;
		if(position.y > 190) {
			position.y = -175;
		}
		gameObject.transform.position = position;
	}
}
