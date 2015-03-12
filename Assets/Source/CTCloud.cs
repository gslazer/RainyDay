using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CTCloud : MonoBehaviour {
	public float lifeTime;
	float birthTime;
	float deltaTime=0.0f;
	bool notActedYet;
	SpriteRenderer spriteRenderer; 
	public Sprite afterSprite;
	public GameObject thunder;
	bool alive;
	
	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); 
		birthTime=ToolManager.gameTime;
		//Destroy (gameObject, 1.0f);  
		notActedYet = true;
		alive=true;
	}
	
	// Update is called once per frame
	void Update () {
		if(lifeTime<ToolManager.gameTime-birthTime)Destroy(gameObject);
		deltaTime += Time.deltaTime;
		if (deltaTime > 0.5f && notActedYet) {
			Instantiate (thunder, this.transform.position + new Vector3 (0, -5.0f, 0.5f), Quaternion.Euler (0, 0, 0));
			spriteRenderer.sprite = afterSprite;
			notActedYet=false;
		}
		if(!alive && ToolManager.alive)
			Destroy(gameObject);
		alive=ToolManager.alive;
	}
}
