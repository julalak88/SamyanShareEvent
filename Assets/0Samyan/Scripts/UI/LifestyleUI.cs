using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

public class LifestyleUI : MonoBehaviour
{

    public Transform headingT, backButton;

    Button[] list;
    float speed = .5f;

    private void Awake() {
        list = transform.Find("Menu").GetComponentsInChildren<Button>(true);
        for (int i = 0; i < list.Length; i++) {
            list[i].onClick.AddListener(onChoose);
        }
    }
    
    public void StartMenu() {
        headingT.localScale = backButton.localScale = Vector3.zero;
        float time;
        for (int i = 0; i < list.Length; i++) {
            list[i].transform.localScale = Vector3.zero;
            time = (i * 0.05f) + .2f;
            if (i == list.Length - 1) list[i].transform.DOScale(1, .2f).SetDelay(time).SetEase(Ease.OutBack).OnComplete(() => backButton.DOScale(1, .3f).SetEase(Ease.OutBack));
            else list[i].transform.DOScale(1, .2f).SetDelay(time).SetEase(Ease.OutBack);
        }

        gameObject.SetActive(true);

        headingT.DOScale(1, .3f).SetEase(Ease.OutBack).SetDelay(.1f);
    }

    private void onChoose() {
        SoundManager.Ins.PlayChoosed();
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        int index = obj.transform.GetSiblingIndex();
        GameManager.Ins.itemData = DataManager.Ins.lifestyle[index];
        GameManager.Ins.FinishScene();
    }
}
