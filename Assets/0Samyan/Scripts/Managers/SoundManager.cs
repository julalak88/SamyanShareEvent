using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.SoundManagerNamespace;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Ins = null;

    public AudioSource BGM;
    public AudioSource[] audios;

    private void Awake() {
        Ins = this;
    }

    public void PlayBGM() {
        BGM.PlayLoopingMusicManaged(.5f, 0, true);
    }

    public void PlayPopup() {
        audios[0].PlayOneShotSoundManaged(audios[0].clip);
    }

    public void PlaySelectAvatar() {
        audios[1].PlayOneShotSoundManaged(audios[1].clip);
    }

    public void PlayChoosed() {
        audios[2].PlayOneShotSoundManaged(audios[2].clip);
    }

    public void PlaySelect() {
        audios[3].PlayOneShotSoundManaged(audios[3].clip);
    }

    public void PlayClick() {
        audios[4].PlayOneShotSoundManaged(audios[4].clip);
    }

    public void PlaySelect2() {
        audios[5].PlayOneShotSoundManaged(audios[5].clip);
    }
}
