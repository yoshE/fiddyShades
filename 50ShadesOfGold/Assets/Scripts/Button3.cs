using UnityEngine;
using System.Collections;

public class Button3 : MonoBehaviour {
	
	GameObject Controller;
	Color startColor;
	
	// Use this for initialization
	void Start () {
		Controller = GameObject.Find("Controller");
		startColor = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseEnter(){
		renderer.material.color = new Color(0.56f,1.0f,0.56f);
	}
	
	void OnMouseExit(){
		renderer.material.color = startColor;
	}
	
	void OnMouseDown(){
		renderer.material.color = new Color(0,0.90f,0);
	}
	
	void OnMouseUp()
	{
		Controller.SendMessage("SpawnUnit", 3);
		renderer.material.color = new Color(0.56f,1.0f,0.56f);
	}
}
