using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualBase : MonoBehaviour {

    private int _state;//制圧状況

    private float debugTimer;

	// Use this for initialization
	void Start () {
        debugTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        debugTimer += Time.deltaTime;
        _state = (int)debugTimer % 3;
	}

    public int GetState()
    {
        return _state;
    }

    public void SetState(int argState)
    {
        _state = argState;
    }
}
