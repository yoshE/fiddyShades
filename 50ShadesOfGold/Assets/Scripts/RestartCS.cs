using UnityEngine;
using System.Collections;

public class RestartCS : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseOver(){
		renderer.material.color = Color.red;
	}
	
	void OnMouseUp(){
		Application.LoadLevel(1);
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
