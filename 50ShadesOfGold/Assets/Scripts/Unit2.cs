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
		if(ActiveUnit)
		{
			if(!Invulnerable)
			{
				if(collision.gameObject.name == "Terrain1(Clone)" || collision.gameObject.name == "Terrain3(Clone)")
				{
					GameObject temp = (GameObject) Instantiate(Resources.Load("TombStone"),new Vector3(this.rigidbody.position.x, 30, -17), transform.rotation);
					Controller.SendMessage("LeaderDied", this.gameObject);
				}
			}
			if(collision.gameObject.name == "Terrain2(Clone)" && tutorial)
			{
				Controller.SendMessage("displayTouchMe");
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
