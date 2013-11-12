using UnityEngine;
using System.Collections;

public class BoulderCS : MonoBehaviour {
	
	GameObject Controller;
	
	// Use this for initialization
	void Start () {
		Controller = GameObject.Find("Controller");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision)
	{
		print (collision.gameObject.tag);
		if(collision.gameObject.tag != "Terrain" && collision.gameObject.tag != "Coin" && collision.gameObject.tag != "Unit")
		{
			Controller.GetComponent<GameState>().Boulders.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
	}
}
