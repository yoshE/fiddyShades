using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {
	
	List<GameObject> Units  = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
		SpawnUnit(1);
		SpawnUnit(2);
		SpawnUnit(3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Move(){
		if(Input.GetKey(KeyCode.D)){
			transform.rigidbody.velocity = new Vector3(10,rigidbody.velocity.y,0);
		}
		if(Input.GetKeyUp(KeyCode.D))
		{
			transform.rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,0);
		}
		if(Input.GetKey(KeyCode.A)){
			transform.rigidbody.velocity = new Vector3(-10,rigidbody.velocity.y,0);
		}
		if(Input.GetKeyUp(KeyCode.A))
		{
			transform.rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,0);
		}
		if(Input.GetKey(KeyCode.W))
		{
			if(rigidbody.position.y <= 11)
			{
				transform.rigidbody.velocity = new Vector3(rigidbody.velocity.x,5,0);
			}
		}
		if(Input.GetKey(KeyCode.Space))
		{
			print("yes");
			GameObject bob = new GameObject();	
			bob = Units[0];
			for (int i = 0; i < Units.Count -1 ; i++){
				Units[i] = Units[i+1];	
			}
			Units[Units.Count-1] = bob;
				
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
