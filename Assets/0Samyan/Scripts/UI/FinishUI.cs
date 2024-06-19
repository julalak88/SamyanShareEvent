using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FinishUI : MonoBehaviour
{

    public Image image_th, image_en;
    public GameObject heading,firework;

    private void Awake() {
        firework.SetActive(false);
        gameObject.SetActive(false);    
    }

    public void StartUI() {
        image_th.transform.parent.localScale = Vector3.zero;
        image_th.sprite = GameManager.Ins.itemData.textSprite_th;
        image_en.transform.parent.localScale = Vector3.zero;
        image_en.sprite = GameManager.Ins.itemData.textSprite_en;
        heading.transform.localScale = Vector3.zero;

        gameObject.SetActive(true);

        firework.SetActive(true);

        image_en.transform.parent.DOScale(1, .4f).SetEase(Ease.OutBack).SetDelay(1);
        image_th.transform.parent.DOScale(1, .4f).SetEase(Ease.OutBack).SetDelay(1.15f);
        heading.transform.DOScale(1, .4f).SetEase(Ease.OutBack).SetDelay(2);
    }

    private void OnDisable() {
        firework.SetActive(false);
    }
}
