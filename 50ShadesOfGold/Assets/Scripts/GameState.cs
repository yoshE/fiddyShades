using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;

public class GameState : MonoBehaviour {
	
	List<GameObject> CoinList = new List<GameObject>();
	List<GameObject> Units  = new List<GameObject>();
	List<GameObject> Terrains = new List<GameObject>();
	public List<GameObject> Boulders = new List<GameObject>();
	Vector3 showerPos = new Vector3(-100,35,-20);
	GameObject[] HQButts;
	GameObject[] Away;
	float jumpPos;
	float prevVelocity = 10;
	int coinCount = 0;
	int gold = 3000;
	int collectedGold = 0;
	int numDeath = 0;
	int numUnitDeath = 0;
	int unitsBought = 0;
	public bool paused = false;
	bool deathPause = false;
	bool swapped = false;
	bool started = false;
	bool shopping = false;
	bool touchingRight = false;
	bool touchingLeft = false;
	//bool playerSwapTrue = false;
	float ptempx, ptempy;
	public Stopwatch CountDownTimer = new Stopwatch();
	float totalTime = 90.0f;
	int timeLeft = 0;
	int timetrack = 0;
	GUIStyle style;
	
	// Use this for initialization
	void Start () 
	{
		string dateTime = System.DateTime.Now.ToString() + Environment.NewLine; 	//Get the time to tack on to the file name
		dateTime = dateTime.Replace ("/", "-"); 			//Replace slashes with dashes, because Unity thinks they are directories..
		string fileName = "Data.txt";
		//string fileName = "Resources/Data.txt";
		File.AppendAllText(fileName, dateTime);
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
		if(!paused && started && !deathPause)
		{
			if(CoinList.Count < 400){
				spawnCoin ();
			}
			//print ("READ THIS: "+Units[0].GetComponent<Unit>().rigidbody.velocity.y);
			if(Units.Count > 0)
			{
				UpdateCamera();
				MoveLeader(Units[0]);
				MoveFollower();
				for (int i = 0; i < CoinList.Count -1 ; i++){
					colCheck (CoinList[i]);
				}
				if(Units.Count == 1)
				{
					ShowHint();
				}
			}
			Swap();
		}
		if(timetrack >= 240){
			Time.timeScale = 1.0f;
			deathPause = false;
			timetrack = 0;
			Camera.main.transform.position = new Vector3(-94.2477f, 21.29793f, -55.64714f);
		}else if(Time.timeScale > 1.0f){
			timetrack++;
		}
	}
	
	void ShowHint()
	{
		GameObject[] hint = GameObject.FindGameObjectsWithTag("ShopHere");
		foreach(GameObject o in hint)
		{
			Destroy(o);
		}
		GameObject temp = (GameObject) Instantiate(Resources.Load("UseShop"),new Vector3(Units[0].transform.position.x, 20, -20), transform.rotation);
		if(Units.Count != 1)
		{
			foreach(GameObject o in hint)
			{
				Destroy(o);
			}
		}
	}
	
