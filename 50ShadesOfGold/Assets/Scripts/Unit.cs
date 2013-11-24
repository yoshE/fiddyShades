using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	public AudioClip jumpSFX;
	public AudioClip deathSFX;
	public bool isDead = false;
	protected int UnitType;
	protected GameObject Controller;
	protected bool Invulnerable;
	public bool ActiveUnit = false;
	public bool Grounded =  true;
	public bool tutorial = false;
	
	// Use this for initialization
	void Start () {
		Controller = GameObject.Find("Controller");
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	int Type(){
		return UnitType;	
	}
	
	void IsActiveUnit()
	{
		ActiveUnit = true;	
	}
	
	void IsNotActiveUnit()
	{
		ActiveUnit = false;	
	}
	
	void Spawning()
	{
		Invulnerable = true;
	}
	
	void Begin()
	{
		Invulnerable = false;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Terrain" || collision.gameObject.name == "Obstacle1")
		{
			Grounded = true;
		}
		if(collision.gameObject.name == "Jacuzzi Model(Clone)" || collision.gameObject.name == "Jacuzzi Model")
		{
			Controller.SendMessage("endGame");
		}
	}
	
	void OnCollisionStay(Collision collision)
	{
		if(ActiveUnit)
		{
			if(collision.gameObject.name == "Obstacle1" || collision.gameObject.name == "HQ")
			{
				Vector3 pos = collision.contacts[0].point;
				if(pos.y < collision.gameObject.transform.position.y + .9f)
				{
				print ("pos is " + pos);
					print ("col y pos is " + collision.gameObject.transform.position.y);
					
					if(pos.x > this.rigidbody.position.x)
					{
						Controller.SendMessage("TouchingWall", 1.0f);
					}
					else
					{
						Controller.SendMessage("TouchingWall", -1.0f);
					}
				}
				else
				{
				print ("turned off constraint " + pos);
					
					Controller.SendMessage("TouchingWall", 0f);
				}
			}
		}		
	}
	
	void OnCollisionExit(Collision collision)
	{
		if(ActiveUnit)
		{
			if(collision.gameObject.name == "Obstacle1" || collision.gameObject.name == "HQ")
			{
				Controller.SendMessage("TouchingWall", 0f);
			}
		}
	}
	
	public void died(){
		AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);
	}
	public void jumped(){
		AudioSource.PlayClipAtPoint(jumpSFX, Camera.main.transform.position);
	}

}
