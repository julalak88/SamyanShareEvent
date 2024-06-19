using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public List<QueueData> queue;
    public GameObject adult, kid, elder;

    QueuePoint[] queuePoints;
    int qCount = 0;

    private void Awake() {
        queuePoints = GameObject.Find("TV/QueueAll").GetComponentsInChildren<QueuePoint>();
    }

    void CheckQueue() {
        if(queue.Count > 0) {
            QueueData data = queue[0];
            List<QueuePoint> freeList = new List<QueuePoint>();
            int length = queuePoints.Length;
            int tmpQCount = 999999;
            int iBusy=-1;
            for (int i = 0; i < length; i++) {
                if (!queuePoints[i].onProgress && queuePoints[i].activities.Contains(data.itemData.activity)) {
                    if (queuePoints[i].qCount == -1)
                        freeList.Add(queuePoints[i]);
                    else {
                        if (queuePoints[i].qCount < tmpQCount) {
                            tmpQCount = queuePoints[i].qCount;
                            iBusy = i;
                        }
                    }
                }
            }

            if (freeList.Count == 0 && iBusy == -1) return;

            GameObject obj;
            if (data.age == NameList.Age.Adult)
                obj = Instantiate(adult, new Vector3(0, 100, 0), Quaternion.Euler(0, 180, 0), GameObject.Find("TV").transform);
            else if(data.age == NameList.Age.Elder)
                obj = Instantiate(elder, new Vector3(0, 100, 0), Quaternion.Euler(0, 180, 0), GameObject.Find("TV").transform);
            else
                obj = Instantiate(kid, new Vector3(0, 100, 0), Quaternion.Euler(0, 180, 0), GameObject.Find("TV").transform);

            AvatarCreator avatar = obj.GetComponent<AvatarCreator>();
            avatar.age = data.age;
            avatar.gender = data.gender;
            avatar.animationSet = data.itemData.animation;
            avatar.numLoopEvent = Mathf.FloorToInt(data.itemData.numLoopEvent * data.mutiplyLoop);
            avatar.ApplyProps(data.itemData.prop_L, data.itemData.prop_R);
            avatar.ApplyAvatar(data.head, data.body, data.leg, data.shoe);
            avatar.StartIdle();
            obj.transform.localScale = Vector3.one * .3f;

            QueuePoint selectedPoint = null;
            if (freeList.Count > 0) {
                int ind = UnityEngine.Random.Range(0, freeList.Count);
                selectedPoint = freeList[ind];
            } else if(iBusy != -1){
                //print("busy " + iBusy);
                selectedPoint = queuePoints[iBusy];
                selectedPoint.avatar.StartOutForce();
            }

            if (selectedPoint != null) {
                selectedPoint.qCount = qCount++;
                selectedPoint.onProgress = true;

                selectedPoint.avatar = avatar;
                avatar.selectedPoint = selectedPoint;

                #region create friend
                if (avatar.animationSet == NameList.AnimationSet.Friend && selectedPoint.friendPoint != null) { // create friend

                    if (data.age == NameList.Age.Adult)
                        obj = Instantiate(adult, new Vector3(0, 100, 0), Quaternion.Euler(0, 180, 0), GameObject.Find("TV").transform);
                    else if (data.age == NameList.Age.Elder)
                        obj = Instantiate(elder, new Vector3(0, 100, 0), Quaternion.Euler(0, 180, 0), GameObject.Find("TV").transform);
                    else
                        obj = Instantiate(kid, new Vector3(0, 100, 0), Quaternion.Euler(0, 180, 0), GameObject.Find("TV").transform);

                    AvatarCreator avatarF = obj.GetComponent<AvatarCreator>();
                    avatarF.age = data.age;

                    PartData parts = null;
                    int rnd = UnityEngine.Random.Range(0, 1);
                    if (rnd == 0) {
                        if (data.age == NameList.Age.Kid) {
                            parts = DataManager.Ins.boy_part;
                            avatarF.gender = NameList.Gender.Boy;
                        } else if (data.age == NameList.Age.Adult) {
                            parts = DataManager.Ins.man_part;
                            avatarF.gender = NameList.Gender.Man;
                        } else {
                            parts = DataManager.Ins.grandpa_part;
                            avatarF.gender = NameList.Gender.Grandpa;
                        }
                    } else {
                        if (data.age == NameList.Age.Kid) {
                            parts = DataManager.Ins.girl_part;
                            avatarF.gender = NameList.Gender.Girl;
                        } else if (data.age == NameList.Age.Adult) {
                            parts = DataManager.Ins.woman_part;
                            avatarF.gender = NameList.Gender.Woman;
                        } else {
                            parts = DataManager.Ins.grandma_part;
                            avatarF.gender = NameList.Gender.Grandma;
                        }
                    }
                    string head = parts.head[UnityEngine.Random.Range(0, parts.head.Count)];
                    string body = parts.body[UnityEngine.Random.Range(0, parts.body.Count)];
                    string leg = parts.leg[UnityEngine.Random.Range(0, parts.leg.Count)];
                    string shoe = parts.shoe[UnityEngine.Random.Range(0, parts.shoe.Count)];

                    avatarF.animationSet = data.itemData.animation;
                    avatarF.ApplyAvatar(head, body, leg, shoe);
                    avatarF.StartIdle();
                    obj.transform.localScale = Vector3.one * .3f;

                    avatarF.numLoopEvent = 2000;
                    avatarF.selectedPoint = selectedPoint.friendPoint;
                    avatar.friend = avatarF;

                    avatarF.StartMove();
                }
                #endregion

                queue.RemoveAt(0);

                avatar.StartMove();
            }
        }
    }

    public void AddQueue() {
        print("add queue");
        QueueData qData = new QueueData();
        qData.age = GameManager.Ins.avatarUI.selectedAvatar.age;
        qData.gender = GameManager.Ins.avatarUI.selectedAvatar.gender;
        if (GameManager.Ins.avatarUI.selectedAvatar.isMale) {
            qData.head = GameManager.Ins.avatarUI.selectedAvatar.male_head;
            qData.body = GameManager.Ins.avatarUI.selectedAvatar.male_body;
            qData.leg = GameManager.Ins.avatarUI.selectedAvatar.male_leg;
            qData.shoe = GameManager.Ins.avatarUI.selectedAvatar.male_shoe;
        } else {
            qData.head = GameManager.Ins.avatarUI.selectedAvatar.female_head;
            qData.body = GameManager.Ins.avatarUI.selectedAvatar.female_body;
            qData.leg = GameManager.Ins.avatarUI.selectedAvatar.female_leg;
            qData.shoe = GameManager.Ins.avatarUI.selectedAvatar.female_shoe;
        }
        qData.mutiplyLoop = 1.7f;
        qData.itemData = GameManager.Ins.itemData;
        queue.Add(qData);
    }
    
    float cc = 0f, ccRnd = 14f;
    private void Update() {

        cc += Time.deltaTime;
        if(cc > 3) {
            cc = 0;
            CheckQueue();
        }

        ccRnd += Time.deltaTime;
        if (ccRnd > 15) {
            ccRnd = 0;

            QueueData qData = null;

            int rnd = UnityEngine.Random.Range(0, 3);
            if (rnd == 0) qData = RandomKid();
            else if (rnd == 1) qData = RandomAdult();
            else qData = RandomElder();

            rnd = UnityEngine.Random.Range(0, 2);
            if (rnd == 0)
                qData.itemData = DataManager.Ins.food[UnityEngine.Random.Range(0, DataManager.Ins.food.Count)];
            else
                qData.itemData = DataManager.Ins.lifestyle[UnityEngine.Random.Range(0, DataManager.Ins.lifestyle.Count)];

            //qData.itemData = DataManager.Ins.lifestyle[2];

            queue.Add(qData);
        }
    }

    #region random queue type
    QueueData RandomKid() {

        QueueData qData = new QueueData();
        qData.age = NameList.Age.Kid;

        int rnd = UnityEngine.Random.Range(0, 1);

        PartData parts = null;
        if (rnd == 0) {
            parts = DataManager.Ins.boy_part;
            qData.gender = NameList.Gender.Boy;
        } else {
            parts = DataManager.Ins.girl_part;
            qData.gender = NameList.Gender.Girl;
        }

        qData.head = parts.head[UnityEngine.Random.Range(0, parts.head.Count)];
        qData.body = parts.body[UnityEngine.Random.Range(0, parts.body.Count)];
        qData.leg = parts.leg[UnityEngine.Random.Range(0, parts.leg.Count)];
        qData.shoe = parts.shoe[UnityEngine.Random.Range(0, parts.shoe.Count)];

        return qData;
    }

    QueueData RandomAdult() {

        QueueData qData = new QueueData();
        qData.age = NameList.Age.Adult;

        int rnd = UnityEngine.Random.Range(0, 2);

        PartData parts = null;
        if (rnd == 0) {
            parts = DataManager.Ins.man_part;
            qData.gender = NameList.Gender.Man;
        } else {
            parts = DataManager.Ins.woman_part;
            qData.gender = NameList.Gender.Woman;
        }

        qData.head = parts.head[UnityEngine.Random.Range(0, parts.head.Count)];
        qData.body = parts.body[UnityEngine.Random.Range(0, parts.body.Count)];
        qData.leg = parts.leg[UnityEngine.Random.Range(0, parts.leg.Count)];
        qData.shoe = parts.shoe[UnityEngine.Random.Range(0, parts.shoe.Count)];

        return qData;
    }

    QueueData RandomElder() {

        QueueData qData = new QueueData();
        qData.age = NameList.Age.Elder;

        int rnd = UnityEngine.Random.Range(0, 2);

        PartData parts = null;
        if (rnd == 0) {
            parts = DataManager.Ins.grandpa_part;
            qData.gender = NameList.Gender.Grandpa;
        } else {
            parts = DataManager.Ins.grandma_part;
            qData.gender = NameList.Gender.Grandma;
        }

        qData.head = parts.head[UnityEngine.Random.Range(0, parts.head.Count)];
        qData.body = parts.body[UnityEngine.Random.Range(0, parts.body.Count)];
        qData.leg = parts.leg[UnityEngine.Random.Range(0, parts.leg.Count)];
        qData.shoe = parts.shoe[UnityEngine.Random.Range(0, parts.shoe.Count)];

        return qData;
    }
    #endregion

}

[System.Serializable]
public class QueueData {
    public NameList.Age age = NameList.Age.Adult;
    public NameList.Gender gender = NameList.Gender.Boy;
    public string head, body, leg, shoe;
    public ItemData itemData;
    public float mutiplyLoop = 1;
}
