using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicePlayer : MonoBehaviour {
    [SerializeField]
    private VoiceManager _VoiceManager;

    public void PlayVoice(int number)
    {
        _VoiceManager.PlayVoice(number);
    }

    //中断シャッター等で使用
    public void InterruptionVoice()
    {
        _VoiceManager.Interruption();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            //GO!GO!
            if (Input.GetKeyDown(KeyCode.W))
            {
                PlayVoice(0);
            }
            //Help!
            if (Input.GetKeyDown(KeyCode.A))
            {
                PlayVoice(1);
            }
            //MakeFun
            if (Input.GetKeyDown(KeyCode.S))
            {
                PlayVoice(2);
            }
            //Thanks
            if (Input.GetKeyDown(KeyCode.D))
            {
                PlayVoice(3);
            }
        }
    }
}
