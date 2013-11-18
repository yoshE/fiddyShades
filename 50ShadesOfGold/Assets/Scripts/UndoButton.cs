using UnityEngine;
using System.Collections;

public class UndoButton : MonoBehaviour {
	
	GameObject Controller;
	public bool tutorial = false;
	
	// Use this for initialization
	void Start () {
		Controller = GameObject.Find("Controller");
		renderer.material.color = Color.white;
	}
	
	void Update(){
		
	}
	
	void OnMouseEnter() {
        renderer.material.color = Color.red;
    }
	
	void OnMouseExit()
	{
		renderer.material.color = Color.white;
	}

	void OnMouseDown()
	{
		Controller.SendMessage("UndoUnit");
		Controller.SendMessage("IsLeader");
	}
}
