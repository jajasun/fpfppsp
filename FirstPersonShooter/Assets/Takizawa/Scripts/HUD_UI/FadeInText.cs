using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInText : MonoBehaviour {
    private Text targetText;
    private float _alpha;
	// Use this for initialization
	void Start () {
        targetText = GetComponentInChildren<Text>();
        _alpha = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        _alpha += 0.01f;
        targetText.color = new Color(1f, 0f, 0f, _alpha);

        if(Input.GetKeyDown(KeyCode.F))
        {
            Destroy(this.gameObject);
        }
	}
}
