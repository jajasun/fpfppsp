using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField]
    [Tooltip("何分間のゲームか")]
    private float time;

    private float second;//残り時間の秒数

    private Text timerText;

	void Start () {
        second = time * 60.0f;
        timerText = GetComponentInChildren<Text>();
    }
	
	void Update () {
        if (second >= 0)
        {
            second -= Time.deltaTime;
        }
        timerText.text = string.Format("{0:00}:{1:00}", (int)second / 60, (int)second % 60);
    }
}
