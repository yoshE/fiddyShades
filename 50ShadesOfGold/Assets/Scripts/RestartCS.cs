using UnityEngine;
using System.Collections;

public class RestartCS : MonoBehaviour {
	
	public bool tutorial = false;
	
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
		if(tutorial)
		{
			Application.LoadLevel(4);
		}
		else{
			Application.LoadLevel(1);
		}
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
