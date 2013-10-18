﻿using UnityEngine;
using System.Collections;

public class Unit3 : Unit {

	// Use this for initialization
	void Start () {
		UnitType = 3;
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
			if(collision.gameObject.name == "Terrain1(Clone)" || collision.gameObject.name == "Terrain2(Clone)" || collision.gameObject.name == "Terrain3(Clone)")
			{
				Controller.SendMessage("TouchedFloorTrue");
			}
			if(!Invulnerable)
			{
				if(collision.gameObject.name == "Terrain1(Clone)")
				{
					print ("Collided with Terrain1");
					Controller.SendMessage("TouchedFloorTrue");
					Controller.SendMessage("LeaderDied");
					Destroy(this.gameObject);
					GameObject temp = (GameObject) Instantiate(Resources.Load("TombStone"),new Vector3(this.rigidbody.position.x, 40, -17), transform.rotation);
				}
				else if(collision.gameObject.name == "Terrain2(Clone)")
				{
					print ("Collided with Terrain2");
					Controller.SendMessage("TouchedFloorTrue");
					Controller.SendMessage("LeaderDied");
					Destroy(this.gameObject);	
					GameObject temp = (GameObject) Instantiate(Resources.Load("TombStone"),new Vector3(this.rigidbody.position.x, 40, -17), transform.rotation);
				}
			}
		}
		if(collision.gameObject.name == "Jacuzzi Model(Clone)" || collision.gameObject.name == "Jacuzzi Model")
		{
			Controller.SendMessage("endGame");
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
