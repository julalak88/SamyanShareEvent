using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class AvatarUIManager : SerializedMonoBehaviour
{
    public AvatarCreator[] avatars;
    public Dictionary<NameList.Gender, GameObject> scrollList;
    public GameObject confirmPopup, fg;
    public RectTransform headingT, chooseT, scrollT, confirmT;

    [HideInInspector]
    public AvatarCreator selectedAvatar;

    bool isMale = true;
    GameObject curScrollList;

    private void Awake() {
        confirmPopup.SetActive(false);
    }

    private void OnDisable() {
        confirmPopup.SetActive(false);
    }

    private void Start() {
        for (int i = 0; i < avatars.Length; i++) {
            avatars[i].ChangeSex(true, true);
            avatars[i].gameObject.SetActive(false);
        }
        selectedAvatar = avatars[1];
        selectedAvatar.gameObject.SetActive(true);
        curScrollList = scrollList[NameList.Gender.Man];
        curScrollList.SetActive(true);
    }

    public void StartAvatar() {
        confirmPopup.SetActive(false);
        fg.SetActive(false);

        Vector2 pos = chooseT.anchoredPosition;
        pos.x = -600;
        chooseT.anchoredPosition = pos;
        pos = scrollT.anchoredPosition;
        pos.x = 2800;
        scrollT.anchoredPosition = pos;

        headingT.localScale = confirmT.localScale = Vector3.zero;

        gameObject.SetActive(true);

        headingT.DOScale(1, .3f).SetEase(Ease.OutBack).SetDelay(.1f);
        chooseT.DOAnchorPosX(706, .3f).SetEase(Ease.OutBack).SetDelay(.2f);
        scrollT.DOAnchorPosX(1015, .3f).SetEase(Ease.OutBack).SetDelay(.3f);
        confirmT.DOScale(1, .3f).SetEase(Ease.OutBack).SetDelay(.4f);
    }

    #region choose gender, age
    public void choose_Gender_Male(bool value) {
        //print("male : " + value);
        if (!value) return;
        isMale = true;
        selectedAvatar.ChangeSex(true);
        curScrollList.SetActive(false);
        curScrollList = scrollList[selectedAvatar.gender];
        curScrollList.SetActive(true);
        SoundManager.Ins.PlaySelect2();
    }
    public void choose_Gender_Female(bool value) {
        //print("female : " + value);
        if (!value) return;
        isMale = false;
        selectedAvatar.ChangeSex(false);
        curScrollList.SetActive(false);
        curScrollList = scrollList[selectedAvatar.gender];
        curScrollList.SetActive(true);
        SoundManager.Ins.PlaySelect2();
    }

    public void choose_Age_Kid(bool value) {
        if (!value || selectedAvatar == avatars[0]) return;
        selectedAvatar.gameObject.SetActive(false);
        selectedAvatar = avatars[0];
        if (selectedAvatar.isMale != isMale) selectedAvatar.ChangeSex(isMale);
        selectedAvatar.gameObject.SetActive(true);
        curScrollList.SetActive(false);
        curScrollList = scrollList[selectedAvatar.gender];
        curScrollList.SetActive(true);
        SoundManager.Ins.PlaySelect2();
    }
    public void choose_Age_Adult(bool value) {
        if (!value || selectedAvatar == avatars[1]) return;
        selectedAvatar.gameObject.SetActive(false);
        selectedAvatar = avatars[1];
        if (selectedAvatar.isMale != isMale) selectedAvatar.ChangeSex(isMale);
        selectedAvatar.gameObject.SetActive(true);
        curScrollList.SetActive(false);
        curScrollList = scrollList[selectedAvatar.gender];
        curScrollList.SetActive(true);
        SoundManager.Ins.PlaySelect2();
    }
    public void choose_Age_Elder(bool value) {
        if (!value || selectedAvatar == avatars[2]) return;
        selectedAvatar.gameObject.SetActive(false);
        selectedAvatar = avatars[2];
        if (selectedAvatar.isMale != isMale) selectedAvatar.ChangeSex(isMale);
        selectedAvatar.gameObject.SetActive(true);
        curScrollList.SetActive(false);
        curScrollList = scrollList[selectedAvatar.gender];
        curScrollList.SetActive(true);
        SoundManager.Ins.PlaySelect2();
    }

    #endregion

    public void Done() {
        GameManager.Ins.StartCountdown = true;
        Transform popup = confirmPopup.transform.Find("Window");
        popup.localScale = Vector3.zero;
        confirmPopup.SetActive(true);

        popup.DOScale(1, .3f).SetEase(Ease.OutBack);
        SoundManager.Ins.PlayPopup();
    }

    public void Confirm() {
        SoundManager.Ins.PlaySelectAvatar();
        GameManager.Ins.InterestScene();
    }

    public void Cancel() {
        GameManager.Ins.StartCountdown = true;
        confirmPopup.SetActive(false);
    }
}
