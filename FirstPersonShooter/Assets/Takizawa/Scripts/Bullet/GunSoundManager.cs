using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSoundManager : MonoBehaviour {

    [SerializeField]
    private AudioClip[] m_audioClip;

    private AudioSource m_audioSource;

    // Use this for initialization
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySE(int number,bool looping,float volume)
    {
        m_audioSource.clip = m_audioClip[number];
        m_audioSource.loop = looping;
        m_audioSource.volume = volume;
        m_audioSource.Play();
    }

    public void Interruption()
    {
        m_audioSource.Stop();
    }
}
