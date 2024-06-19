using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAvaterScript : MonoBehaviour
{
    public AvatarCreator kid, adult, elder;

    private void Start() {
        kid.age = NameList.Age.Kid;
        adult.age = NameList.Age.Adult;
        elder.age = NameList.Age.Elder;

        RandomKid();
        RandomAdult();
        RandomElder();

        kid.StartIdle();
        adult.StartIdle();
        elder.StartIdle();
    }

    private void Update() {

        if(Input.GetKeyDown(KeyCode.Space)) {
            RandomKid();
            RandomAdult();
            RandomElder();
        }

    }

    void RandomKid() {
        int rnd = UnityEngine.Random.Range(0, 1);

        PartData parts = null;
        if (rnd == 0) {
            parts = DataManager.Ins.boy_part;
            kid.gender = NameList.Gender.Boy;
        } else {
            parts = DataManager.Ins.girl_part;
            kid.gender = NameList.Gender.Girl;
        }

        string head = parts.head[UnityEngine.Random.Range(0, parts.head.Count)];
        string body = parts.body[UnityEngine.Random.Range(0, parts.body.Count)];
        string leg = parts.leg[UnityEngine.Random.Range(0, parts.leg.Count)];
        string shoe = parts.shoe[UnityEngine.Random.Range(0, parts.shoe.Count)];
        kid.ApplyAvatar(head, body, leg, shoe);
    }

    void RandomAdult() {
        int rnd = UnityEngine.Random.Range(0, 2);

        PartData parts = null;
        if (rnd == 0) {
            parts = DataManager.Ins.man_part;
            adult.gender = NameList.Gender.Man;
        } else {
            parts = DataManager.Ins.woman_part;
            adult.gender = NameList.Gender.Woman;
        }

        string head = parts.head[UnityEngine.Random.Range(0, parts.head.Count)];
        string body = parts.body[UnityEngine.Random.Range(0, parts.body.Count)];
        string leg = parts.leg[UnityEngine.Random.Range(0, parts.leg.Count)];
        string shoe = parts.shoe[UnityEngine.Random.Range(0, parts.shoe.Count)];
        adult.ApplyAvatar(head, body, leg, shoe);
    }

    void RandomElder() {
        int rnd = UnityEngine.Random.Range(0, 2);

        PartData parts = null;
        if (rnd == 0) {
            parts = DataManager.Ins.grandpa_part;
            elder.gender = NameList.Gender.Grandpa;
        } else {
            parts = DataManager.Ins.grandma_part;
            elder.gender = NameList.Gender.Grandma;
        }

        string head = parts.head[UnityEngine.Random.Range(0, parts.head.Count)];
        string body = parts.body[UnityEngine.Random.Range(0, parts.body.Count)];
        string leg = parts.leg[UnityEngine.Random.Range(0, parts.leg.Count)];
        string shoe = parts.shoe[UnityEngine.Random.Range(0, parts.shoe.Count)];
        elder.ApplyAvatar(head, body, leg, shoe);
    }
}
