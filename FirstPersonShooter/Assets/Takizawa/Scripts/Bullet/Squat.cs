using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.7f, this.transform.position.z);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.7f, this.transform.position.z);
        }
    }
}
