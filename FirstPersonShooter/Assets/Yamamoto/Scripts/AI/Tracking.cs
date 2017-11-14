using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour {

    // 追いかける対象(リーダー)
    [SerializeField]
    private GameObject TrackObj;

    // 攻撃対象
    [SerializeField]
    private GameObject Target;

    private RaycastHit hit;
    bool isHit;

    


    // Use this for initialization
    void Start () {
        Target = null;
	}
	
	// Update is called once per frame
	void Update () {
        // 行動

        // リーダーを設定(死亡時は新しいリーダーを設定)

        // リーダーの指示に従って行動
        // 1.リーダーを追いかける？
        // 2.指定拠点占領

        // レイを設定
        isHit = Physics.Raycast(transform.position, transform.forward, out hit,10);


        // 見つけたらターゲットに設定
        if (isHit)
        {
            Target = hit.transform.gameObject;
            Debug.DrawRay(transform.position + Vector3.up, transform.forward * hit.distance, Color.red, 1, false);
        }
        else
        {
            // デバッグ用の可視化されたレイ
            Debug.DrawRay(transform.position + Vector3.up, transform.forward * 10, Color.red, 1, false);
        }




        // もし敵を見つけていないときに銃によるダメージを受けたらターゲットに設定(特定範囲内のみ)
        if (Target == null) return;

        // ターゲットが設定されているならターゲットを撃つ
        Shoot();

        // ターゲットが死んだらNULLに
        
    }

    void Shoot() {

    }
}
