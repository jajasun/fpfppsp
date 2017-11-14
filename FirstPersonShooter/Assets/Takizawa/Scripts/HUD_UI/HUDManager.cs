using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public static HUDManager _instance;
    public static HUDManager Instance { get { return _instance; } }

    //操作キャラクターのクラス
    [SerializeField]
    private VirtualPlayer _charactor;
    [SerializeField]
    private GameObject _playerCharactor;

    //銃弾の弾数に関する文字
    [SerializeField]
    private Text[] _ammoState = new Text[4];

    //プレイヤーの体力
    [SerializeField]
    private Slider _playerHealth;

    //リロード警告のオブジェクト
    [SerializeField]
    private GameObject _reloadSign;

    //スコアの文字
    [SerializeField]
    private Text _scoreLog;

    //プレイヤーの名札
    [SerializeField]
    private GameObject _nameTag;

    //プレイヤーが死んだときに出る
    [SerializeField]
    private GameObject _deadEffect;

    //親となるHUDのキャンバス
    private GameObject _HUD;

    void Start()
    {
        _instance = this;

        _ammoState[0].text = _charactor.GetAmmo().ToString();
        _ammoState[1].text = _charactor.GetAmmo().ToString();
        _ammoState[2].text = "/∞";
        _ammoState[3].text = "/∞";
        _playerHealth.value = _charactor.GetHealth();

        _HUD = GameObject.Find("HUD");

        CreateNameTag(_playerCharactor, _charactor.GetName(), _charactor.IsBlueTeam());
    }

    void Update() {
        //弾残数の表示
        _ammoState[0].text = _charactor.GetAmmo().ToString();
        _ammoState[1].text = _charactor.GetAmmo().ToString();
        //所持弾数の表示
        //_ammoState[2].text = "/ " + _charactor.GetMaxAmmo().ToString();
        //_ammoState[3].text = "/ " + _charactor.GetMaxAmmo().ToString();
        //警告の表示を切り替える
        if(_charactor.IsReload())
        {
            _reloadSign.SetActive(true);
        }
        else
        {
            _reloadSign.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            DeadHUD();
        }
    }

    //スコア加算の文字を表示
    public void CreateScoreLog(string action, int scoreValue)
    {
        Debug.Log("スコアを加算したいところで呼び出す");
        //表示するテキスト
        Text slog = Instantiate(_scoreLog, Vector3.zero , Quaternion.identity)as Text;
        slog.transform.SetParent(_HUD.transform,false);
        //大きさ、ポジションを修正
        slog.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        slog.transform.localPosition = new Vector3(-30f, -70f, 0f);
        slog.GetComponent<LogText>().DisplayLog(action, false);
        
        //加算するスコア
        Text adds = Instantiate(_scoreLog, Vector3.zero, Quaternion.identity) as Text;
        adds.transform.SetParent(_HUD.transform, false);
        //大きさ、ポジションを修正
        adds.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        adds.transform.localPosition = new Vector3(37f, -67f, 0f);
        adds.GetComponent<LogText>().DisplayLog(scoreValue.ToString(), true);
    }

    public void CreateNameTag(GameObject player,string name,bool isBlueTeam)
    {
        Debug.Log("プレイヤー生成後に名札をつける");
        GameObject obj = Instantiate(_nameTag, player.transform.position, Quaternion.identity);
        obj.transform.parent = player.transform;
        obj.GetComponentInChildren<NameTag>().CreateNameTag(player,name, isBlueTeam);
    }

    public void DeadHUD()
    {
        Debug.Log("プレイヤーが死んだら呼び出す");
        GameObject obj= Instantiate(_deadEffect, new Vector3(0f, 0f, 0f), Quaternion.identity);
        obj.transform.parent = _HUD.transform;
        obj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        obj.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
    }
}