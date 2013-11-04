using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	
	GameObject Controller;
	// Use this for initialization
	void Start () {
		Controller = GameObject.Find("Controller");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/*void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Unit"){
			Controller.SendMessage("AddGold");
			//Destroy (this.gameObject);
			
		}
	}*/
}
