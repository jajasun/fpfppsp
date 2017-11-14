using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDirection : MonoBehaviour {

    [SerializeField]
    private Camera _player;

    private float _angle;

    private float _limitedTime=0.0f;

    //中身の絵
    [SerializeField]
    private Image _imageArrow;
    private float _alpha = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(_limitedTime>0.0f)
        {
            //表示
            _imageArrow.color = new Color(0.6f, 0f, 0f, 1f);
            //回転
            this.transform.rotation = Quaternion.Euler(0f, 0f, -_angle);
            //時間を減らす
            _limitedTime -= Time.deltaTime ;
        }
        else
        {
            _alpha -= 0.01f;
            _imageArrow.color = new Color(0.6f, 0f, 0f, _alpha);
        }
    }

    float Map360To180(float degree)
    {
        if(degree<180)
        {
            return degree;
        }
        return  degree-360;
    }

    //ヒットエフェクトを表示する
    //引数：プレイヤーのコライダーに当たったオブジェクト
    //※プレイヤークラスで呼び出す
    public void HitEffect(GameObject argTarget)
    {
        //表示時間を設定する
        _limitedTime = 1.0f;
        _alpha = 1.0f;

        //角度を取得する
        var targetRot = Quaternion.LookRotation(argTarget.transform.position - _player.transform.position);
        var requiredRot = targetRot * Quaternion.Inverse(_player.transform.rotation);
        var requiredDegree = requiredRot.eulerAngles;
        _angle = requiredDegree.y;
    }
}
