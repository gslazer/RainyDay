using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CRainDrop : MonoBehaviour {
	public float maxSpeed;
	public float accel;
	public float velocity;
	Vector3 rainy;
	float deltaTime;
	bool alive;
	// Use this for initialization
	void Start () {
		alive=true;
		rainy = new Vector3 (0, 0, 15);
		
	}

	// Update is called once per frame
	void Update () {
		if(!ToolManager.alive){
			gameObject.rigidbody2D.velocity = new Vector2 (0,0);
			alive=false;
			return;
		}
		if(this.transform.position.y<-10.0f)Destroy(gameObject);
		deltaTime+= ToolManager.deltaTime;
	
		rainy=new Vector3(0,0,15*ToolManager.wind);
		gameObject.transform.rotation=Quaternion.Euler (rainy);

		if(deltaTime>0.01){
			deltaTime=0;
			velocity+=accel;
			if(velocity>maxSpeed)velocity=maxSpeed;
		}

		if (alive) {
			gameObject.rigidbody2D.velocity = new Vector2 (0, -1 * velocity);
			if(ToolManager.wind!=0){
				gameObject.rigidbody2D.velocity = new Vector2 (0.26f*velocity*ToolManager.wind, -1 * velocity);
			}
		}
		else Destroy(gameObject);
	}
}
