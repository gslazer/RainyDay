using UnityEngine;
using System.Collections;
using AssemblyCSharp;
public class CThunder : MonoBehaviour {
	bool alive;
	float deltaTime;
	public float lifeTime;
	// Use this for initialization
	void Start () {	
		deltaTime=0.0f;
		alive=true;
	}
	
	// Update is called once per frame
	void Update () {
		deltaTime+= ToolManager.deltaTime;
		if(!ToolManager.alive){
			alive=false;
			return;
		}
		if(alive)
			if(deltaTime>lifeTime)Destroy(gameObject);
		if(!alive && ToolManager.alive) Destroy(gameObject);
		alive=ToolManager.alive;
	}
}
