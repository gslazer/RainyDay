using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CRainDrop : MonoBehaviour {
	public float maxSpeed;
	public float accel;
	public float velocity;
	float deltaTime;
	bool alive;
	// Use this for initialization
	void Start () {
		alive=true;
		Destroy (gameObject, 1.5f); 
	}
	void OnCollisionEnter2D(Collision2D other){
        if(other.transform.tag == "land"){  //충돌한 tag가 Land 일때
			Debug.Log ("dropped");
            alive=false;
			DestroyObject(gameObject);
        }
         
        if(other.transform.tag == "player"){
           alive=false ; //collision with player
        }
	}
	void OnCollisionStay2D(Collision2D other){
        if(other.transform.tag == "land"){  //충돌한 tag가 Land 일때
			Debug.Log ("dropped");
            alive=false;
			Destroy(gameObject);
        }
         
        if(other.transform.tag == "player"){
           alive=false ; //collision with player
        }
	}
	// Update is called once per frame
	void Update () {
		deltaTime+= ToolManager.deltaTime;
		if(deltaTime>0.01){
			deltaTime=0;
			velocity+=accel;
			if(velocity>maxSpeed)velocity=maxSpeed;
		}
		if(alive)gameObject.rigidbody2D.velocity=new Vector2(0,-1*velocity);
	}
}
