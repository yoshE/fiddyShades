using UnityEngine;
using System.Collections;

public class FadedAlpha : MonoBehaviour {
	Color startColor;
	
	// Use this for initialization
	void Start () {
		startColor = renderer.material.color;
		startColor.a = 0.5f;
		renderer.material.color = startColor;			
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
