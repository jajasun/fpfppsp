using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementLeader : MonoBehaviour
{




    // 指示先（トランスフォーム）
    private Transform _targetPoint;

    // 所属チーム
    public Team Team { get; set; }


    // 目標達成
    private bool _success;

    // マネージャー
    ElementLeaderManager _elementManager;

    // LeaderのAI
    public int ID { get; set; }


    // 仮
    Vector3 pos;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        pos = new Vector3(Random.Range(0, 130), 0, Random.Range(0, 130));
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// Managerに走らせるためのやーつ
    /// </summary>
    public void Run()
    {
        pos = new Vector3(Random.Range(0, 130), 0, Random.Range(0, 130));

        

        // ターゲット指定されていて,目標達成した場合
        if (_targetPoint && _success)
        {
            _targetPoint = null;
        }
        else
        {

        }
    }


    


}
