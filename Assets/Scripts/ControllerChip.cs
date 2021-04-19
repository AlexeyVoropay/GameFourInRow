using UnityEngine;
using System.Collections;

public class ControllerChip : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(
			new Vector3(
			Input.mousePosition.x,
			Input.mousePosition.y,
			-Camera.main.transform.position.z)
			);
		//Instantiate(RedChip, new Vector3(2, 2, 0), Quaternion.identity);
		float posX = mousePos.x + 4f;
		
		float fl = 8f/6f;
		if(posX <0)
			posX = 0;
		else if(posX>8)
			posX = 8;
		else			
			if((posX)%fl>fl/2f)				
				posX = (float)((int)(posX/fl+1)*fl);
		else posX = (float)((int)(posX/fl)*fl);
		
		posX =posX-4.1f;		
		mousePos.x = posX;
		mousePos.y = transform.position.y;
		transform.position = mousePos;
	}
}
