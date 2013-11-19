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
		if(collision.gameObject.tag == "Terrain")
		{
			Grounded = true;
		}
		if(collision.gameObject.name == "Jacuzzi Model(Clone)" || collision.gameObject.name == "Jacuzzi Model")
		{
			Controller.SendMessage("endGame");
		}
	}
	public void died(){
		AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);
	}
	public void jumped(){
		AudioSource.PlayClipAtPoint(jumpSFX, Camera.main.transform.position);
	}

}
