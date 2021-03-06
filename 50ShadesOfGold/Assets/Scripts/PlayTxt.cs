﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayTxt : MonoBehaviour {
	
	List<GameObject> CoinList = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 1.0f;
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
			GameObject temp = (GameObject)Instantiate(Resources.Load("Coin"),new Vector3(Random.Range(-5.0f,5.0f), 17, Random.Range(1.5f, 3.5f)), new Quaternion(Random.Range(-4.0f,4.0f),Random.Range(-4.0f,4.0f),Random.Range(-4.0f,4.0f),Random.Range(-5.0f,5.0f)));
			CoinList.Add(temp);
		}
	}
}
