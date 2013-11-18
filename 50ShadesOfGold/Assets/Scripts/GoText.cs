﻿using UnityEngine;
using System.Collections;

public class GoText : MonoBehaviour {
	GameObject Controller;
	public bool tutorial = false;
	
	// Use this for initialization
	void Start () {
		Controller = GameObject.Find("Controller");
		renderer.material.color = Color.black;
	}
	
	void Update(){
		
	}
	
	void OnMouseEnter() {
        renderer.material.color = Color.red;
    }
	
	void OnMouseExit()
	{
		renderer.material.color = Color.black;
	}

	void OnMouseDown()
	{
		Controller.SendMessage("StartQuest");
		Controller.SendMessage("IsLeader");
	}
}
