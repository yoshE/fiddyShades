using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class GameStateTry : MonoBehaviour {
	
	List<GameObject> CoinList = new List<GameObject>();
	List<GameObject> Units  = new List<GameObject>();
	List<GameObject> Terrains = new List<GameObject>();
	float jumpPos;
	float prevVelocity = 10;
	int coinCount = 0;
	int gold = 0;
	// Use this for initialization
	void Start () {
		SpawnUnit(1);
		SpawnUnit(2);
		SpawnUnit(3);
		SpawnUnit(2);
		IsLeader();
		OnGUI ();
	}
	
	// Update is called once per frame
	void Update () {
		if(coinCount<300){
			spawnCoin ();
		}
		if(Units.Count > 0)
		{
			MoveLeader(Units[0]);
			MoveFollower();
			for (int i = 0; i < CoinList.Count -1 ; i++){
				colCheck (CoinList[i]);
			}
		}
	}
	
	void MoveLeader(GameObject unit){
		//code to swap
		if(Input.GetKeyDown(KeyCode.Space))
		{
			GameObject bob;	
			bob = Units[0];
			float tempPosX = bob.rigidbody.position.x;
			if(prevVelocity==10){
				for (int i = 0; i < Units.Count -1 ; i++){
					Units[i] = Units[i+1];
					Units[i].rigidbody.position = new Vector3(tempPosX,Units[i].rigidbody.position.y,-20);
					tempPosX = (Units[i].rigidbody.position.x - 1.5f);
					print(tempPosX);
				}
				bob.rigidbody.position = new Vector3(tempPosX,bob.rigidbody.position.y,-20);
				Units[Units.Count-1] = bob;
			}
			else{
				for (int i = 0; i < Units.Count -1 ; i++){
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
		
		//leader moving code
		if(Input.GetKey(KeyCode.D)){
			unit.transform.rigidbody.velocity = new Vector3(10,unit.rigidbody.velocity.y,0);
			if(prevVelocity == -10)
			{
				jumpPos = 100000000;
				TurnAround();
				prevVelocity = 10;
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
			}
		}
		if(Input.GetKeyUp(KeyCode.A))
		{
			unit.transform.rigidbody.velocity = new Vector3(0,unit.rigidbody.velocity.y,0);
		}
		if(Input.GetKey(KeyCode.W))
		{
			if(unit.rigidbody.position.y <= 11)
			{
				jumpPos = unit.transform.position.x;
				unit.transform.rigidbody.velocity = new Vector3(unit.rigidbody.velocity.x,7,0);
				print("jumpPos is" + jumpPos +"!");
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
	
	void SpawnUnit(int uType){
		if(uType == 1)
		{
			GameObject temp = (GameObject) Instantiate(Resources.Load("Unit1"),new Vector3(-85 - 1.5f * Units.Count, 11, -20), transform.rotation);
			Units.Add(temp);
		}
		else if(uType == 2)
		{
			GameObject temp = (GameObject) Instantiate(Resources.Load("Unit2"),new Vector3(-85 - 1.5f * Units.Count, 11, -20), transform.rotation);
			Units.Add(temp);
		}
		else if(uType == 3)
		{
			GameObject temp = (GameObject) Instantiate(Resources.Load("Unit3"),new Vector3(-85 - 1.5f * Units.Count, 11, -20), transform.rotation);
			Units.Add(temp);
		}
	}
	
	void TurnAround(){
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
		Units[0].SendMessage("IsActiveUnit");	
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
		for(int i = 0; i < Units.Count; i++)
		{
			Units[i].SendMessage("InvulnerableOn");
		}
		StartCoroutine("InvulnerableTime");
	}
	
	private IEnumerator InvulnerableTime()
	{
		yield return new WaitForSeconds(2);
		for(int i = 0; i < Units.Count; i++)
		{
			Units[i].SendMessage("InvulnerableOff");
		}
		print ("Invulnerable Time Over!");
		Units[0].SendMessage("IsActiveUnit");
	}
	
	void spawnCoin()
	{
		if(prevVelocity==10){
			GameObject temp = (GameObject)Instantiate(Resources.Load("Coin"),new Vector3(Units[0].rigidbody.position.x + Random.Range (-1f,6f), 35, -20), transform.rotation);
			CoinList.Add(temp);
		}else{
			GameObject temp = (GameObject)Instantiate(Resources.Load("Coin"),new Vector3(Units[0].rigidbody.position.x + Random.Range (-4f,1f), 35, -20), transform.rotation);
			CoinList.Add(temp);
			coinCount++;
		}
		
	}
	
	void colCheck(GameObject coin1){
		if(Mathf.Abs(coin1.rigidbody.position.x - Units[0].rigidbody.position.x) <1){
			if(coin1.rigidbody.position.y <13){
				gold++;
				coin1.rigidbody.position = new Vector3(Units[0].rigidbody.position.x + Random.Range (-2f,5f), 35, -20);
				
					
			}
		}
		if(Mathf.Abs(coin1.rigidbody.position.y) > 90){
				coin1.rigidbody.position = new Vector3(Units[0].rigidbody.position.x + Random.Range (-7f,7f), 35, -20);
		}
		
	}
	void OnGUI(){
		GUI.Box (new Rect (10,Screen.height - 25,100,25), "$ "+ gold);
	}

}
