using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {


    bool isDead;


    // Use this for initialization
    void Start () {
        isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    bool IsDead() { return isDead; }
    void SetDead(bool dead) { isDead = dead; }
}
