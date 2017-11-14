using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;

public class PlayerUnitCore : MonoBehaviour
{
	Rigidbody _body;

	[SerializeField]
	float _speed = 10f;
	[SerializeField]
	CapsuleCollider _collider;

	PlayerUnitAnimationController _animCon;
	PlayerUnitCameraController _camCon;

	private void Awake()
	{
		_animCon = GetComponent<PlayerUnitAnimationController>();
		_camCon = GetComponent<PlayerUnitCameraController>();
	}
	// Use this for initialization
	void Start ()
	{
		//カーソルの無効化
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		_body = GetComponent<Rigidbody>();

	}

	void Update()
	{
		/*
		Ray ray = new Ray(transform.position, Vector3.down);
		RaycastHit hit;
		bool isHit = Physics.Raycast(ray.origin, ray.direction, out hit, 100f, LayerMask.GetMask("Level"));
		if (isHit)
		{
			if(hit.distance>0.3f)
			{
				transform.position = hit.point;
			}
		}
		*/
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		//アニメーションフラグの設定
		var flags = _animCon.flags;
		flags.isIdle = !(MyInput.w || MyInput.a || MyInput.s || MyInput.d || MyInput.leftCtrl);
		flags.isForward = MyInput.w;
		flags.isBack = MyInput.s;
		flags.isStrafe = MyInput.a || MyInput.d;
		flags.isSprint = MyInput.leftShift;
		flags.isCrouch = MyInput.leftCtrl;

		//移動速度の設定
		Vector3 vel = new Vector3();
		if(MyInput.w)
		{
			vel.z += _speed;
		}
		if(MyInput.a)
		{
			vel.x -= _speed;
		}
		if(MyInput.d)
		{
			vel.x += _speed;
		}
		if(MyInput.s)
		{
			vel.z -= _speed;
		}
		if(MyInput.leftShift&&MyInput.w)
		{
			vel = Vector3.zero;
			vel.z += _speed * 1.5f;
		}

		if (_body.velocity.y < 0)
		{
			vel.y = _body.velocity.y * 2f;
		}
		_body.velocity = transform.TransformDirection(vel);
	}
}
