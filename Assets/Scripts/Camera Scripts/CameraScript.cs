using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public static float offsetX;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (BirdScripts.instance != null) {
			if(BirdScripts.instance.isAlive){
				MoveTheCamera();
			}
		}
	}

	void MoveTheCamera(){
		Vector3 temp = transform.position;
		temp.x = BirdScripts.instance.GetPositionX() + offsetX;
		transform.position = temp;

	}
}
