using UnityEngine;

public class ThrowCollide : MonoBehaviour {

    [SerializeField]
    private DamageDirection _dd;

    //位置を合わせる
    [SerializeField]
    private GameObject _player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = _player.transform.position;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            _dd.HitEffect(collision.gameObject);
            //敵を殺したとする
            Debug.Log("スコア管理クラスでスコアに加算");
            HUDManager.Instance.CreateScoreLog("EnemyKill",100);
        }
    }
}
