using UnityEngine;
using System.Collections;
using System.IO;
using System;


public class ReplayNo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		renderer.material.color = Color.black;

	}
	
	void OnMouseEnter()
	{
		renderer.material.color = Color.green;
	}
	
	void OnMouseExit()
	{
		renderer.material.color = Color.black;
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
		string output= "Clicked NO" + Environment.NewLine + Environment.NewLine;
		string fileName = "Data.txt";
		//string fileName = "Resources/Data.txt";
		File.AppendAllText(fileName, output);
	}
}
