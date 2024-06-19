using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollListLoader : MonoBehaviour
{
    public NameList.Gender gender = NameList.Gender.Boy;
    public ToggleGroup head_group, body_group, leg_group, shoe_group;
    public GameObject head_thumb, body_thumb, leg_thumb, shoe_thumb;

    private void Start() {
        LoadThumbnails();

        gameObject.SetActive(false);
    }

    public void LoadThumbnails() {

        Texture2D tex = null;

        NameList.Age age = NameList.Age.Adult;
        List<string> head = null, body = null, leg = null, shoe = null;

        if (gender == NameList.Gender.Man) {
            age = NameList.Age.Adult;
            head = DataManager.Ins.man_part.head;
            body = DataManager.Ins.man_part.body;
            leg = DataManager.Ins.man_part.leg;
            shoe = DataManager.Ins.man_part.shoe;
        } else if(gender == NameList.Gender.Woman) {
            age = NameList.Age.Adult;
            head = DataManager.Ins.woman_part.head;
            body = DataManager.Ins.woman_part.body;
            leg = DataManager.Ins.woman_part.leg;
            shoe = DataManager.Ins.woman_part.shoe;
        } else if (gender == NameList.Gender.Boy) {
            age = NameList.Age.Kid;
            head = DataManager.Ins.boy_part.head;
            body = DataManager.Ins.boy_part.body;
            leg = DataManager.Ins.boy_part.leg;
            shoe = DataManager.Ins.boy_part.shoe;
        } else if (gender == NameList.Gender.Girl) {
            age = NameList.Age.Kid;
            head = DataManager.Ins.girl_part.head;
            body = DataManager.Ins.girl_part.body;
            leg = DataManager.Ins.girl_part.leg;
            shoe = DataManager.Ins.girl_part.shoe;
        } else if (gender == NameList.Gender.Grandpa) {
            age = NameList.Age.Elder;
            head = DataManager.Ins.grandpa_part.head;
            body = DataManager.Ins.grandpa_part.body;
            leg = DataManager.Ins.grandpa_part.leg;
            shoe = DataManager.Ins.grandpa_part.shoe;
        } else if (gender == NameList.Gender.Grandma) {
            age = NameList.Age.Elder;
            head = DataManager.Ins.grandma_part.head;
            body = DataManager.Ins.grandma_part.body;
            leg = DataManager.Ins.grandma_part.leg;
            shoe = DataManager.Ins.grandma_part.shoe;
        }

        GameObject obj;
        ThumbnailSelector firstSelector = null, selector;
        for (int i = 0; i < head.Count; i++) {
            tex = Resources.Load<Texture2D>("Thumbnail/" + age + "/Head/Head_" + gender + head[i]);
            obj = Instantiate(head_thumb, head_group.transform, false);
            selector = obj.GetComponent<ThumbnailSelector>();
            if (i == 0) firstSelector = selector;
            selector.SetThumbnail(Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f)), head_group);
        }
        firstSelector.toggle.isOn = true;
        RectTransform content = (RectTransform)head_group.transform.parent;
        Vector2 size = content.sizeDelta;
        size.x = head.Count * (256 + 20);
        content.sizeDelta = size;

        for (int i = 0; i < body.Count; i++) {
            tex = Resources.Load<Texture2D>("Thumbnail/" + age + "/Body/Body_" + gender + body[i]);
            obj = Instantiate(body_thumb, body_group.transform, false);
            selector = obj.GetComponent<ThumbnailSelector>();
            if (i == 0) firstSelector = selector;
            selector.SetThumbnail(Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f)), body_group);
        }
        firstSelector.toggle.isOn = true;
        content = (RectTransform)body_group.transform.parent;
        size = content.sizeDelta;
        size.x = body.Count * (301 + 20);
        content.sizeDelta = size;

        for (int i = 0; i < leg.Count; i++) {
            tex = Resources.Load<Texture2D>("Thumbnail/" + age + "/Leg/Leg_" + gender + leg[i]);
            obj = Instantiate(leg_thumb, leg_group.transform, false);
            selector = obj.GetComponent<ThumbnailSelector>();
            if (i == 0) firstSelector = selector;
            selector.SetThumbnail(Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f)), leg_group);
        }
        firstSelector.toggle.isOn = true;
        content = (RectTransform)leg_group.transform.parent;
        size = content.sizeDelta;
        size.x = leg.Count * (257 + 35);
        content.sizeDelta = size;

        for (int i = 0; i < shoe.Count; i++) {
            tex = Resources.Load<Texture2D>("Thumbnail/" + age + "/Shoe/Shoe_" + gender + shoe[i]);
            obj = Instantiate(shoe_thumb, shoe_group.transform, false);
            selector = obj.GetComponent<ThumbnailSelector>();
            if (i == 0) firstSelector = selector;
            selector.SetThumbnail(Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f)), shoe_group);
        }
        firstSelector.toggle.isOn = true;
        content = (RectTransform)shoe_group.transform.parent;
        size = content.sizeDelta;
        size.x = shoe.Count * (321 + 25);
        content.sizeDelta = size;

    }
}
