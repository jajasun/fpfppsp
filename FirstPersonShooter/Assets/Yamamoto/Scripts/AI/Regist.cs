using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regist : MonoBehaviour {

    const int DEF_ENEMY = 20;

    [SerializeField]
    private GameObject aTeamEnemy; // 生成したいプレハブ
    [SerializeField]
    private Transform aTeamBase; // 指定のスポーン位置
    [SerializeField]
    private GameObject bTeamEnemy; // 生成したいプレハブ
    [SerializeField]
    private Transform bTeamBase; // 指定のスポーン位置

    [SerializeField]
    private float maxRange;
    [SerializeField]
    private float minRange;

    [SerializeField]
    private int idNum = 1; // 初期登録するIDナンバー

    // ID登録時の区切り
    public int idPunctuation = 4;

    // 同じIDで登録した数
    int _num = 0;

    // 生成座標
    Vector3 _genePos;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        // スペースで生成 ※今後特定範囲内にランダム生成に変更
		if(Input.GetKeyDown(KeyCode.Space))
        {
            // 敵の生成
            GameObject enemy = Instantiate(aTeamEnemy);
            
            // 敵のID ※IDは最初に目指す目的地番号
            Id id = enemy.GetComponent<Id>();

            // IDの登録
            id.DestId(idNum);

            // 登録回数
            _num++;

            // 区切り数と登録数が一致するかどうか
            if (_num != idPunctuation) return;

            // IDの番号を増やす
            idNum++;

            // 登録回数のリセット
            _num = 0;
        }




	}

    private void OnValidate()
    {
        // 登録数の区切りは1から5まで
        idPunctuation = Mathf.Clamp(idPunctuation, 1, 5);

        maxRange = Mathf.Clamp(maxRange, 0, 10);
        minRange = Mathf.Clamp(minRange, -10, 0);
    }
}
