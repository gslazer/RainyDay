using UnityEngine;
using System.Collections;

public class CCloud : MonoBehaviour {
	float deltaTime;
	public GameObject rainDrop;
	// Use this for initialization
	void Start () {
		Instantiate(rainDrop,this.transform.position+new Vector3(0,-1,0),Quaternion.Euler(0,180,0));
		Destroy (gameObject, 1.0f);  
	}
	
	// Update is called once per frame
	void Update () {
	}
}
