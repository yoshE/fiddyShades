using UnityEngine;
using System.Collections;

public class RedTxt : MonoBehaviour {
	
	GameObject Red;
	
	// Use this for initialization
	void Start () {
		Red = GameObject.Find("RDemo");
	}
	
	void OnMouseOver(){
		renderer.material.color = Color.red;
		if(Red.rigidbody.position.y < 1.0f)
		{
			Red.rigidbody.velocity += new Vector3(0,5,0);
		}
	}
	
	void OnMouseUp(){
		Camera.main.transform.position = new Vector3(67.3722f, 10.53314f,-17.90969f);
	}
}
