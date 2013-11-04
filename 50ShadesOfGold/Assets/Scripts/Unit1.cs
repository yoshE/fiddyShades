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
			if(!Invulnerable)
			{
				if(collision.gameObject.name == "Terrain2(Clone)" || collision.gameObject.name == "Terrain3(Clone)")
				{
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
