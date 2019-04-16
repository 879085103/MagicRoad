using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickAudio : MonoBehaviour {

    private AudioSource m_AudioSource;
    private ManagerVars vars;

    void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        vars = ManagerVars.GetManagerVars();
        EventCenter.AddListener(EventDefine.PlayAudio, PlayAudio);
        EventCenter.AddListener<bool>(EventDefine.IsMusicOn, IsMusicOn);
    }

    void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.PlayAudio, PlayAudio);
        EventCenter.RemoveListener<bool>(EventDefine.IsMusicOn, IsMusicOn);
    }

    public void PlayAudio()
    {
        m_AudioSource.PlayOneShot(vars.buttonClip);
    }

    private void IsMusicOn(bool isMusicOn)
    {
        m_AudioSource.mute = !isMusicOn;
    }
}
