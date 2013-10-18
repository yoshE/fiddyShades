using UnityEngine;
using System.Collections;

public class ReplayYes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		renderer.material.color = Color.black;
	}
	
	void OnMouseOver()
	{
		renderer.material.color = Color.blue;
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.black;
	}
	
	void OnMouseUp()
	{
		Application.LoadLevel(1);
	}
}
