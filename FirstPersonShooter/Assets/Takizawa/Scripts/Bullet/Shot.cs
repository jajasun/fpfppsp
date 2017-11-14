using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;//弾丸のプレハブ

    [SerializeField]
    GameObject muzzle;

    private VirtualPlayer player;

    private float range = 100f;

    private RaycastHit hit;

    private int interbal;//銃を撃つインターバル

    // Use this for initialization
    void Start()
    {
        player = GetComponent<VirtualPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        DebugDrawRay();

        //もしもクリックボタンが押されたら
        if (Input.GetMouseButton(0))
        {
            interbal++;
            if (interbal % 10 == 0&&player.GetAmmo()>0)
                ShotBullet();


            if(interbal>=10000000)
            {
                interbal = 0;
            }
        }

        //リロード処理
        if(Input.GetKeyDown(KeyCode.R))
        {
            Invoke("Reload", 3.0f);
        }
    }

    void DebugDrawRay()
    {
        //画面中央のスクリーン座標を取得
        Vector3 cameraCenter = new Vector3(Screen.width / 2f+0.08f, Screen.height / 2f, 0f);

        //Raycastで飛ばす光線を作成(Mainカメラの中央部分から飛ばす)
        Ray ray = Camera.main.ScreenPointToRay(cameraCenter);

        //Rayを表示するためのデバッグ機能（Gameビューには表示されない）
        Debug.DrawRay(ray.origin, ray.direction * range);
    }

    public void ShotBullet()
    {
        player.SetAmmo(player.GetAmmo() - 1);

        //画面中央のスクリーン座標を取得
        Vector3 cameraCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        //Raycastで飛ばす光線を作成(Mainカメラの中央部分から飛ばす)
        Ray ray = Camera.main.ScreenPointToRay(cameraCenter);

        GameObject bul=Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.identity);
        bul.GetComponent<Rigidbody>().AddForce(ray.direction * 8000.0f);

        //Raycastを上記で設定した光線でrange分の距離だけ飛ばす。
        if (Physics.Raycast(ray, out hit, range))
        {
            //Raycastで何かヒットしたら実行される。
            if (hit.transform.tag == "Player")
            {
                //Enemyタグの時のみ実行される。
                //Enemyタグを持つオブジェクトにDamage関数を実行する。
                Debug.Log("hit");
                //hit.transform.SendMessage("Damage");
            }
        }
    }

    public void Reload()
    {
        player.SetAmmo(40);
    }
}