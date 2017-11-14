using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSight : MonoBehaviour {

    [SerializeField]
    private Camera _playerEye;

    private Image _sight;

	// Use this for initialization
	void Start () {
        _sight = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(1))
        {
            _sight.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            _playerEye.fieldOfView = 45f;

        }
        else
        {
            _sight.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            _playerEye.fieldOfView = 60f;
        }
    }
}
