using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateTry : MonoBehaviour {
	
	List<GameObject> Units  = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
		SpawnUnit(1);
		SpawnUnit(2);
		SpawnUnit(3);
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < Units.Count; i++)
		{
			Move (Units[i]);	
		}
	}
	
	void Move(GameObject unit){
		if(Input.GetKey(KeyCode.D)){
			unit.transform.rigidbody.velocity = new Vector3(10,unit.rigidbody.velocity.y,0);
		}
		if(Input.GetKeyUp(KeyCode.D))
		{
			unit.transform.rigidbody.velocity = new Vector3(0,unit.rigidbody.velocity.y,0);
		}
		if(Input.GetKey(KeyCode.A)){
			unit.transform.rigidbody.velocity = new Vector3(-10,unit.rigidbody.velocity.y,0);
		}
		if(Input.GetKeyUp(KeyCode.A))
		{
			unit.transform.rigidbody.velocity = new Vector3(0,unit.rigidbody.velocity.y,0);
		}
		if(Input.GetKey(KeyCode.W))
		{
			if(unit.rigidbody.position.y <= 11)
			{
				unit.transform.rigidbody.velocity = new Vector3(unit.rigidbody.velocity.x,5,0);
			}
		}	
	}
	
	void SpawnUnit(int uType){
		if(uType == 1)
		{
			GameObject temp = (GameObject)Instantiate(Resources.Load("Unit1"),new Vector3(-85, 11, -20), transform.rotation);
			Units.Add(temp);
		}
		else if(uType == 2)
		{
			GameObject temp = (GameObject)Instantiate(Resources.Load("Unit2"),new Vector3(-86.5f, 11, -20), transform.rotation);
			Units.Add(temp);
		}
		else if(uType == 3)
		{
			GameObject temp = (GameObject)Instantiate(Resources.Load("Unit3"),new Vector3(-88, 11, -20), transform.rotation);
			Units.Add(temp);
		}
	}
}
