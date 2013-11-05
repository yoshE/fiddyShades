using UnityEngine;
using System.Collections;

public class ShopExitText : MonoBehaviour {

	GameObject Controller;
	
	// Use this for initialization
	void Start () {
		Controller = GameObject.Find("Controller");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseOver(){
		renderer.material.color = Color.red;
	}
	
	void OnMouseUp(){
		Controller.SendMessage("Unshop");
	}
	
	void OnMouseExit(){
		renderer.material.color = Color.white;
	}
}
