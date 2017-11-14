using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualGameManager : MonoBehaviour {
    public static VirtualGameManager _instance;
    public static VirtualGameManager Instance { get { return _instance; } }

    //経過時間
    private float _timer;

    //ゲーム内の目標チケット数
    private int _goalTicket;

    //青チームのチケット数
    private int _blueTicket;
    [SerializeField]
    private Text _blueTicketText;
    [SerializeField]
    private Slider _blueTicketSlider;

    //赤チームのチケット数
    private int _redTicket;
    [SerializeField]
    private Text _redTicketText;
    [SerializeField]
    private Slider _redTicketSlider;

    //ステージ上にいるプレイヤー
    [SerializeField]
    private List<GameObject> _players=new List<GameObject>();

    [SerializeField]
    private List<GameObject> _bases = new List<GameObject>();

    void Awake()
    {
        _instance = this;
        
        //チケット数の初期化
        _goalTicket = 1000;
        _blueTicket = 0;
        _redTicket = 0;

        //スライダーの設定
        _blueTicketSlider.maxValue = _goalTicket;
        _redTicketSlider.maxValue = _goalTicket;
    }
	
	// Update is called once per frame
	void Update () {
        //HUDに現在のチケット数を反映させる
        _blueTicketText.text = _blueTicket.ToString();
        _blueTicketSlider.value = _blueTicket;
        _redTicketText.text = _redTicket.ToString();
        _redTicketSlider.value = _redTicket;

        //デバッグ用処理
        if(Input.GetKeyDown(KeyCode.T))
        {
            if (_blueTicket <= _goalTicket)
                _blueTicket += 2;

            if (_redTicket <= _goalTicket)
                _redTicket++;
        }
	}

    //プレイヤーの数を取得する
    public int GetPlayerNum()
    {
        return _players.Count;
    }

    //ゲーム内にプレイヤーを追加する
    public void AddPlayer(GameObject argPlayer)
    {
        _players.Add(argPlayer);
    }

    //指定した配列番号のプレイヤーを取得する(GameObject型)
    public GameObject GetPlayer(int number)
    {
        return _players[number];
    }

    //拠点の数を取得する
    public int GetBaseNum()
    {
        return _bases.Count;
    }

    //ゲーム内の拠点を追加する
    public void AddBase(GameObject argPlayer)
    {
        _bases.Add(argPlayer);
    }

    //指定した拠点を取得する(GameObject型)
    public GameObject GetBase(int number)
    {
        return _bases[number];
    }
}
