using UnityEngine;
using System.Collections;

public class Button1 : MonoBehaviour {
	
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
		renderer.material.color = new Color(1.0f,0.39f,0.39f);
	}
	
	void OnMouseExit(){
		renderer.material.color = startColor;
	}
	
	void OnMouseDown(){
		renderer.material.color = new Color(0.90f,0,0);
	}
	
	void OnMouseUp()
	{
		Controller.SendMessage("SpawnUnit", 1);
		renderer.material.color = new Color(1.0f,0.39f,0.39f);
	}
}
