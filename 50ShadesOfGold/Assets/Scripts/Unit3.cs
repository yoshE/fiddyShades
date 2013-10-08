using UnityEngine;
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
		if(ActiveUnit && !CantDie)
		{
			if(collision.gameObject.name == "Terrain1")
			{
				print ("Collided with Terrain1");
				Controller.SendMessage("LeaderDied");
				Destroy(this.gameObject);	
			}
			else if(collision.gameObject.name == "Terrain2")
			{
				print ("Collided with Terrain2");
				Controller.SendMessage("LeaderDied");
				Destroy(this.gameObject);	
			}
		}
	}
	
	void InvulnerableOn()
	{
		CantDie = true;
	}
	
	void InvulnerableOff()
	{
		CantDie = false;
	}
}
