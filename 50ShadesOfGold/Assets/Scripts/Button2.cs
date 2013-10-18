using UnityEngine;
using System.Collections;

public class Button2 : MonoBehaviour {
	
	GameObject Controller;
	
	// Use this for initialization
	void Start () {
		Controller = GameObject.Find("Controller");
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnMouseDown()
	{
		Controller.SendMessage("SpawnUnit", 2);
	}
}
