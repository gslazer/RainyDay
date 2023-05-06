using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AssemblyCSharp;
using System.IO;
using TMPro;

public class CTGameObject : MonoBehaviour {
	public GameObject cloud;
	public GameObject tCloud;
	public GameObject AdObject;
	public float nextWindTime=10.0f;
	public float nextWindRange=1.0f;
	public float nextWindDelay=5.0f;
	public float gameTime;
	public GameObject startLogo;
	public GameObject resultLogo;
	public TextMeshPro gameTimer;
	public TextMeshPro resultTimer;
	public TextMeshPro highScoreText;
	float deltaTime;
	float cloudTime;
	public float tCloudTime;
	float nextTCloudTime=5.0f;
	float gameStartTime;
	bool resultPopup;
	bool startPopup;
	int intTime;
	int hightScoreInt;
	// Use this for initialization
	void Start () {
		ToolManager.alive=false;
		gameStartTime=Time.time;
		gameTime=0;
		startLogo.SetActive(true);
		startPopup=true;
		ToolManager.setFilePath(SetFilePath());
		setHighScoreText();
		resultLogo.SetActive(false);
		AdObject.SendMessage("Load");
		AdObject.SendMessage("Hide");
		gameTimer.gameObject.SetActive(false);
	}

	void setHighScoreText(){
		ToolManager.readScore();
		hightScoreInt=ToolManager.highScore;
		highScoreText.text=hightScoreInt/100+"."+hightScoreInt%100;
	}
	void registHighScore(){
		if( intTime>hightScoreInt ){
			ToolManager.writeScore(intTime);
			hightScoreInt=intTime;
		}
	}
	public string SetFilePath(){
		string filename="hs.hs";
		if(Application.platform == RuntimePlatform.Android)
		{
			string path = Application.persistentDataPath; 
			path = path.Substring(0, path.LastIndexOf( '/' ) ); 
			return Path.Combine (path, filename);
		} 
		else 
		{
			string path = Application.dataPath; 
			path = path.Substring(0, path.LastIndexOf( '/' ) );
			return Path.Combine (path, filename);
		}
	}
	void setDeltaTime ()
	{
		ToolManager.setDeltaTime(Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		if(ToolManager.alive==false){
			if(startPopup){
				if( ( Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))){
					startLogo.SetActive(false);
					startPopup=false;
					ToolManager.alive=true;
					ToolManager.gameTime=0;
					ToolManager.wind=0;
					gameStartTime=Time.time;
					gameTime=0;
					cloudTime=0;
					tCloudTime=0;
					nextTCloudTime=5.0f;
					nextWindTime=10.0f;
					nextWindRange=1.0f;
					nextWindDelay=5.0f;
					gameTimer.gameObject.SetActive(true);
					Debug.Log("game start!");
					return;
				}
				else return;
			}
			resultLogo.SetActive(true);
			registHighScore();
			//AdObject?.SendMessage("Show");
			resultPopup=true;
			if(intTime%100<10) resultTimer.text=intTime/100+".0"+intTime%100;
			else resultTimer.text=intTime/100+"."+intTime%100;
			if(resultPopup){
				if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)){
					resultLogo.SetActive(false);
					//AdObject?.SendMessage("Hide");
					resultPopup=false;
					startLogo.SetActive(true);
					setHighScoreText();
					gameTimer.gameObject.SetActive(false);
					startPopup=true;
				}
				else return;
			}
			
		}
		
		if(startPopup || resultPopup) return;
		
		InputProcess ();
		upTimer ();

		if(cloudTime>0.1f){
			generateCloud();
			cloudTime=0;
		}
		if (tCloudTime > nextTCloudTime) {
			generateTCloud();
			tCloudTime=0;
			nextTCloudTime=Random.Range (0.5f,5.0f);
		}
		if (gameTime > nextWindTime) {
			WindSwitch();
		}
	}
	private void WindSwitch(){
		if (nextWindDelay < 0) {
			WindStart ();
			nextWindTime+=nextWindRange;
		}
		else if (ToolManager.wind == 0) {
			WindStart ();
			nextWindTime += nextWindRange;
			nextWindRange+=1.0f;
		} 
		else {
			WindStop();
			nextWindTime+=nextWindDelay;
			nextWindDelay-=1.0f;
		}
	}
	private void WindStart(){
		if (Random.Range (0, 2) == 0)
			ToolManager.wind = 1;
		else
			ToolManager.wind = -1;
		
	}
	private void WindStop(){
		ToolManager.wind = 0;
	}
	private void upTimer(){
		setDeltaTime();
		deltaTime=ToolManager.deltaTime;
		gameTime+=deltaTime;
		ToolManager.gameTime=gameTime;
		cloudTime += deltaTime;
		tCloudTime += deltaTime;
		intTime=(int) (gameTime*100);
		//gameTimer.text =intTime/100+"."+intTime%100;
		if(intTime%100<10) gameTimer.text=intTime/100+".0"+intTime%100;
		else gameTimer.text=intTime/100+"."+intTime%100;
		
	}

	public void generateCloud(){
		Vector3 position;
		float x=Random.Range(-3.5f,3.5f);
		float y=5;
		position=new Vector3(x,y,-9);
		Instantiate(cloud,position,Quaternion.Euler(0,0,0));
	}
	public void generateTCloud(){
		Vector3 position;
		float x=Random.Range(-3.5f,3.5f);
		float y=5;
		position=new Vector3(x,y,-10);
		Instantiate(tCloud,position,Quaternion.Euler(0,0,0));
	}

	public void InputProcess(){
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			InputManager.setDirection(-1);
			Debug.Log ("Move Left"+InputManager.direction);
		}
		if(Input.GetKeyUp(KeyCode.LeftArrow) && InputManager.direction==-1){
			InputManager.setDirection(0);
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)){		
			InputManager.setDirection(1);
			Debug.Log ("Move Right"+InputManager.direction);
		}
		if(Input.GetKeyUp(KeyCode.RightArrow) && InputManager.direction==1){
			InputManager.setDirection(0);
		}
		/*
		if(Input.GetMouseButtonDown(0)){
			if(Input.mousePosition.x < Screen.width/2){
				InputManager.setDirection(-1);
				Debug.Log ("Move Left"+InputManager.direction);
			}
			else{
				InputManager.setDirection(1);
				Debug.Log ("Move Right"+InputManager.direction);
			}
		}
		*/
		foreach(Touch t in Input.touches){
			if(t.position.x < Screen.width/2){
				InputManager.setDirection(-1);
				Debug.Log ("Move Left"+InputManager.direction);
			}
			else{
				InputManager.setDirection(1);
				Debug.Log ("Move Right"+InputManager.direction);
			}
		}
		if(Input.GetMouseButtonUp(0)){
			if(Input.mousePosition.x < Screen.width/2){
				if(InputManager.direction==-1)
				InputManager.setDirection(0);
			}
			else{
				if(InputManager.direction==1)
				InputManager.setDirection(0);
			}
		}
	}
}
