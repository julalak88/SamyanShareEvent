using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Ins = null;

    public GameObject charactor;
    public Transform p1, p2;
    public ScreenSaverUI screenSaverUI;
    public AvatarUIManager avatarUI;
    public InterestUI interestUI;
    public FinishUI finishUI;
    public float delayTime = 10;

    float cc = 0;
    bool _countdown = false;
    public bool StartCountdown {
        set {
            _countdown = value;
            cc = 0;
        }
        get {
            return _countdown;
        }
    }
    
    [HideInInspector]
    public ItemData itemData;

    QueueManager queue;
    bool _mouse = false;

    private void Awake() {
        Ins = this;
        
        queue = GetComponent<QueueManager>();
    }

    private void Start() {

        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
        if (Display.displays.Length > 2)
            Display.displays[2].Activate();

        //Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        //Screen.fullScreen = true;
        //Screen.SetResolution(3840, 2160, true);

        SoundManager.Ins.PlayBGM();
        ScreenSaverScene();
        Cursor.visible = _mouse;
    }

    private void Update() {
        if(_countdown) {
            cc += Time.deltaTime;
            if(cc >= 30) {
                StartCountdown = false;
                ScreenSaverScene();
            }
        }

        if(Input.GetMouseButtonDown(1)) {
            _mouse = !_mouse;
            Cursor.visible = _mouse;
        }
    }

    public void ScreenSaverScene() {
        StartCountdown = false;
        avatarUI.gameObject.SetActive(false);
        interestUI.gameObject.SetActive(false);
        finishUI.gameObject.SetActive(false);
        charactor.SetActive(false);
        screenSaverUI.gameObject.SetActive(true);
    }

    public void AvatarScene() {
        StartCountdown = true;
        if (avatarUI.selectedAvatar) avatarUI.selectedAvatar.ApplyProps(null, null);
        screenSaverUI.gameObject.SetActive(false);

        charactor.transform.position = p1.position;
        charactor.transform.localScale = Vector3.zero;
        charactor.SetActive(true);
        charactor.transform.DOScale(p1.localScale, .3f).SetEase(Ease.OutBack).SetDelay(.6f);
        avatarUI.StartAvatar();
    }

    public void InterestScene() {
        StartCountdown = true;
        avatarUI.gameObject.SetActive(false);
        charactor.SetActive(false);
        interestUI.StartMenu();
    }

    public void FinishScene() {
        StartCountdown = false;
        avatarUI.selectedAvatar.ApplyProps(itemData.prop_L, itemData.prop_R);
        charactor.transform.position = p2.position + ((avatarUI.selectedAvatar.age == NameList.Age.Kid) ? new Vector3(0,2.5f,0) : Vector3.zero);
        charactor.transform.localScale = p2.localScale;
        charactor.SetActive(true);
        avatarUI.selectedAvatar.StartEvent(itemData.animation);
        interestUI.gameObject.SetActive(false);
        finishUI.StartUI();

        DOVirtual.DelayedCall(delayTime * .5f, () => queue.AddQueue());
        DOVirtual.DelayedCall(delayTime, ScreenSaverScene);
    }
}
