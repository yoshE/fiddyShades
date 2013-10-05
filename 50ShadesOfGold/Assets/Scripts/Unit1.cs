using UnityEngine;
using System.Collections;

public class Unit1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.D)){
			transform.rigidbody.velocity = new Vector3(10,rigidbody.velocity.y,0);
		}
		if(Input.GetKey(KeyCode.W))
		{
			transform.rigidbody.velocity = new Vector3(rigidbody.velocity.x,10,0);
		}
	}
}
