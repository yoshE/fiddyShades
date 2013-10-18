using UnityEngine;
using System.Collections;

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
}
