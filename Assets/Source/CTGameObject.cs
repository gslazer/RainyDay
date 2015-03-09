using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CTGameObject : MonoBehaviour {
	public GameObject cloud;
	public GameObject tCloud;
	float deltaTime;
	float cloudTime;
	float tCloudTime;
	float nextTCloudTime=5.0f;
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
	}

	private void upTimer(){
		setDeltaTime();
		deltaTime=ToolManager.deltaTime;
		cloudTime += deltaTime;
		tCloudTime += deltaTime;
	}

	public void generateCloud(){
		Vector3 position;
		float x=Random.Range(-3.5f,3.5f);
		float y=5;
		position=new Vector3(x,y,0);
		Instantiate(cloud,position,Quaternion.Euler(0,180,0));
	}
	public void generateTCloud(){
		Vector3 position;
		float x=Random.Range(-3.5f,3.5f);
		float y=5;
		position=new Vector3(x,y,-1);
		Instantiate(tCloud,position,Quaternion.Euler(0,180,0));
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
