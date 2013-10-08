using UnityEngine;
using System.Collections;

public class Unit1 : Unit {

	// Use this for initialization
	void Start () {
		UnitType = 1;
		Controller = GameObject.Find("Controller");
		CantDie = false;
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
			if(collision.gameObject.name == "Terrain2")
			{
				print ("Collided with Terrain2");
				Controller.SendMessage("LeaderDied");
				Destroy(this.gameObject);	
			}
			else if(collision.gameObject.name == "Terrain3")
			{
				print ("Collided with Terrain3");
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
