using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InterestUI : MonoBehaviour
{
    public GameObject menu, fg;
    public Transform headingT, foodT, lifestyleT;
    public FoodUI food;
    public LifestyleUI lifestyle;

    private void Awake() {
        fg.SetActive(false);
        food.gameObject.SetActive(false);
        lifestyle.gameObject.SetActive(false);
    }

    public void StartMenu() {
        GameManager.Ins.StartCountdown = true;
        fg.SetActive(true);

        headingT.localScale = foodT.localScale = lifestyleT.localScale = Vector3.zero;

        menu.SetActive(true);

        headingT.DOScale(1, .3f).SetEase(Ease.OutBack).SetDelay(.1f);
        foodT.DOScale(1, .3f).SetEase(Ease.OutBack).SetDelay(.2f);
        lifestyleT.DOScale(1, .3f).SetEase(Ease.OutBack).SetDelay(.3f);

        food.gameObject.SetActive(false);
        lifestyle.gameObject.SetActive(false);

        gameObject.SetActive(true);
        SoundManager.Ins.PlayClick();
    }

    public void ChooseFood() {
        GameManager.Ins.StartCountdown = true;
        SoundManager.Ins.PlayClick();
        menu.SetActive(false);
        food.StartMenu();
    }

    public void ChooseLifestyle() {
        GameManager.Ins.StartCountdown = true;
        SoundManager.Ins.PlayClick();
        menu.SetActive(false);
        lifestyle.StartMenu();
    }
}
