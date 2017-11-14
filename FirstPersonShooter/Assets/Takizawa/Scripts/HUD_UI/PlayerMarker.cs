using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMarker : MonoBehaviour {
    //位置のアイコン
    Image[] marker = new Image[4];

    //マーカーの画像
    [SerializeField]
    private Image markerImage;

    //レーダーの画像
    GameObject compass;

    void Start()
    {
        //マーカーのレーダー上に表示する
        compass = GameObject.Find("Radar");
        markerImage.color = new Color(0.1f, 0.4f, 0f, 1f);

        //マーカーの生成
        for (int i = 0; i < marker.Length; i++)
        {
            marker[i] = Instantiate(markerImage, compass.transform.position, Quaternion.identity) as Image;
            marker[i].transform.SetParent(compass.transform, false);
        }
    }

    void Update()
    {
        for (var i = 0; i < VirtualGameManager.Instance.GetBaseNum(); i++)
        {
            if (Vector3.Distance(transform.position, VirtualGameManager.Instance.GetBase(i).transform.position) <= 50)
            {
                if (!marker[i].gameObject.activeSelf)
                {
                    marker[i].gameObject.SetActive(true);
                }
                //位置設定
                Vector3 position = VirtualGameManager.Instance.GetBase(i).transform.position - transform.position;
                marker[i].transform.localPosition = new Vector3(position.x, position.z, 0);
            }
            else
            {
                //マーカーを消す
                marker[i].gameObject.SetActive(false);
            }
        }
    }
}
