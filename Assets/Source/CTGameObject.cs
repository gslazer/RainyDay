using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CTGameObject : MonoBehaviour {
	public GameObject cloud;
	public GameObject tCloud;
	public float nextWindTime=10.0f;
	public float nextWindRange=1.0f;
	public float nextWindDelay=5.0f;
	public float gameTime;
	float deltaTime;
	float cloudTime;
	float tCloudTime;
	float nextTCloudTime=5.0f;
	float gameStartTime;
	// Use this for initialization
	void Start () {
		ToolManager.alive=false;
		gameStartTime=Time.time;
		gameTime=0;
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
			if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)){
				ToolManager.alive=true;
				ToolManager.gameTime=0;
				ToolManager.wind=0;
				gameStartTime=Time.time;
				gameTime=0;
				cloudTime=0;
				tCloudTime=0;
				nextWindTime=10.0f;
				nextWindRange=1.0f;
				nextWindDelay=5.0f;
				Debug.Log("game start!");
			}
			else return;
		}
		
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
		
	}

	public void generateCloud(){
		Vector3 position;
		float x=Random.Range(-3.5f,3.5f);
		float y=5;
		position=new Vector3(x,y,0);
		Instantiate(cloud,position,Quaternion.Euler(0,0,0));
	}
	public void generateTCloud(){
		Vector3 position;
		float x=Random.Range(-3.5f,3.5f);
		float y=5;
		position=new Vector3(x,y,-1);
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
