﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateTry : MonoBehaviour {
	
	List<GameObject> CoinList = new List<GameObject>();
	List<GameObject> Units  = new List<GameObject>();
	List<GameObject> Terrains = new List<GameObject>();
	float jumpPos;
	float prevVelocity = 10;
	int coinCount = 0;
	int gold = 2000;
	bool touchedFloor = true;
	bool paused = true;
	bool swapped = false;
	
	// Use this for initialization
	void Start () 
	{
		spawnTerrain();
		Restart();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!paused)
		{
			if(coinCount < 300){
				spawnCoin ();
			}
			if(Units.Count > 0)
			{
				UpdateCamera();
				MoveLeader(Units[0]);
				MoveFollower();
				for (int i = 0; i < CoinList.Count -1 ; i++){
					colCheck (CoinList[i]);
				}
			}
			Swap();
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Restart();
		}
	}
	
	void Swap()
	{
		//code to swap
		if(Input.GetKeyDown(KeyCode.Space))
		{
			GameObject bob;	
			bob = Units[0];
			float tempPosX = bob.rigidbody.position.x;
			if(prevVelocity==10)
			{
				for (int i = 0; i < Units.Count -1 ; i++)
				{
					Units[i] = Units[i+1];
					Units[i].rigidbody.position = new Vector3(tempPosX,Units[i].rigidbody.position.y,-20);
					tempPosX = (Units[i].rigidbody.position.x - 1.5f);
					print(tempPosX);
				}
				bob.rigidbody.position = new Vector3(tempPosX,bob.rigidbody.position.y,-20);
				Units[Units.Count-1] = bob;
			}
			else
			{
				for (int i = 0; i < Units.Count -1 ; i++)
				{
					Units[i] = Units[i+1];
					Units[i].rigidbody.position = new Vector3(tempPosX,Units[i].rigidbody.position.y,-20);
					tempPosX = (Units[i].rigidbody.position.x + 1.5f);
					print(tempPosX);
				}
				bob.rigidbody.position = new Vector3(tempPosX,bob.rigidbody.position.y,-20);
				Units[Units.Count-1] = bob;
			}
			IsLeader();
			IsFollower();
		}	
	}
	
	void MoveLeader(GameObject unit)
	{
		//leader moving code
		if(Input.GetKey(KeyCode.D)){
			unit.transform.rigidbody.velocity = new Vector3(10,unit.rigidbody.velocity.y,0);
			if(prevVelocity == -10)
			{
				jumpPos = 100000000;
				TurnAround();
				prevVelocity = 10;
				swapped = true;
			}
		}
		if(Input.GetKeyUp(KeyCode.D))
		{
			unit.transform.rigidbody.velocity = new Vector3(0,unit.rigidbody.velocity.y,0);
		}
		if(Input.GetKey(KeyCode.A)){
			unit.transform.rigidbody.velocity = new Vector3(-10,unit.rigidbody.velocity.y,0);
			if(prevVelocity == 10)
			{
				jumpPos = 100000000;
				TurnAround();
				prevVelocity = -10;
				swapped = true;
			}
		}
		if(Input.GetKeyUp(KeyCode.A))
		{
			unit.transform.rigidbody.velocity = new Vector3(0,unit.rigidbody.velocity.y,0);
		}
		if(Input.GetKey(KeyCode.W))
		{
			if(touchedFloor)
			{
				jumpPos = unit.transform.position.x;
				unit.transform.rigidbody.velocity = new Vector3(unit.rigidbody.velocity.x,7,0);
				print("jumpPos is" + jumpPos +"!");
				touchedFloor = false;
			}
		}
			
	}
	
	void MoveFollower(){
		for(int i = 1; i < Units.Count; i++)
		{
			if(prevVelocity == 10){
				Units[i].transform.rigidbody.position = new Vector3(Units[0].rigidbody.position.x - i * 1.5f,Units[i].rigidbody.position.y,-20);
			}
			if(prevVelocity == -10){
				Units[i].transform.rigidbody.position = new Vector3(Units[0].rigidbody.position.x + i * 1.5f,Units[i].rigidbody.position.y,-20);
			}
			if(Units[i].rigidbody.position.y <= 11)
			{
				if(Units[i].rigidbody.position.x >= jumpPos - .3f && Units[i].rigidbody.position.x <= jumpPos + .3f)
				{
					Units[i].transform.rigidbody.velocity = new Vector3(Units[i].rigidbody.velocity.x,7,0);
					print("follower jumped");
				}
			}
		}
		
	}
	
	void SpawnUnit(int uType)
	{
		if(gold >= 500 && Units.Count < 6)
		{
			if(Units.Count > 0)
			{
				foreach(GameObject o in Units)
				{
					o.rigidbody.position += new Vector3(1.5f,0,0);
				}
			}
			if(uType == 1)
			{
				GameObject temp = (GameObject) Instantiate(Resources.Load("Unit1"),new Vector3(-88, 11, -20), transform.rotation);
				Units.Add(temp);
			}
			else if(uType == 2)
			{
				GameObject temp = (GameObject) Instantiate(Resources.Load("Unit2"),new Vector3(-88, 11, -20), transform.rotation);
				Units.Add(temp);
			}
			else if(uType == 3)
			{
				GameObject temp = (GameObject) Instantiate(Resources.Load("Unit3"),new Vector3(-88, 11, -20), transform.rotation);
				Units.Add(temp);
			}
			gold -= 500;
		}
	}
	
	void TurnAround()
	{
		if(Units.Count % 2 == 0)
		{
			for(int i = 0; i < Units.Count/2; i++)
			{
				float tempPosX = Units[i].rigidbody.position.x;
				Units[i].rigidbody.position = new Vector3(Units[Units.Count - i - 1].rigidbody.position.x,Units[i].rigidbody.position.y,-20);
				Units[Units.Count - i - 1].rigidbody.position = new Vector3(tempPosX, Units[Units.Count - i-1].rigidbody.position.y,-20);
			}
		}
		else if(Units.Count % 2 == 1)
		{
			for(int i = 0; i < Units.Count/2 + 1; i++)
			{
				float tempPosX = Units[i].rigidbody.position.x;
				Units[i].rigidbody.position = new Vector3(Units[Units.Count - i - 1].rigidbody.position.x,Units[i].rigidbody.position.y,-20);
				Units[Units.Count - i - 1].rigidbody.position = new Vector3(tempPosX, Units[Units.Count - i-1].rigidbody.position.y,-20);
			}
		}
	}
	
	void IsLeader()
	{
		if(Units.Count > 0)
		{
			Units[0].SendMessage("IsActiveUnit");	
		}
	}
	
	void IsFollower()
	{
		for(int i = 1; i < Units.Count; i++)
		{
			Units[i].SendMessage("IsNotActiveUnit");
		}
	}
	
	void LeaderDied()
	{
		Units.RemoveAt(0);
		if(Units.Count > 0)
		{
			for(int i = 0; i < Units.Count; i++)
			{
				Units[i].SendMessage("InvulnerableOn");
			}
			Units[0].SendMessage("IsActiveUnit");
			StartCoroutine("InvulnerableTime");
		}
		else
		{
			paused = true;
			Camera.main.transform.position = new Vector3(-80.14238f, 21.38423f, -43.61178f);
			foreach(GameObject o in CoinList)
			{
				Destroy(o);
			}
			CoinList.Clear();
		}
	}
	
	private IEnumerator InvulnerableTime()
	{
		if(Units.Count > 0)
		{
			yield return new WaitForSeconds(2);
			for(int i = 0; i < Units.Count; i++)
			{
				Units[i].SendMessage("InvulnerableOff");
			}
			print ("Invulnerable Time Over!");
		}
	}
	
	void colCheck(GameObject coin1)
	{
		if(Mathf.Abs(coin1.rigidbody.position.x - Units[0].rigidbody.position.x) <1){
			if(coin1.rigidbody.position.y <13)
			{
				gold++;
				coin1.rigidbody.position = new Vector3(Units[0].rigidbody.position.x + Random.Range (-2f,15f), 35, -20);
				coin1.rigidbody.velocity = new Vector3(0,0,0);
			}
		}
		if(Mathf.Abs(coin1.rigidbody.position.x - Units[0].rigidbody.position.x) > 40){
			coin1.rigidbody.position = new Vector3(Units[0].rigidbody.position.x + Random.Range (-2f,12f), 35, -20);
			coin1.rigidbody.velocity = new Vector3(0,0,0);
		}
		if(Mathf.Abs(coin1.rigidbody.position.y) > 90)
		{
			coin1.rigidbody.position = new Vector3(Units[0].rigidbody.position.x + Random.Range (-2f,7f), 35, -20);
			coin1.rigidbody.velocity = new Vector3(0,0,0);
		}
	}
	
	void OnGUI()
	{
		GUI.Box (new Rect (10,Screen.height - 25,100,25), "$ "+ gold);
	}
	
	void TouchedFloorTrue()
	{
		touchedFloor = true;
	}
	
	void spawnCoin()
	{
		if(Units.Count > 0)
		{
			if(prevVelocity==10){
				GameObject temp = (GameObject)Instantiate(Resources.Load("Coin"),new Vector3(Units[0].rigidbody.position.x + Random.Range (-1f,6f), 35, -20), transform.rotation);
				CoinList.Add(temp);
			}
			else
			{
				GameObject temp = (GameObject)Instantiate(Resources.Load("Coin"),new Vector3(Units[0].rigidbody.position.x + Random.Range (-2f,7f), 35, -20), transform.rotation);
				CoinList.Add(temp);
				coinCount++;
			}
		}
	}
	
	void UpdateCamera()
	{
		/*if(swapped && ((Mathf.Abs(Camera.main.transform.position.x - Units[0].rigidbody.position.x))<= 0.5f)){
			Camera.main.transform.position = new Vector3(Units[0].rigidbody.position.x, 21.38423f,-46.61178f);
			swapped = false;
		}
		else if(swapped && prevVelocity == -10){
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - (Camera.main.transform.position.x - Units[0].rigidbody.position.x)/30, 21.38423f,-46.61178f);
		}
		else if(swapped && prevVelocity == 10){
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + (Units[0].rigidbody.position.x - Camera.main.transform.position.x)/30, 21.38423f,-46.61178f);
		}
		else{*/
			Camera.main.transform.position = new Vector3(Units[0].rigidbody.position.x, 21.38423f,-46.61178f);
		//}
	}
	
	void StartQuest()
	{
		if(Units.Count > 0)
		{
			paused = false;
			IsLeader();
		}
	}
	
	void Restart()
	{
		foreach(GameObject o in Units)
		{
			Destroy(o);
		}
		foreach(GameObject o in CoinList)
		{
			Destroy(o);
		}
		Units.Clear();
		CoinList.Clear();
		gold = 2000;
	}
	
	void endGame()
	{
		Vector3 teleport = new Vector3(-295, 0, 0);
		foreach(GameObject u in Units)
		{
			u.rigidbody.position += teleport;
		}
	}
	
	void spawnTerrain()
	{
		//print ("Spawned terrain");
		int t1count = 0, t2count = 0, t3count = 0;
		int prevType = 0;
		for(int i = 0; i < 15; i++)
		{
			bool firstit = true;
			bool exit = false;
			int Ttype = Random.Range (1,4);
			if(i == 14)
			{
				if(t1count < 5)
				{
					GameObject temp = (GameObject) Instantiate(Resources.Load("Terrain1"),new Vector3(-80 + 20.0f * i, 10, -20), transform.rotation);
				}
				else if(t2count < 5)
				{
					GameObject temp = (GameObject) Instantiate(Resources.Load("Terrain2"),new Vector3(-80 + 20.0f * i, 10, -20), transform.rotation);
				}
				else if(t3count < 5)
				{
					GameObject temp = (GameObject) Instantiate(Resources.Load("Terrain3"),new Vector3(-80 + 20.0f * i, 10, -20), transform.rotation);
				}
				GameObject temp1 = (GameObject) Instantiate(Resources.Load("Jacuzzi Model"),new Vector3(-75 + 20.0f * i, 12, -20), transform.rotation);
				break;
			}	
			while(!exit)
			{
				if(firstit)
				{
					if(prevType == 1)
					{
						Ttype = Random.Range(2,4);
					}
					else if(prevType == 2)
					{
						Ttype = Random.Range(3,5);
					}
					else if(prevType == 3)
					{
						Ttype = Random.Range(1,3);
					}
					firstit = false;
				}
				if(Ttype == 1 || Ttype == 4)
				{
					if(t1count < 5)
					{
						GameObject temp = (GameObject) Instantiate(Resources.Load("Terrain1"),new Vector3(-80 + 20.0f * i, 10, -20), transform.rotation);
						t1count++;
						exit = true;
						prevType = 1;
					}
					else
					{
						Ttype = Random.Range(2,4);	
					}
				}
				if(Ttype == 2)
				{
					if(t2count < 5)
					{
						GameObject temp = (GameObject) Instantiate(Resources.Load("Terrain2"),new Vector3(-80 + 20.0f * i, 10, -20), transform.rotation);
						t2count++;
						exit = true;
						prevType = 2;
					}
					else
					{
						Ttype = Random.Range(3,5);
					}
				}
				if(Ttype == 3)
				{
					if(t3count < 5)
					{
						GameObject temp = (GameObject) Instantiate(Resources.Load("Terrain3"),new Vector3(-80 + 20.0f * i, 10, -20), transform.rotation);
						t3count++;
						exit = true;
						prevType = 3;
					}
					else
					{
						Ttype = Random.Range(1,3);
					}
				}
			}
		}
	}
}
