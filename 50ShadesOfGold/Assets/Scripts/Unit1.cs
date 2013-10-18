using UnityEngine;
using System.Collections;

public class Unit1 : Unit {

	// Use this for initialization
	void Start () {
		UnitType = 1;
		Controller = GameObject.Find("Controller");
		Invulnerable = false;
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
				if(collision.gameObject.name == "Terrain2(Clone)")
				{
					print ("Collided with Terrain2");
					Controller.SendMessage("TouchedFloorTrue");
					Controller.SendMessage("LeaderDied");
					Destroy(this.gameObject);	
				}
				else if(collision.gameObject.name == "Terrain3(Clone)")
				{
					print ("Collided with Terrain3");
					Controller.SendMessage("TouchedFloorTrue");
					Controller.SendMessage("LeaderDied");
					Destroy(this.gameObject);	
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
