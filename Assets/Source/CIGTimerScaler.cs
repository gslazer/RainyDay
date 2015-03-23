using UnityEngine;
using System.Collections;

public class CIGTimerScaler : MonoBehaviour {
	float ratioX;
	float ratioY;
	GUIText text;
	public float offsetX;
	public float offsetY;
	public int fontSize;
	// Use this for initialization
	void Start () {
		ratioX=Screen.width/720.0f;
		ratioY=Screen.height/1280.0f;
		text= GetComponent<GUIText>();
		text.pixelOffset=new Vector2( offsetX*ratioX,offsetY*ratioY);
		text.fontSize= (int) (fontSize*ratioY)*1;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
