using UnityEngine;
using System.Collections;

public class Unit2ShopBut : MonoBehaviour {

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
		renderer.material.color = new Color(0.39f,0.39f,1.0f);
	}
	
	void OnMouseExit(){
		renderer.material.color = startColor;
	}
	
	void OnMouseDown(){
		renderer.material.color = new Color(0,0,0.90f);
	}
	
	void OnMouseUp()
	{
		Controller.SendMessage("SpawnUnit", 5);
		renderer.material.color = new Color(0.39f,0.39f,1.0f);
	}
}
