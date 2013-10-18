using UnityEngine;
using System.Collections;

public class GreenTxt : MonoBehaviour {
	
	GameObject Green;
	float prevVelocity = -10;
	
	// Use this for initialization
	void Start () {
		Green = GameObject.Find("GDemo");
	}
	
	void OnMouseOver(){
		renderer.material.color = Color.green;
		if(prevVelocity == -10)
		{
			Green.rigidbody.velocity = new Vector3(10,0,0);
			if(Green.rigidbody.position.x >= 11.5f)
			{
				prevVelocity = 10;
			}
		}
		if(prevVelocity == 10)
		{
			Green.rigidbody.velocity = new Vector3(-10,0,0);
			if(Green.rigidbody.position.x <= 4.5f)
			{
				prevVelocity = -10;
			}
		}
	}
	
	void OnMouseExit()
	{
		Green.rigidbody.velocity = new Vector3(0,0,0);
	}
	
	void OnMouseUp()
	{
		Application.Quit();
	}
}