	void Swap()
	{
		//code to swap
		if(Input.GetKeyDown(KeyCode.Space))
		{
			//playerSwapTrue = true;
			GameObject bob;	
			bob = Units[0];
			float tempPosX = bob.rigidbody.position.x;
			float tempPosY = bob.rigidbody.position.y;
			float tempPosY2 = bob.rigidbody.position.y;
			float verticalSpeed = bob.rigidbody.velocity.y;
			float horizSpeed = bob.rigidbody.velocity.x;
			//print ("Bob X: " + tempPosX + " and Bob Y: " + tempPosY);
			//print ("UNIT[0] X: " + Units[0].rigidbody.position.x );
			if(!(Units.Count == 1)){
				bob.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);	
			}
			if(prevVelocity==10)
			{

					for (int i = 0; i < Units.Count -1 ; i++)
					{
						if(i == 0){
							tempPosY2 = Units[Units.Count-1].rigidbody.position.y;
							Units[i] = Units[i+1];
							IsLeader ();
							Units[i].rigidbody.position = new Vector3(tempPosX,tempPosY,-20);
							Units[i].rigidbody.velocity = new Vector3(bob.rigidbody.velocity.x, verticalSpeed, 0);
							tempPosX = (Units[i].rigidbody.position.x - 1.5f);
							//print(tempPosX);
						}else{
							Units[i] = Units[i+1];
							Units[i].rigidbody.position = new Vector3(tempPosX,Units[i].rigidbody.position.y,-20);
							//print ("UNIT FOR LOOP Y: " + Units[i].rigidbody.position.y);
							tempPosX = (Units[i].rigidbody.position.x - 1.5f);
							//print(tempPosX);
						}
					}
					//print ("UNIT Y: " + Units[Units.Count-1].rigidbody.position.y);
					//print ("Not Changed Y: " + tempPosY2);
					bob.rigidbody.position = new Vector3(tempPosX,tempPosY2,-20);
					Units[Units.Count-1] = bob;
					Units[0].GetComponent<Unit>().Grounded = false;
				
				}
			else
			{
					for (int i = 0; i < Units.Count -1 ; i++)
					{
						if(i == 0){
							tempPosY2 = Units[Units.Count-1].rigidbody.position.y;
							Units[i] = Units[i+1];
							IsLeader ();
							Units[i].rigidbody.position = new Vector3(tempPosX,tempPosY,-20);
							Units[i].rigidbody.velocity = new Vector3(bob.rigidbody.velocity.x, verticalSpeed, 0);
							tempPosX = (Units[i].rigidbody.position.x + 1.5f);
							//print(tempPosX);
						}else{
							Units[i] = Units[i+1];
							Units[i].rigidbody.position = new Vector3(tempPosX,Units[i].rigidbody.position.y,-20);
							//print ("UNIT FOR LOOP Y: " + Units[i].rigidbody.position.y);
							tempPosX = (Units[i].rigidbody.position.x + 1.5f);
							//print(tempPosX);
						}
					}
					//print ("UNIT -Y: " + Units[Units.Count-1].rigidbody.position.y);
					//print ("Not Changed -Y: " + tempPosY2);
					bob.rigidbody.position = new Vector3(tempPosX,tempPosY2,-20);
					Units[Units.Count-1] = bob;
					Units[0].GetComponent<Unit>().Grounded = false;
			}
			IsFollower();
		}	
	}
	
	void MoveLeader(GameObject unit)
	{
		//leader moving code
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			if(!touchingRight)
			{
				unit.transform.rigidbody.velocity = new Vector3(10,unit.rigidbody.velocity.y,0);
			}
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
			touchingRight = false;
		}
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			if(!touchingLeft)
			{
				unit.transform.rigidbody.velocity = new Vector3(-10,unit.rigidbody.velocity.y,0);
			}
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
			touchingLeft = false;
		}
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			/*if(playerSwapTrue && Units[0].GetComponent<Unit>().rigidbody.velocity.y == 0){
				playerSwapTrue = false;	
				Units[0].GetComponent<Unit>().Grounded = true;
			}*/
			if(Units[0].GetComponent<Unit>().rigidbody.velocity.y == 0 && Units[0].GetComponent<Unit>().rigidbody.position.y <= 11.2){
				Units[0].GetComponent<Unit>().Grounded = true;
			}
			if(Units[0].GetComponent<Unit>().Grounded)
			{
				Units[0].GetComponent<Unit>().jumped ();
				jumpPos = unit.transform.position.x;
				unit.transform.rigidbody.velocity = new Vector3(unit.rigidbody.velocity.x,7,0);
				//print("jumpPos is" + jumpPos +"!");
				Units[0].GetComponent<Unit>().Grounded = false;
			}
		}
	}
	
	void TouchingWall(float direction)
	{
		if(direction == 1)
		{
			touchingRight = true;
		}
		else if(direction == -1)
		{
			touchingLeft = true;
		}
		else
		{
			touchingRight = false;
			touchingLeft = false;
		}
	}

	void MoveFollower(){
		for(int i = 1; i < Units.Count; i++)
		{
			/*
			if(prevVelocity == 10){
				Units[i].transform.rigidbody.position = new Vector3(Units[0].rigidbody.position.x - i * 1.5f,Units[i].rigidbody.position.y,-20);
			}
			if(prevVelocity == -10){
				Units[i].transform.rigidbody.position = new Vector3(Units[0].rigidbody.position.x + i * 1.5f,Units[i].rigidbody.position.y,-20);
			}
			 public Transform startMarker;
    public Transform endMarker;
    private float startTime;
    private float journeyLength;
    public Transform target;
    void Start() {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }
    void Update() {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
    }
			*/
			//float speed = 5.0f;
			//Vector3 start = Units[i].transform.position;
			if(prevVelocity == 10){
				if(Units[i].transform.position.x < Units[0].transform.position.x - i * 1.5f)
				{
					Units[i].rigidbody.velocity = new Vector3(10,Units[i].rigidbody.velocity.y,0);
				}
				else 
				{
					Units[i].rigidbody.velocity = new Vector3(0,Units[i].rigidbody.velocity.y,0);
				}
				if(Units[0].transform.position.x - i * 1.5f - Units[i].transform.position.x > 2)
				{
					Units[i].transform.position = new Vector3(Units[0].transform.position.x - i * 1.5f,Units[i].rigidbody.position.y,-20);
				}
			}
			if(prevVelocity == -10){
				if(Units[i].transform.position.x > Units[0].rigidbody.position.x + i * 1.5f)
				{
					Units[i].rigidbody.velocity = new Vector3(-10,Units[i].rigidbody.velocity.y,0);
				}
				else 
				{
					Units[i].rigidbody.velocity = new Vector3(0,Units[i].rigidbody.velocity.y,0);
				}
				if(Units[i].transform.position.x - Units[0].rigidbody.position.x + i * 1.5f > 2)
				{
					Units[i].transform.position = new Vector3(Units[0].transform.position.x + i * 1.5f,Units[i].rigidbody.position.y,-20);
				}
			}
			if(Units[i].rigidbody.position.y <= 11 && Units[i - 1].rigidbody.position.y > 12 && Units[i - 1].rigidbody.velocity.y > 0)
			{
				if(Units[i].GetComponent<Unit>().Grounded)
				{
					Units[i].transform.rigidbody.velocity = new Vector3(Units[i].rigidbody.velocity.x,7,0);
					Units[i].GetComponent<Unit>().Grounded = false;
					//print("unit in front y vel = " + Units[i - 1].rigidbody.velocity.y);
				}
			}
		}
	}
	
	void SpawnUnit(int uType)
	{
		if(gold >= 500 && Units.Count < 6)
		{
			if(Units.Count > 0 && !started)
			{
				foreach(GameObject o in Units)
				{
					o.rigidbody.position += new Vector3(1.5f,0,0);
					o.SendMessage("Spawning");
				}
			}
			if(uType == 1)
			{
				GameObject temp = (GameObject) Instantiate(Resources.Load("Unit1"),new Vector3(-88, 11, -20), transform.rotation);
				Units.Add(temp);
				unitsBought++;
				gold -= 500;
			}
			else if(uType == 2)
			{
				GameObject temp = (GameObject) Instantiate(Resources.Load("Unit2"),new Vector3(-88, 11, -20), transform.rotation);
				Units.Add(temp);
				unitsBought++;
				gold -= 500;
			}
			else if(uType == 3)
			{
				GameObject temp = (GameObject) Instantiate(Resources.Load("Unit3"),new Vector3(-88, 11, -20), transform.rotation);
				Units.Add(temp);
				unitsBought++;
				gold -= 500;
			}
			else if(gold >= 1000 && started && shopping)
			{
				//Time.timeScale = 1.0f;
				if(uType == 4)
				{
					if(prevVelocity == 10)
					{
						GameObject temp = (GameObject) Instantiate(Resources.Load("Unit1"),new Vector3(Units[Units.Count - 1].rigidbody.position.x - 1.5f, 35, -20), transform.rotation);
						Units.Add(temp);
						gold -= 1000;
					}
					else if(prevVelocity == -10)
					{
						GameObject temp = (GameObject) Instantiate(Resources.Load("Unit1"),new Vector3(Units[Units.Count - 1].rigidbody.position.x + 1.5f, 35, -20), transform.rotation);
						Units.Add(temp);
						gold -= 1000;
					}
				}
				else if(uType == 5)
				{
					if(prevVelocity == 10)
					{
						GameObject temp = (GameObject) Instantiate(Resources.Load("Unit2"),new Vector3(Units[Units.Count - 1].rigidbody.position.x - 1.5f, 35, -20), transform.rotation);
						Units.Add(temp);
						gold -= 1000;
					}
					else if(prevVelocity == -10)
					{
						GameObject temp = (GameObject) Instantiate(Resources.Load("Unit2"),new Vector3(Units[Units.Count - 1].rigidbody.position.x + 1.5f, 35, -20), transform.rotation);
						Units.Add(temp);
						gold -= 1000;
					}
				}
				else if(uType == 6)
				{
					if(prevVelocity == 10)
					{
						GameObject temp = (GameObject) Instantiate(Resources.Load("Unit3"),new Vector3(Units[Units.Count - 1].rigidbody.position.x - 1.5f, 35, -20), transform.rotation);
						Units.Add(temp);
						gold -= 1000;
					}
					else if(prevVelocity == -10)
					{
						GameObject temp = (GameObject) Instantiate(Resources.Load("Unit3"),new Vector3(Units[Units.Count - 1].rigidbody.position.x + 1.5f, 35, -20), transform.rotation);
						Units.Add(temp);
						gold -= 1000;
					}
				}
				unitsBought++;
				//Time.timeScale = 0;
			}
		}
		/*if(Units.Count == 1)
		{
			Units[0].transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
		}
		*/
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
	
	void LeaderDied(GameObject leader)
	{
		leader.GetComponent<Unit>().died();
		//Units[0].SendMessage("died");
		numUnitDeath++;
		string output2 = "Unit Died at X-Position: " + Units[0].rigidbody.position.x + Environment.NewLine;
		string fileName2 = "Data.txt";
		File.AppendAllText(fileName2, output2);
		Units.RemoveAt(0);
		Destroy(leader);
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
			prevVelocity = 10;

			showerPos = new Vector3(-80,35,-20);
			started = false;
			numDeath++;
			CountDownTimer.Stop();
			Time.timeScale= 1.2f;
			print("DEATH!");
			deathPause = true;
			//Camera.main.transform.position = new Vector3(-94.2477f, 21.29793f, -55.64714f);
			foreach(GameObject o in CoinList)
			{
				Destroy(o);
			}
			CoinList.Clear();
			coinCount = 0;
			foreach(GameObject o in Boulders)
			{
				Destroy(o);
			}
			Boulders.Clear();
			GameObject[] hint = GameObject.FindGameObjectsWithTag("ShopHere");
			foreach(GameObject o in hint)
			{
				Destroy(o);
			}
			foreach(GameObject butt in HQButts)
			{
				butt.SetActive(true);
			}
			foreach(GameObject brb in Away)
			{
				brb.SetActive(false);
			}
			if(gold < 500)
			{
				string output = "Group Deaths: " + numDeath + "   Total Unit Deaths: " + numUnitDeath + Environment.NewLine + "Total Units Bought: " + unitsBought + Environment.NewLine + "Coins Collected: " + collectedGold + Environment.NewLine + "Time Left: " + timeLeft + Environment.NewLine;
				string fileName = "Data.txt";
				//string fileName = "Resources/Data.txt";
				File.AppendAllText(fileName, output);
				Application.LoadLevel(3);
			}
		}
	}
	
	private IEnumerator InvulnerableTime()
	{
		if(Units.Count > 0)
		{
			yield return new WaitForSeconds(1);
			for(int i = 0; i < Units.Count; i++)
			{
				Units[i].SendMessage("InvulnerableOff");
			}
			//print ("Invulnerable Time Over!");
		}
	}
	
	void colCheck(GameObject coin1)
	{
		if(Mathf.Abs(coin1.rigidbody.position.x - Units[0].rigidbody.position.x) <1){
			if(Mathf.Abs(coin1.rigidbody.position.y - Units[0].rigidbody.position.y) <2)
			{

				gold += 3;
				collectedGold += 3;
				if(showerPos.x < Units[0].rigidbody.position.x){
					//showerPos.x = Units[0].rigidbody.position.x;
					showerPos.x += 5;
					coin1.rigidbody.position = new Vector3(Units[0].rigidbody.position.x + UnityEngine.Random.Range (5f,15f)+ Mathf.Abs(Units[0].rigidbody.velocity.x), 35, -20);
				}else{
					coin1.rigidbody.position = new Vector3(showerPos.x + UnityEngine.Random.Range (5f,15f)+ Mathf.Abs(Units[0].rigidbody.velocity.x), 35, -20);	
				}
				coin1.rigidbody.velocity = new Vector3(0,0,0);
			}
		}
		if(Mathf.Abs(coin1.rigidbody.position.x - Units[0].rigidbody.position.x) > 40){
			if(showerPos.x < Units[0].rigidbody.position.x){
				//showerPos.x = Units[0].rigidbody.position.x;
				showerPos.x += 5;
				coin1.rigidbody.position = new Vector3(Units[0].rigidbody.position.x + UnityEngine.Random.Range (0f,12f)+ Mathf.Abs(Units[0].rigidbody.velocity.x), 35, -20);
			}
			coin1.rigidbody.velocity = new Vector3(0,0,0);
		}
		if(Mathf.Abs(coin1.rigidbody.position.y) < 0 || Mathf.Abs(coin1.rigidbody.position.y) > 90)
		{
			if(showerPos.x < Units[0].rigidbody.position.x){
				//showerPos.x = Units[0].rigidbody.position.x;
				showerPos.x += 5;
				coin1.rigidbody.position = new Vector3(Units[0].rigidbody.position.x + UnityEngine.Random.Range (-3f,20f)+ Mathf.Abs(Units[0].rigidbody.velocity.x), 35, -20);
			}else{
				coin1.rigidbody.position = new Vector3(showerPos.x + UnityEngine.Random.Range (-3f,20f)+ Mathf.Abs(Units[0].rigidbody.velocity.x), 35, -20);
			}
			coin1.rigidbody.velocity = new Vector3(0,0,0);
		}
		for( int i = 1; i < Units.Count; i++){
			if(Mathf.Abs(coin1.rigidbody.position.x - Units[i].rigidbody.position.x) <1){
			if(Mathf.Abs(coin1.rigidbody.position.y - Units[i].rigidbody.position.y) <2)
			{
				//print ("GOTCHA ");
				gold++;
				if(showerPos.x < Units[0].rigidbody.position.x){
					//showerPos.x = Units[0].rigidbody.position.x;
					showerPos.x += 5;
					coin1.rigidbody.position = new Vector3(Units[0].rigidbody.position.x + UnityEngine.Random.Range (5f,25f)+ Mathf.Abs(Units[0].rigidbody.velocity.x), 35, -20);
				}else{
					coin1.rigidbody.position = new Vector3(showerPos.x + UnityEngine.Random.Range (5f,25f)+ Mathf.Abs(Units[0].rigidbody.velocity.x), 35, -20);	
				}
				coin1.rigidbody.velocity = new Vector3(0,0,0);
			}
		}
		}
	}
	
	void OnGUI()
	{
		GUIStyle style = new GUIStyle(GUI.skin.label);
		int intFont = 40;
		GUI.skin.label.fontSize = intFont;
		GUI.skin.label.alignment = TextAnchor.UpperCenter;
		//style.alignment = TextAnchor.UpperCenter;
		GUI.color = Color.yellow;
		GUI.Label (new Rect (Screen.width/2 - 100,Screen.height/5,200,100), "$ "+ gold);

		timeLeft = (int)totalTime - (int)(CountDownTimer.ElapsedMilliseconds/1000.0f);
		if(timeLeft > 0)
		{
			GUI.skin.label.fontSize = intFont;
			GUI.color = Color.red;
			GUI.Label (new Rect (Screen.width/2 - 200,Screen.height/9,400,100), "Seconds Left: "+ timeLeft);
		}
		else
		{
			GUI.Box (new Rect (Screen.width - 175,100,150,25), "Seconds Left: "+ 0);
			string output = "Group Deaths: " + numDeath + "   Total Unit Deaths: " + numUnitDeath + Environment.NewLine + "Total Units Bought: " + unitsBought + Environment.NewLine + "Coins Collected: " + collectedGold + Environment.NewLine + "Time Left: " + timeLeft + "    - Ran out of Time" + Environment.NewLine;
			string fileName = "Data.txt";
			//	string fileName = "Resources/Data.txt";
			File.AppendAllText(fileName, output);
			Application.LoadLevel(3);
		}
		GUI.color = Color.white;
		if(GUI.Button(new Rect(Screen.width - 150,20,100,60), "Pause")) {
			if(!paused)
			{
				CountDownTimer.Stop();
				Vector3 pos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 10);
				paused = true;
				GameObject tempWall = Instantiate(Resources.Load("Faded"), pos, transform.rotation) as GameObject;
				GameObject tempTxt = Instantiate(Resources.Load("PausedTxt"), new Vector3(pos.x, pos.y + 1.5f, pos.z - 3), transform.rotation) as GameObject;
				GameObject tempRestart = (GameObject)Instantiate(Resources.Load("RestartTxt"), new Vector3(pos.x, pos.y- 3, pos.z - 3), transform.rotation);
				GameObject tempResume = (GameObject)Instantiate(Resources.Load("ResumeTxt"), new Vector3(pos.x, pos.y - 9, pos.z - 3), transform.rotation);
				GameObject tempMain = (GameObject)Instantiate(Resources.Load("MainMenuTxt"), new Vector3(pos.x, pos.y - 6, pos.z - 3), transform.rotation);
				string output = "Group Deaths: " + numDeath + "   Total Unit Deaths: " + numUnitDeath + Environment.NewLine + "Total Units Bought: " + unitsBought + Environment.NewLine + "Coins Collected: " + collectedGold + Environment.NewLine + "Time Left: " + timeLeft + Environment.NewLine + "|| Paused, may go to Main Menu if no more ||" + Environment.NewLine;
				string fileName = "Data.txt";
				File.AppendAllText(fileName, output);
				Time.timeScale = 0.0f;
			}
			else if(paused && !shopping)
			{
				Unpause ();
			}
		}
		if(GUI.Button(new Rect(Screen.width - 255,20,100,60), "Shop")) {
			if(!paused)
			{
				Time.timeScale = 0.0f;
				shopping = true;
				CountDownTimer.Stop();
				Vector3 pos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 10);
				paused = true;
				GameObject tempWall = Instantiate(Resources.Load("Faded"), pos, transform.rotation) as GameObject;
				GameObject tempButton1 = (GameObject)Instantiate(Resources.Load("Unit1ShopBut"), new Vector3(pos.x-8, pos.y, pos.z - 3), transform.rotation);
				GameObject tempButton2 = (GameObject)Instantiate(Resources.Load("Unit2ShopBut"), new Vector3(pos.x, pos.y, pos.z - 3), transform.rotation);
				GameObject tempButton3 = (GameObject)Instantiate(Resources.Load("Unit3ShopBut"), new Vector3(pos.x+8, pos.y, pos.z - 3), transform.rotation);
				GameObject tempMoney = (GameObject) Instantiate(Resources.Load("1000ea"), new Vector3(pos.x, pos.y - 5, pos.z - 3), transform.rotation);
				GameObject tempResume = (GameObject)Instantiate(Resources.Load("ShopExitTxt"), new Vector3(pos.x, pos.y - 10, pos.z - 3), transform.rotation);
				//Time.timeScale = 0.0f;
				GameObject[] hint = GameObject.FindGameObjectsWithTag("ShopHere");
				if(Units.Count == 1)
				{
					foreach(GameObject o in hint)
					{
						Destroy(o);
					}
				}
			}
			else if(paused && shopping)
			{
				Unshop ();
			}
		}
	}
	
	public void Unpause()
	{
		paused = false;
		GameObject tempWall = GameObject.Find("Faded(Clone)");
		GameObject tempTxt = GameObject.Find("PausedTxt(Clone)");
		GameObject tempRestart = GameObject.Find("RestartTxt(Clone)");
		GameObject tempResume = GameObject.Find("ResumeTxt(Clone)");
		GameObject tempMain = GameObject.Find ("MainMenuTxt(Clone)");
		Destroy (tempWall);
		Destroy (tempTxt);
		Destroy (tempRestart);
		Destroy (tempResume);
		Destroy (tempMain);
		if(started)
		{
			CountDownTimer.Start();
		}
		Time.timeScale = 1.0f;
	}
	
	public void Unshop()
	{
		paused = false;
		shopping = false;
		GameObject tempWall = GameObject.Find("Faded(Clone)");
		GameObject tempButton1 = GameObject.Find("Unit1ShopBut(Clone)");
		GameObject tempButton2 = GameObject.Find("Unit2ShopBut(Clone)");
		GameObject tempButton3 = GameObject.Find("Unit3ShopBut(Clone)");
		GameObject tempExit = GameObject.Find("ShopExitTxt(Clone)");
		GameObject tempMoney = GameObject.Find("1000ea(Clone)");
		Destroy (tempWall);
		Destroy (tempButton1);
		Destroy (tempButton2);
		Destroy (tempButton3);
		Destroy (tempExit);
		Destroy (tempMoney);
		if(started)
		{
			CountDownTimer.Start();
		}
		Time.timeScale = 1.0f;
	}
	
	void spawnCoin()
	{
		if(Units.Count > 0)
		{
			if(prevVelocity==10){
				GameObject temp = (GameObject)Instantiate(Resources.Load("Coin"),new Vector3(Units[0].rigidbody.position.x + UnityEngine.Random.Range (-0f,10f)+ Mathf.Abs(Units[0].rigidbody.velocity.x), 35, -20), new Quaternion(UnityEngine.Random.Range(-4.0f,4.0f),UnityEngine.Random.Range(-4.0f,4.0f),UnityEngine.Random.Range(-4.0f,4.0f),UnityEngine.Random.Range(-5.0f,5.0f)));
				CoinList.Add(temp);
			}
			else
			{
				GameObject temp = (GameObject)Instantiate(Resources.Load("Coin"),new Vector3(Units[0].rigidbody.position.x + UnityEngine.Random.Range (-0f,7f)+ Mathf.Abs(Units[0].rigidbody.velocity.x), 35, -20), new Quaternion(UnityEngine.Random.Range(-4.0f,4.0f),UnityEngine.Random.Range(-4.0f,4.0f),UnityEngine.Random.Range(-4.0f,4.0f),UnityEngine.Random.Range(-5.0f,5.0f)));
				CoinList.Add(temp);
				coinCount++;
			}
		}
	}
	
	void UpdateCamera()
	{
		if(swapped && ((Mathf.Abs(Camera.main.transform.position.x - Units[0].rigidbody.position.x))<= 1.0)){
			Camera.main.transform.position = new Vector3(Units[0].rigidbody.position.x, 21.38423f,-46.61178f);
			swapped = false;
			//print("CAUGHT ~ : " + Mathf.Abs(Camera.main.transform.position.x - Units[0].rigidbody.position.x));
		}
		else if(swapped && prevVelocity == -10){
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - 0.4f, 21.38423f,-46.61178f);
			//Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - (Camera.main.transform.position.x - Units[0].rigidbody.position.x)/15, 21.38423f,-46.61178f);
			//print ("CHANGE LEFT");
			//print(Mathf.Abs(Camera.main.transform.position.x - Units[0].rigidbody.position.x));
		}
		else if(swapped && prevVelocity == 10){
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + 0.4f, 21.38423f,-46.61178f);
			//Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + (Units[0].rigidbody.position.x - Camera.main.transform.position.x)/15, 21.38423f,-46.61178f);
			//print ("CHANGE RIGHT");
			//print(Mathf.Abs(Camera.main.transform.position.x - Units[0].rigidbody.position.x));
		}
		else{
			Camera.main.transform.position = new Vector3(Units[0].rigidbody.position.x, 21.38423f,-46.61178f);
			swapped = false;
			//print ("CHECKING>>>");
		}
	}
	
	void StartQuest()
	{
		if(Units.Count > 0)
		{
			started = true;
			IsLeader();
			foreach(GameObject butt in HQButts)
			{
				butt.SetActive(false);
			}
			foreach(GameObject brb in Away)
			{
				brb.SetActive(true);
			}
			CountDownTimer.Start();
			foreach(GameObject o in Units)
			{
				o.SendMessage("Begin");
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
		prevVelocity = 10;
		Units.Clear();
		CoinList.Clear();
		gold = 3000;
		coinCount = 0;
		Time.timeScale = 1.0f;
		InvokeRepeating("spawnBoulders", 1.5f, 2.0f);
		showerPos = new Vector3(-80,35,-20);
	}
	
	void endGame()
	{
		/*string dateTime = System.DateTime.Now.ToString (); 	//Get the time to tack on to the file name
		dateTime = dateTime.Replace ("/", "-"); 			//Replace slashes with dashes, because Unity thinks they are directories..
		string fileName = "Metrics_" + dateTime;			//Append file name
		string output= "Death: " + numDeath + "\n";
		FileStream fs = File.Create ("Assets/Metrics/" + fileName + ".txt"); 	//Need to close this after so something else (StreamWriter) can access it
		fs.Close ();	//Close it!
		sw = new StreamWriter ("Assets/Metrics/" + fileName + ".txt");	//Create a StreamWriter which can write onto the file
		sw.WriteLine (output);	//Write line
		sw.Close ();	//Close access to file*/
		string output = "Group Deaths: " + numDeath + "   Total Unit Deaths: " + numUnitDeath + Environment.NewLine + "Total Units Bought: " + unitsBought + Environment.NewLine + "Coins Collected: " +collectedGold + Environment.NewLine  + "Time Left: " + timeLeft + Environment.NewLine;
		string fileName = "Data.txt";
		//string fileName = "Resources/Data.txt";
		File.AppendAllText(fileName, output);
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
			int Ttype = UnityEngine.Random.Range (1,4);
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
						Ttype = UnityEngine.Random.Range(2,4);
					}
					else if(prevType == 2)
					{
						Ttype = UnityEngine.Random.Range(3,5);
					}
					else if(prevType == 3)
					{
						Ttype = UnityEngine.Random.Range(1,3);
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
						Ttype = UnityEngine.Random.Range(2,4);	
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
						Ttype = UnityEngine.Random.Range(3,5);
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
						Ttype = UnityEngine.Random.Range(1,3);
					}
				}
			}
		}
	}
	
	void spawnBoulders()
	{
		GameObject temp = (GameObject) Instantiate(Resources.Load("Boulder"),new Vector3(191, 25, -20), transform.rotation);
		temp.rigidbody.velocity = new Vector3(UnityEngine.Random.Range(-20, -15), 0, 0);
		Boulders.Add (temp);
	}
	
	void UndoUnit()
	{
		if(Units.Count > 0)
		{
			gold += 500;
			Vector3 tempV = Units[0].rigidbody.position;
			GameObject temp = Units[Units.Count - 1];
			Units.Remove(temp);
			Destroy (temp);
			for(int i = Units.Count - 1; i > -1; i--)
			{
				print("back up");
				Units[i].rigidbody.position -= new Vector3(1.5f,0,0);
			}
		}
	}
	
	void OnApplicationQuit(){
		//print ("QUIT");
		string dateTime = System.DateTime.Now.ToString (); 	//Get the time to tack on to the file name
		dateTime = dateTime.Replace ("/", "-"); 			//Replace slashes with dashes, because Unity thinks they are directories..
		string Name = "Metrics_" + dateTime;			//Append file name
		string output= "Group Deaths: " + numDeath + "   Total Unit Deaths: " + numUnitDeath + Environment.NewLine + "Total Units Bought: " + unitsBought + Environment.NewLine + "Coins Collected: " + collectedGold + Environment.NewLine + "Time Left: " + timeLeft + Environment.NewLine + "Ended from Game" + Environment.NewLine + Environment.NewLine;
		string fileName = "Data.txt";
		//string fileName = "Resources/Data.txt";
		File.AppendAllText(fileName, output);
	}
}
