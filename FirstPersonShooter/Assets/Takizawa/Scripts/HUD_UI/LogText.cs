using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogText : MonoBehaviour {
    //スコアログの文字
    [SerializeField]
    [Tooltip("自分自身のテキスト")]
    private Text _logText;

    //補間をかけた数字を表示
    private int _displayscore;
    private int _addscore;

    //表示時間
    private float _limitedTime;
    //透明度
    private float _alpha;

    private bool _isNumber;

    void Start () {
        _limitedTime = 1.0f;
    }
	
	void Update () {
        //**移動と透過
        if (_isNumber)
        {
            //数字の表記に補間をかける
            if (_displayscore < _addscore)
            {
                _displayscore = (int)Mathf.Lerp(_displayscore, _addscore, 0.5f);
            }
            string disp = string.Format("+{0:000}", _displayscore);
            _logText.text = disp;
        }
        //スコア表示の補間
        if (_limitedTime > 0.0f)
        {
            //表示
            _logText.color = new Color(1f, 1f, 1f, _alpha);
            //上に移動
            //時間を減らす
            _limitedTime -= Time.deltaTime;
        }
        else
        {
            //上に移動
            Vector3 pos = transform.position;
            pos.y += 0.4f;
            this.transform.position = pos;
            //透過していく
            _alpha -= 0.03f;
            if (_alpha < 0f)
            {
                _displayscore = 0;
                _addscore = 0;
                //オブジェクトの破壊
                Destroy(this.gameObject);
            }
            //テキストに透明度を適応
            _logText.color = new Color(1f, 1f, 1f, _alpha);
        }
    }

    public void DisplayLog(string logText,bool isInteger)
    {
        _isNumber = isInteger;
        //文字の設定
        _logText.text = logText;
        //数字ならintに変換
        if (_isNumber)
        {
            _displayscore = 0;
            _addscore = int.Parse(logText) + 1;
        }
        _limitedTime = 1.0f;
        _alpha = 1.0f;
    }
}
