using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThumbnailSelector : MonoBehaviour
{
    public Toggle toggle;
    public Image image;
    public int type = 0;

    string[] alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S" };

    private void Awake() {
        toggle.onValueChanged.AddListener(onToggle);
    }

    private void onToggle(bool value) {
        if (!value || GameManager.Ins == null) return;
        if (GameManager.Ins.avatarUI == null) return;
        if (GameManager.Ins.avatarUI.selectedAvatar == null) return;

        string index = alphabet[transform.GetSiblingIndex()];

        if (type == 0) {
            GameManager.Ins.avatarUI.selectedAvatar.ChangeHead(index);
        } else if (type == 1) {
            GameManager.Ins.avatarUI.selectedAvatar.ChangeBody(index);
        } else if (type == 2) {
            GameManager.Ins.avatarUI.selectedAvatar.ChangeLeg(index);
        } else {
            GameManager.Ins.avatarUI.selectedAvatar.ChangeShoe(index);
        }
        GameManager.Ins.StartCountdown = true;
        SoundManager.Ins.PlaySelect();
    }

    public void SetThumbnail(Sprite _image, ToggleGroup group) {
        image.sprite = _image;
        toggle.group = group;
    }
}
