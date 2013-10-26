using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateTry : MonoBehaviour {
	
	List<GameObject> CoinList = new List<GameObject>();
	List<GameObject> Units  = new List<GameObject>();
	List<GameObject> Terrains = new List<GameObject>();
	GameObject[] HQButts;
	GameObject[] Away;
	float jumpPos;
	float prevVelocity = 10;
	int coinCount = 0;
	int gold = 2000;
	bool paused = true;
	bool swapped = false;
	
	// Use this for initialization
	void Start () 
	{
		HQButts = GameObject.FindGameObjectsWithTag("HQ_Buttons");
		Away = GameObject.FindGameObjectsWithTag("BRB");
		foreach(GameObject brb in Away)
		{
			brb.SetActive(false);
		}
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
			Application.LoadLevel(0);
		}
	}
	
	void Swap()
	{
		//code to swap
		if(Input.GetKeyDown(KeyCode.Space))
		{
			GameObject bob;	
			bob = Units[0];
			bob.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
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
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
			unit.transform.rigidbody.velocity = new Vector3(10,unit.rigidbody.velocity.y,0);
			if(prevVelocity == -10)
			{
				jumpPos = 100000000;
				TurnAround();
				prevVelocity = 10;
				swapped = true;
			}
		}
		if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
		{
			unit.transform.rigidbody.velocity = new Vector3(0,unit.rigidbody.velocity.y,0);
		}
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			unit.transform.rigidbody.velocity = new Vector3(-10,unit.rigidbody.velocity.y,0);
			if(prevVelocity == 10)
			{
				jumpPos = 100000000;
				TurnAround();
				prevVelocity = -10;
				swapped = true;
			}
		}
		if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
		{
			unit.transform.rigidbody.velocity = new Vector3(0,unit.rigidbody.velocity.y,0);
		}
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			if(Units[0].GetComponent<Unit>().Grounded)
			{
				jumpPos = unit.transform.position.x;
				unit.transform.rigidbody.velocity = new Vector3(unit.rigidbody.velocity.x,7,0);
				print("jumpPos is" + jumpPos +"!");
				Units[0].GetComponent<Unit>().Grounded = false;
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
			if(Units[i].rigidbody.position.y <= 11 && Units[i - 1].rigidbody.position.y > 12 && Units[i - 1].rigidbody.velocity.y > 0)
			{
				if(Units[i].GetComponent<Unit>().Grounded)
				{
					Units[i].transform.rigidbody.velocity = new Vector3(Units[i].rigidbody.velocity.x,7,0);
					Units[i].GetComponent<Unit>().Grounded = false;
					print("unit in front y vel = " + Units[i - 1].rigidbody.velocity.y);
				}
			}
		}
	}
	
	void SpawnUnit(int uType)
	{
		if(gold >= 500 && Units.Count < 6 && paused)
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
		if(Units.Count == 1)
		{
			Units[0].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
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
			Units[0].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
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
			IsLeader();
		}
		else
		{
			paused = true;
			Camera.main.transform.position = new Vector3(-94.2477f, 21.29793f, -55.64714f);
			foreach(GameObject o in CoinList)
			{
				Destroy(o);
			}
			CoinList.Clear();
			coinCount = 0;
			foreach(GameObject butt in HQButts)
			{
				butt.SetActive(true);
			}
			foreach(GameObject brb in Away)
			{
				brb.SetActive(false);
			}
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
		GUI.Box (new Rect (Screen.width - 350,Screen.height - 200,100,25), "$ "+ gold);
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
			foreach(GameObject butt in HQButts)
			{
				butt.SetActive(false);
			}
			foreach(GameObject brb in Away)
			{
				brb.SetActive(true);
			}
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
		coinCount = 0;
	}
	
	void endGame()
	{
		Application.LoadLevel(2);
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
