using UnityEngine;
using System.Collections;

public class Unit2 : Unit {

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
		if(ActiveUnit && !Invulnerable)
		{
			if(collision.gameObject.name == "Terrain1")
			{
				print ("Collided with Terrain1");
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
		Invulnerable = true;
	}
	
	void InvulnerableOff()
	{
		Invulnerable = false;
	}
}
