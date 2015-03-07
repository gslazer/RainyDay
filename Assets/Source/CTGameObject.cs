using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CTGameObject : MonoBehaviour {
	public GameObject cloud;
	float deltaTime;
	// Use this for initialization
	void Start () {
		generateCloud();
		generateCloud();
		generateCloud();
	}

	void setDeltaTime ()
	{
		ToolManager.setDeltaTime(Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {
		InputProcess ();
		setDeltaTime();
		deltaTime+=ToolManager.deltaTime;
		if(deltaTime>0.1){
			generateCloud();
			deltaTime=0;
		}
	}
	
	public void generateCloud(){
		Vector3 position;
		float x=Random.Range(-3.5f,3.5f);
		float y=5;
		position=new Vector3(x,y,0);
		Instantiate(cloud,position,Quaternion.Euler(0,180,0));
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
