using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlueTxt : MonoBehaviour {
	
	List<GameObject> CoinList = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnMouseOver(){
		renderer.material.color = Color.blue;
		spawnCoin();
	}
	
	void OnMouseEnter(){
		foreach(GameObject o in CoinList)
		{
			Destroy(o);
		}
		CoinList.Clear();
	}
	
	void OnMouseUp(){
		Application.LoadLevel(1);
	}
	
	void spawnCoin()
	{
		if(CoinList.Count < 800)
		{
			GameObject temp = (GameObject)Instantiate(Resources.Load("Coin"),new Vector3(Random.Range(-5.0f,5.0f), 17, Random.Range(1.5f, 3.5f)), transform.rotation);
			CoinList.Add(temp);
		}
	}
}
