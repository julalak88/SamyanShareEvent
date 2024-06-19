using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ScreenSaverUI : MonoBehaviour
{

    public VideoPlayer videoPlayer;

    private void Awake() {
        videoPlayer.url = Application.dataPath + "/../Video/Screensaver_Loop.mp4";
        videoPlayer.prepareCompleted += prepareCompleted;
        videoPlayer.Prepare();
    }

    private void prepareCompleted(VideoPlayer source) {
        videoPlayer.Play();
    }

    //private void OnDisable() {
    //    videoPlayer.Pause();
    //}

    //private void OnEnable() {
    //    videoPlayer.Prepare();
    //}

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            SoundManager.Ins.PlayClick();
            GameManager.Ins.AvatarScene();
        }
    }
}
