using UnityEngine;
using System.Collections;
using System.IO;
using System;

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
	void OnApplicationQuit(){
		print ("QUIT");
		string dateTime = System.DateTime.Now.ToString (); 	//Get the time to tack on to the file name
		dateTime = dateTime.Replace ("/", "-"); 			//Replace slashes with dashes, because Unity thinks they are directories..
		string Name = "Metrics_" + dateTime;			//Append file name
		string output= "Clicked QUIT" + Environment.NewLine + Environment.NewLine;
		string fileName = "Data.txt";
		//string fileName = "Resources/Data.txt";
		File.AppendAllText(fileName, output);
	}
}
