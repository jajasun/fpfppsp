using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadSign : MonoBehaviour {
    //リロード警告のスプライト
    [SerializeField]
    private Image[] _reloadSprite = new Image[2];
    private float sumTime = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.active)
        {
            Blink();
        }
	}

    //リロード警告のスプライトを点滅させる。
    public void Blink()
    {
        float length = 1f;

        sumTime += Time.deltaTime;
        float spColor = Mathf.PingPong(sumTime * length / 1.0f * 2, length);//引数(時間×最大値÷間隔 , 最大値)

        _reloadSprite[0].color = new Color(spColor, spColor, spColor);
        _reloadSprite[1].color = new Color(1.0f - spColor, 1.0f - spColor, 1.0f - spColor);
    }
}
