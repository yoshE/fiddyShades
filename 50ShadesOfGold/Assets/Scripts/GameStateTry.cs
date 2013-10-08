using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class GameStateTry : MonoBehaviour {
	
	List<GameObject> Units  = new List<GameObject>();
	float jumpPos;
	float prevVelocity = 10;
	
	// Use this for initialization
	void Start () {
		SpawnUnit(1);
		SpawnUnit(2);
		SpawnUnit(3);
		SpawnUnit(2);
	}
	
	// Update is called once per frame
	void Update () {
		MoveLeader(Units[0]);
		for(int i = 1; i < Units.Count; i++)
		{
			MoveFollower(Units[i]);	
		}
	}
	
	void MoveLeader(GameObject unit){
		if(Input.GetKeyDown(KeyCode.Space))
		{
			GameObject bob = new GameObject();	
			bob = Units[0];
			float tempPosX = bob.rigidbody.position.x;
			if(prevVelocity==10){
				for (int i = 0; i < Units.Count -1 ; i++){
					Units[i] = Units[i+1];
					Units[i].rigidbody.position = new Vector3(tempPosX,Units[i].rigidbody.position.y,-20);
					tempPosX = (Units[i].rigidbody.position.x - 1.5f);
					print(tempPosX);
				}
				bob.rigidbody.position = new Vector3(tempPosX,bob.rigidbody.position.y,-20);
				Units[Units.Count-1] = bob;
				DestroyObject(bob);
			}
			else{
				for (int i = 0; i < Units.Count -1 ; i++){
					Units[i] = Units[i+1];
					Units[i].rigidbody.position = new Vector3(tempPosX,Units[i].rigidbody.position.y,-20);
					tempPosX = (Units[i].rigidbody.position.x + 1.5f);
					print(tempPosX);
				}
				bob.rigidbody.position = new Vector3(tempPosX,bob.rigidbody.position.y,-20);
				Units[Units.Count-1] = bob;
			}
			
		}
		if(Input.GetKey(KeyCode.D)){
			unit.transform.rigidbody.velocity = new Vector3(10,unit.rigidbody.velocity.y,0);
			if(prevVelocity == -10)
			{
				jumpPos = 100000000;
				TurnAround();
				prevVelocity = 10;
			}
		}
		if(Input.GetKeyUp(KeyCode.D))
		{
			unit.transform.rigidbody.velocity = new Vector3(0,unit.rigidbody.velocity.y,0);
		}
		if(Input.GetKey(KeyCode.A)){
			unit.transform.rigidbody.velocity = new Vector3(-10,unit.rigidbody.velocity.y,0);
			if(prevVelocity == 10)
			{
				jumpPos = 100000000;
				TurnAround();
				prevVelocity = -10;
			}
		}
		if(Input.GetKeyUp(KeyCode.A))
		{
			unit.transform.rigidbody.velocity = new Vector3(0,unit.rigidbody.velocity.y,0);
		}
		if(Input.GetKey(KeyCode.W))
		{
			if(unit.rigidbody.position.y <= 11)
			{
				jumpPos = unit.transform.position.x;
				unit.transform.rigidbody.velocity = new Vector3(unit.rigidbody.velocity.x,7,0);
				print ("jumpPos is" + jumpPos +"!");
			}
		}
			
	}
	
	void MoveFollower(GameObject unit){
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
		if(unit.rigidbody.position.y <= 11)
		{
			if(unit.rigidbody.position.x >= jumpPos - .3f && unit.rigidbody.position.x <= jumpPos + .3f)
			{
				unit.transform.rigidbody.velocity = new Vector3(unit.rigidbody.velocity.x,7,0);
				print ("follower jumped");
			}
		}
	}
	
	void SpawnUnit(int uType){
		if(uType == 1)
		{
			GameObject temp = (GameObject) Instantiate(Resources.Load("Unit1"),new Vector3(-85 - 1.5f * Units.Count, 11, -20), transform.rotation);
			Units.Add(temp);
		}
		else if(uType == 2)
		{
			GameObject temp = (GameObject) Instantiate(Resources.Load("Unit2"),new Vector3(-85 - 1.5f * Units.Count, 11, -20), transform.rotation);
			Units.Add(temp);
		}
		else if(uType == 3)
		{
			GameObject temp = (GameObject) Instantiate(Resources.Load("Unit3"),new Vector3(-85 - 1.5f * Units.Count, 11, -20), transform.rotation);
			Units.Add(temp);
		}
	}
	
	void TurnAround(){
		if(Units.Count % 2 == 0)
		{
			for(int i = 0; i < Units.Count/2; i++)
			{
				float tempPosX = Units[i].rigidbody.position.x;
				Units[i].rigidbody.position = new Vector3(Units[Units.Count - i - 1].rigidbody.position.x,Units[i].rigidbody.position.y,-20);
				Units[Units.Count - i - 1].rigidbody.position = new Vector3(tempPosX, Units[Units.Count - i-1].rigidbody.position.y,-20);
			}
		}
		else if(Units.Count % 2 == 1)
		{
			for(int i = 0; i < Units.Count/2 + 1; i++)
			{
				float tempPosX = Units[i].rigidbody.position.x;
				Units[i].rigidbody.position = new Vector3(Units[Units.Count - i - 1].rigidbody.position.x,Units[i].rigidbody.position.y,-20);
				Units[Units.Count - i - 1].rigidbody.position = new Vector3(tempPosX, Units[Units.Count - i-1].rigidbody.position.y,-20);
			}
		}
	}
}
