using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideInHUD : MonoBehaviour {
    [SerializeField]
    private GameObject[] slideInContents;
    //移動制御に用いる変数
    private float purpose;

    private float dilayTimer;

    float pos_x=0f;

    // Use this for initialization
    void Start () {
		for(int i=0;i<slideInContents.Length;i++)
        {
            slideInContents[i].transform.localPosition = new Vector3(0f, slideInContents[i].transform.localPosition.y, 0f);
        }
	}
	
	// Update is called once per frame
	void Update () {
        //数字の表記に補間をかける
        for (int i = 0; i < slideInContents.Length; i++)
        {
            if (slideInContents[i].transform.localPosition.x != purpose)
            {
                pos_x = (float)Mathf.Lerp(pos_x, purpose, 0.2f);
                slideInContents[i].transform.localPosition = new Vector3(pos_x, slideInContents[i].transform.localPosition.y, 0f);
            }
        }

        if(Input.GetKey(KeyCode.Tab))
        {
            purpose = 100.0f;
        }
        else
        {
            purpose = 0f;
        }
    }
}
