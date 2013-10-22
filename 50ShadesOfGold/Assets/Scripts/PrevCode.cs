using UnityEngine;
using System.Collections;

public class PrevCode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		renderer.material.color = Color.blue;
	}
	
	void OnMouseEnter(){
		renderer.material.color = Color.red;
	}
	
	void OnMouseExit()
	{
		renderer.material.color = Color.blue;
	}
	
	void OnMouseUp()
	{
		Camera.main.transform.position = new Vector3(67.3722f, 10.53314f,-17.90969f);
	}
}
