using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLineCast : MonoBehaviour {

    public Transform target;

    LayerMask layerMask;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float angle = 80f;

        float hitAngle = Vector3.Angle(target.position - transform.position, transform.forward);

        Debug.DrawLine(transform.position, target.position, Color.red);

        if(hitAngle <= angle)
        {
            RaycastHit hit;

            if (Physics.Linecast(transform.position,target.position,out hit))
            {

                if(hit.collider.tag == "TeamB")
                {
                    Debug.Log("見つかってる");
                }
                else
                {
                    Debug.Log("見つかってないよ");
                }
                
            }
        }
        else
        {
            Debug.Log("見つかってないよ");
        }

    }
}
