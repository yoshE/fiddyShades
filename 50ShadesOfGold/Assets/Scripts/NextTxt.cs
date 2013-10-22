using UnityEngine;
using System.Collections;

public class NextTxt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		renderer.material.color = Color.blue;
	}
	
	void OnMouseEnter(){
		renderer.material.color = Color.red;
	}
	
	void OnMouseExit()
	{
		renderer.material.color = Color.blue;
	}
	
	void OnMouseUp()
	{
		Camera.main.transform.position = new Vector3(141.0735f, 12.22566f,-17.90969f);
	}
}
