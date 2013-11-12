﻿using UnityEngine;
using System.Collections;

public class Unit2 : Unit {
	
	public bool tutorial = false;
	
	// Use this for initialization
	void Start () {
		UnitType = 2;
		Controller = GameObject.Find("Controller");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	int Type(){
		return UnitType;	
	}
	void OnCollisionStay(Collision collision)
	{
		if(ActiveUnit)
		{
			if(!Invulnerable)
			{
				if(collision.gameObject.name == "Terrain1(Clone)" || collision.gameObject.name == "Terrain3(Clone)")
				{
					Controller.SendMessage("LeaderDied");
					Destroy(this.gameObject);	
					GameObject temp = (GameObject) Instantiate(Resources.Load("TombStone"),new Vector3(this.rigidbody.position.x, 40, -17), transform.rotation);
				}
			}
			if(collision.gameObject.name == "Terrain2(Clone)" && tutorial)
			{
				Controller.SendMessage("displayTouchMe");
			}
		}
		if(collision.gameObject.name == "HQ" && Input.GetKey(KeyCode.A))
		{
			transform.rigidbody.velocity -= new Vector3(0, 1.3f, 0);
		}
	}
	
	void InvulnerableOn()
	{
		Invulnerable = true;
	}
	
	void InvulnerableOff()
	{
		Invulnerable = false;
	}
}
