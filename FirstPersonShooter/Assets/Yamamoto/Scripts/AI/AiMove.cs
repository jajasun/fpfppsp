using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMove : MonoBehaviour {

    // ターゲット(敵か味方か)
    [SerializeField]
    private Transform target = null;
    
    // 目的地(占領地点) ※外部からもらってくる
    [SerializeField]
    private Transform destination;

    // いつもの
    Rigidbody rb;
    // AI
    NavMeshAgent nav;

    // 可視距離
    [SerializeField]
    private float visibleDistance;
    // ターゲットとの距離
    float _targetDistance;

    // 視野角
    public float sightAngle;

    // 目の位置
    public Transform lineOfsight;
    // 目とターゲットを結ぶRay
    Ray gazeRay;

    // 対象レイヤー(ターゲットと壁の判定用)
    [SerializeField]
    private LayerMask visibleLayer;

    // ターゲットを追いかけるのをあきらめる時間
    public float targetLostLimitTime;
    // ターゲットを見つける距離
    public float targetFindDistance;
    float _lostTime = 0f;

    // ただ立っているだけの時間(指示待ち？)
    public float idleMaxTime;
    float _idleTime = 0f;

    // 徘徊している時間
    public float wanderMaxTime;
    float _wanderTime = 0f;

    // 移動速度
    public float runSpeed;
    public float walkSpeed;

    public float thinkTime;

    Vector3 _destUpdate;

    float _time = 0;

    enum eState
    {
        Idle,   // 目的なし(立ち止まって敵を探すだけ)
        Wander, // 周りをさまよう
        Face,   // 目的地に向かう
        Attack, // 攻撃(射撃)　※この間は足を止める
        Dead,   // 死(直球)
    }

    eState state = eState.Idle;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();

        _destUpdate = new Vector3(0,0,0);
    }
	
	// Update is called once per frame
	void Update () {
        switch(state)
        {
            case eState.Idle:
                Idle();
                break;

            case eState.Face:
                GoDest(ElementLeaderManager.Instance.Calc(transform.position));
                break;

            case eState.Attack:
                Attack();
                break;

            case eState.Dead:
                break;
        }

        Debug.Log(state);
	}

    // 立ち止まっているとき
    void Idle()
    {
        Search(state);

        if (destination != null) state = eState.Face;
    }
    // ターゲットを探しているとき
    void Search(eState state)
    {

        if(transform.tag != "TeamA")
        {
            SearchTag(gameObject, "TeamA");
        }
        else
        {
            SearchTag(gameObject, "TeamB");
        }

        if (target == null) return;

        // ターゲットとの角度
        float _angle = Vector3.Angle(target.position - transform.position, lineOfsight.forward);


        // その角度が視野角に入ったら
        if (_angle <= sightAngle)
        {

            gazeRay.origin = lineOfsight.position;
            gazeRay.direction = target.position - lineOfsight.position;

            RaycastHit hits;

            if (Physics.Linecast(transform.position, target.position, out hits))
            {

                //Debug.DrawLine(transform.position, target.position, Color.red);


                if (transform.tag == "TeamA")
                {
                    // ターゲットとの間に障害物がないとき
                    if (hits.collider.gameObject.tag == "TeamB")
                    {
                        if (state == eState.Face || state == eState.Wander || state == eState.Idle)
                        {
                            TargetFound();
                        }
                        else if (state == eState.Attack)
                        {
                            TargetInSight();
                        }
                        return;
                    }
                }
                else
                {
                    // ターゲットとの間に障害物がないとき
                    if (hits.collider.gameObject.tag == "TeamA")
                    {
                        if (state == eState.Face || state == eState.Wander || state == eState.Idle)
                        {
                            TargetFound();
                        }
                        else if (state == eState.Attack)
                        {
                            TargetInSight();
                        }
                        return;
                    }
                }
            }

            Debug.DrawRay(gazeRay.origin, gazeRay.direction * visibleDistance, Color.blue);
        }

        // ここから下は敵の視界内に入らずに敵に近づいたとき
        _targetDistance = (transform.position - target.position).magnitude;

        if (_targetDistance < targetFindDistance)
        {
            if (state == eState.Face || state == eState.Wander)
            {
                TargetFound();
            }
            else if (state == eState.Attack)
            {
                TargetInSight();
            }
            return;
        }
    }

    // ターゲットを見つけたら移動
    void TargetFound()
    {
        nav.isStopped = false;
        state = eState.Attack;

        _idleTime = 0f;
        _lostTime = 0f;
    }

    void TargetInSight()
    {
        Debug.Log("Target In Sight");
        _lostTime = 0f;
    }

    // ターゲットを追いかけるとき
    void Chase()
    {
        // 目的地を設定
        nav.SetDestination(destination.position);

        // 常に敵を探す
        Search(state);

        _lostTime += Time.deltaTime;

        if(_lostTime > targetLostLimitTime)
        {
            state = eState.Idle;
            nav.isStopped = true;
            _lostTime = 0f;
        }
    }

    // 攻撃(射撃)
    void Attack()
    {
        transform.LookAt(target);

        // 移動をやめる
        nav.isStopped = true;

        // 常に敵を探す
        Search(state);

        // ここから攻撃処理　※これからつくる

        // 敵を見失うまで
        _lostTime += Time.deltaTime;

        if (_lostTime > targetLostLimitTime)
        {
            state = eState.Idle;
            nav.isStopped = false;
            _lostTime = 0f;
        }
    }

    // 目的地に向かう
    void GoDest(Transform dest)
    {
        nav.isStopped = false;

        _time += Time.deltaTime;

        if(_time > thinkTime)
        {
            _destUpdate = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            _time = 0;
        }

        nav.SetDestination(dest.position + _destUpdate);

        // 常に敵を探す
        Search(state);
    }

    void SearchTag(GameObject obj, string tagName)
    {

        float _tDis = 0;
        float _nearDis = 0;
        float _objAngle = 0;

        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            // ターゲットとの距離
            _tDis = Vector3.Distance(obs.transform.position, obj.transform.position);

            // ターゲットとの角度
            _objAngle = Vector3.Angle(obs.transform.position - obj.transform.position, lineOfsight.forward);

            if (_objAngle > sightAngle) continue; // 視野角にいないなら

            RaycastHit hit;

            if (Physics.Linecast(obj.transform.position, obs.transform.position, out hit))
            {
                // 当たったオブジェクトが探しているタグでないのなら
                if (hit.collider.gameObject.tag != tagName) continue;

                Debug.DrawLine(obj.transform.position, obs.transform.position, Color.yellow);
            }

            // 最も近いオブジェクトを入れる
            if (_nearDis == 0 || _nearDis > _tDis)
            {
                _nearDis = _tDis;
                target = obs.transform;
            }

            
        }

       

    }

   
}
