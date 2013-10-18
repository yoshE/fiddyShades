using UnityEngine;
using System.Collections;

public class GoText : MonoBehaviour {

	// Use this for initialization
	void Start () {
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
}
