using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	protected int UnitType;
	public bool ActiveUnit = false;
	protected GameObject Controller;
	protected bool CantDie;
	
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
}
