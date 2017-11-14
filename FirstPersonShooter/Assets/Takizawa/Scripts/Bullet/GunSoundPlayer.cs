using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSoundPlayer : MonoBehaviour {

    [SerializeField]
    private GunSoundManager _Manager;

    //残弾数を取得するため
    [SerializeField]
    private VirtualPlayer _player;


    public void Play(int number,bool looping,float volume)
    {
        _Manager.PlaySE(number,looping,volume);
    }

    //中断シャッター等で使用
    public void Interruption()
    {
        _Manager.Interruption();
    }

    // Update is called once per frame
    void Update()
    {

        //shot
        if (Input.GetMouseButtonDown(0)&&_player.GetAmmo() > 0)
        {
            Play(0, true, 0.8f);
        }
        if(Input.GetMouseButtonUp(0)||_player.GetAmmo()<=0)
        {
            Interruption();
        }
        //reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            Play(1, false,1.0f);
        }
    }
}
