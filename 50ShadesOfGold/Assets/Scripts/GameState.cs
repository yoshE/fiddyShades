using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {
	
	List<Unit> Units;
	
	// Use this for initialization
	void Start () {
		GameObject Unit1Clone = (GameObject)Instantiate(Resources.Load("Unit1"),new Vector3(-85, 11, -20), transform.rotation);
		GameObject Unit2Clone = (GameObject)Instantiate(Resources.Load("Unit2"),new Vector3(-86.5f, 11, -20), transform.rotation);
		GameObject Unit3Clone = (GameObject)Instantiate(Resources.Load("Unit3"),new Vector3(-88, 11, -20), transform.rotation);
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
	}
}
