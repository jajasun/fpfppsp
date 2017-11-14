using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameTag : MonoBehaviour {

    [SerializeField]
    private Text _name;

    private GameObject _target;

    [SerializeField]
    private Texture _textureRed;

    [SerializeField]
    private Texture _textureBlue;

    [SerializeField]
    private Image _teamTag;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y + 2.2f, _target.transform.position.z);
        this.transform.rotation = _target.transform.rotation;
	}

    public void CreateNameTag(GameObject target,string name,bool isBlueTeam)
    {
        _name.text = name;
        _target = target;
        if(isBlueTeam)
        {
            //軍旗を青に
            _teamTag.material.mainTexture = _textureBlue;
        }
        else
        {
            //軍旗を赤に
            _teamTag.material.mainTexture = _textureRed;
        }

    }
}
