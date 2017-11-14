using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	[SerializeField]
	Vector3 _rotSpeed;
	
	// Update is called once per frame
	void Update ()
	{
		transform.eulerAngles += _rotSpeed * Time.deltaTime;
	}
}
