using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualPlayer : MonoBehaviour {

    [SerializeField]
    string _playerName="Player";

    //プレイヤーの体力
    private float _health=50.0f;

    //銃に装填された弾薬数
    private int _ammo=40;

    //ストックした弾薬数
    private int _maxAmmo=90;

    //チームの識別。青ならtrue
    [SerializeField]
    private bool _isBlueTeam;

    // Update is called once per frame
    void Update () {

	}

    public void SetName(string name)
    {
        _playerName = name;
    }

    public string GetName()
    {
        return _playerName;
    }

    public float GetHealth()
    {
        return _health;
    }

    public int GetAmmo()
    {
        return _ammo;
    }

    public void SetAmmo(int argAmmo)
    {
        _ammo = argAmmo;
    }

    public int GetMaxAmmo()
    {
        return _maxAmmo;
    }

    //リロードが必要かどうか
    public bool IsReload()
    {
        if (_ammo <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsBlueTeam()
    {
        return _isBlueTeam;
    }
}
