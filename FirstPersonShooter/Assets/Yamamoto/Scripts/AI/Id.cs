using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id : MonoBehaviour {

    private int enemyId = 0;

	// Use this for initialization
	void Start () {
        Debug.Log("向かう場所:" + enemyId);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DestId(int id)
    {
        enemyId = id;
    }

    public int GetId()
    {
        return enemyId;
    }
}
