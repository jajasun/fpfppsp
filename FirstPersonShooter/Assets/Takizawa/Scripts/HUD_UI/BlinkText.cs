using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour {
    private Text targetText;

	// Use this for initialization
	void Start () {
        targetText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        targetText.color = new Color(1f, 1f, 1f, Mathf.PingPong(Time.time, 1.5f));
	}
}
