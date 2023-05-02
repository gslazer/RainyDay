using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CIGTimerScaler : MonoBehaviour {
	public bool useOffset;
	float ratioX;
	float ratioY;
    Text text;
	public float offsetX;
	public float offsetY;
	public int fontSize;
	// Use this for initialization
	void Start () {
		ratioX=Screen.width/720.0f;
		ratioY=Screen.height/1280.0f;
		text= GetComponent<Text>();
		//if(useOffset)text.pi
				//pixelOffset=new Vector2( offsetX*ratioX,offsetY*ratioY);
		text.fontSize= (int) (fontSize*ratioY)*1;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
