using UnityEngine;
using System.Collections;

public class GoText : MonoBehaviour {
	GameObject Controller;
	
	// Use this for initialization
	void Start () {
		Controller = GameObject.Find("Controller");
		renderer.material.color = Color.black;
	}
	
	void Update(){
		
	}
	
	void OnMouseEnter() {
		print ("On mouse enter");
        renderer.material.color = Color.red;
    }
	
	void OnMouseExit()
	{
		print ("Mouse Exit");
		renderer.material.color = Color.black;
	}

	void OnMouseDown()
	{
		Controller.SendMessage("StartQuest");
		Controller.SendMessage("IsLeader");
	}
}
