// Copyright (c) 2017 Nathan Robert Yee

// This code is licensed under the MIT License. Refer to the LICENSE file for
// information regarding the license of this code.

// This code was tested in Unity 5.6.0f3.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AfterImageScript : MonoBehaviour {

	void Start()
    {
        InvokeRepeating("SpawnTrail", 0, 0.005f); // replace 0.2f with needed repeatRate
    }
 
    void SpawnTrail()
    {
        GameObject trailPart = new GameObject();
		trailPart.transform.localScale = transform.localScale;
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
		trailPartRenderer.sortingOrder = -1;
		trailPartRenderer.color = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),0.6f);
        trailPart.transform.position = transform.position;
        Destroy(trailPart, 0.2f); // replace 0.5f with needed lifeTime
 
        StartCoroutine("FadeTrailPart", trailPartRenderer);
    }
 
    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        Color color = trailPartRenderer.color;
        color.a -= 0.5f; // replace 0.5f with needed alpha decrement
        trailPartRenderer.color = color;
 
        yield return new WaitForEndOfFrame();
    }
}