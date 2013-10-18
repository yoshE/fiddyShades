using UnityEngine;
using System.Collections;

public class MenuText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		renderer.material.color = Color.black;
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.black;
	}
}
