using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CTCloud : MonoBehaviour {
	float deltaTime=0.0f;
	bool notActedYet;
	SpriteRenderer spriteRenderer; 
	public Sprite afterSprite;
	public GameObject thunder;

	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); 
		Destroy (gameObject, 1.0f);  
		notActedYet = true;
	}
	
	// Update is called once per frame
	void Update () {
		deltaTime += Time.deltaTime;
		if (deltaTime > 0.5f && notActedYet) {
			Instantiate (thunder, this.transform.position + new Vector3 (0, -5.0f, 0.5f), Quaternion.Euler (0, 180, 0));
			spriteRenderer.sprite = afterSprite;
			notActedYet=false;
		}
	}
}
