using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	protected int UnitType;
	protected GameObject Controller;
	protected bool Invulnerable;
	public bool ActiveUnit = false;
	public bool Grounded =  true;
	
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
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Terrain")
		{
			Grounded = true;
		}
	}
}
