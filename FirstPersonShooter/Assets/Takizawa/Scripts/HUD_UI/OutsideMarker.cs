using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutsideMarker : MonoBehaviour {

    //位置のアイコン
    private GameObject[] marker=new GameObject[4];

    //マーカーの画像
    [SerializeField]
    private GameObject markerImage;

    //レーダーの画像
    GameObject compass;

    //拠点の位置
    private GameObject[] bases=new GameObject[4];

	void Start () {
        compass = GameObject.Find("Radar");
        //マーカーの初期化
        for (int i = 0; i < marker.Length; i++)
        {
            marker[i] = Instantiate(markerImage, new Vector3(0f, 0f, 0f), Quaternion.identity);
            marker[i].transform.SetParent(compass.transform, false);
        }

        //拠点位置の取得
        for (int i = 0; i < bases.Length; i++)
        {
            bases[i] = VirtualGameManager.Instance.GetBase(i);
        }
    }
	
	void Update () {
        for (int i = 0; i < bases.Length; i++)
        {
            //レーダーの範囲外なら表示する
            if (Vector3.Distance(transform.position, bases[i].transform.position) >= 50f)
            {
                if (!marker[i].activeSelf)
                {
                    marker[i].SetActive(true);
                }
                //2点間の角度を求める
                var targetRot = Quaternion.LookRotation(bases[i].transform.position - transform.position);
                var requiredRot = targetRot * Quaternion.Inverse(transform.rotation);
                var requiredDegree = requiredRot.eulerAngles;
                marker[i].transform.rotation = Quaternion.Euler(0f, 0f, -requiredDegree.y);
            }
            else
            {
                //近づいたら消す
                if (marker[i].activeSelf)
                {
                    marker[i].SetActive(false);
                }
            }
        }
    }

    float RadToDegree(float degree)
    {
        if (degree <= 180)
        {
            return degree / 180.0f * Mathf.PI;
        }
        else
        {
            return (degree - 360) / 180.0f * Mathf.PI;
        }
    }
}
