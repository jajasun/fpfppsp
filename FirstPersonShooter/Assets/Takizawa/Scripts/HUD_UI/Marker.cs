using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour {
    //位置のアイコン
    Image[] marker=new Image[16];
    //表示するマーカーリストに追加
    private List<Image> markerList = new List<Image>();
    //マーカーの色を入れ替える
    private Color markerColor;

    //マーカーの画像
    //※青が０、赤が１になるようにする
    [SerializeField]
    private Image markerImage;

    //レーダーの画像
    GameObject compass;

    void Start()
    {
        //マーカーのレーダー上に表示する
        compass = GameObject.Find("Radar");

        //マーカーの生成
        for (int i = 0; i < marker.Length; i++)
        {
            if (i< VirtualGameManager.Instance.GetPlayerNum())
            {
                if (VirtualGameManager.Instance.GetPlayer(i).GetComponentInChildren<VirtualPlayer>().IsBlueTeam())
                {
                    markerImage.color = new Color(0f, 0f, 0.6f, 1f);
                }
                else
                {
                    markerImage.color = new Color(0.6f, 0f, 0f, 1f);
                }
            }
            marker[i] = Instantiate(markerImage, compass.transform.position, Quaternion.identity) as Image;
            marker[i].transform.SetParent(compass.transform, false);

            marker[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        for (var i = 0; i < VirtualGameManager.Instance.GetPlayerNum(); i++)
        {
            if (Vector3.Distance(transform.position, VirtualGameManager.Instance.GetPlayer(i).transform.position) <= 50)
            {
                if (!marker[i].gameObject.activeSelf)
                {
                    marker[i].gameObject.SetActive(true);
                }
                //位置設定
                Vector3 position = VirtualGameManager.Instance.GetPlayer(i).transform.position - transform.position;
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