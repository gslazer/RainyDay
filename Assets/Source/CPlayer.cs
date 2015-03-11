using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CPlayer : MonoBehaviour {
	public float maxSpeed;
	float maxSpeedL;
	float maxSpeedR;
	public float velocity;
	public float accel;
	public int direction;
	Animator animator;
	// Use this for initialization
	void Start () {
		
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		direction=InputManager.direction;
		animator.SetInteger ("direction", direction);
		if(direction==0){
			if(velocity<0){
				velocity+=accel;
				if(velocity>0)
					velocity=0;
			}
			if(velocity>0){
				velocity-=accel;
				if(velocity<0)
					velocity=0;
			}
		}
		velocity=velocity+accel*direction;
		maxSpeedL = maxSpeed*-1;
		maxSpeedR = maxSpeed;
		if(velocity>maxSpeedR)velocity=maxSpeedR;
		if(velocity<maxSpeedL)velocity=maxSpeedL;
		gameObject.rigidbody2D.velocity=new Vector2(velocity+ToolManager.wind*maxSpeed*0.5f,0.0f);
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
