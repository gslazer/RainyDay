using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CPlayer : MonoBehaviour {
	public float maxSpeed;
	public float velocity;
	public float accel;
	public int direction;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		direction=InputManager.direction;
		
		if(direction==0){
			if(velocity<0)
				velocity+=accel;
			if(velocity>0)
				velocity-=accel;
		}
		velocity=velocity+accel*direction;
		if(velocity>maxSpeed)velocity=maxSpeed;
		if(velocity<maxSpeed*-1)velocity=-1*maxSpeed;
		gameObject.rigidbody2D.velocity=new Vector2(velocity,0.0f);
		/*
		Vector3 position= new Vector3();
		position=gameObject.transform.position;
		position.x+=valocity;
		if(position.x<-3) position.x=-3.0f;
		if(position.x>3)position.x=3.0f;
		gameObject.transform.position=position;
		*/
	}
}
