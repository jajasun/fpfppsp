using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


    //自動削除されるまでの時間
    [SerializeField]
    private float destroyTime = 3;

    //このオブジェクト(Bullet)が生成された瞬間に実行される。
    void Start()
    {
        //このオブジェクトをdestroyTimeで指定した秒後に削除する。
        Destroy(gameObject, destroyTime);
    }

    //何かにぶつかったら実行される。(hit変数内に当たったオブジェクトに関する情報が代入されている。)
    void OnTriggerEnter(Collider hit)
    {

        //このオブジェクト(弾丸)を削除する。
        Destroy(gameObject);

        //もし当たったオブジェクトがEnemyタグである(＝敵である)場合は、敵のDamage関数を実行する。
        if (hit.tag == "Enemy")
        {
            //Damage関数を実行させる。
            hit.SendMessage("Damage");
        }
    }
}
