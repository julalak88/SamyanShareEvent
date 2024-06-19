using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class AvatarCreator : MonoBehaviour
{
    public NameList.Age age = NameList.Age.Adult;
    public NameList.Gender gender = NameList.Gender.Boy;
    public NameList.AnimationSet animationSet = NameList.AnimationSet.Food;
    public SkinnedMeshRenderer[] body, head, leg, shoe, face;
    public SkinnedMeshRenderer propL, propR;
    public int numLoopEvent = 5;
    public AvatarCreator friend;

    [HideInInspector]
    public QueuePoint selectedPoint;

    [HideInInspector]
    public string male_head = "A", male_body = "A", male_leg = "A", male_shoe = "A";
    [HideInInspector]
    public string female_head = "A", female_body = "A", female_leg = "A", female_shoe = "A";

    Animator animator;
    float maxDist = 5f, maxSpeed = 6.5f;
    int loopEvent = 0, indPath = 0;

    bool _isMale = true;
    public bool isMale {
        get { return _isMale; }
    }
    
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void ApplyAvatar(string _head, string _body, string _leg, string _shoe) {

        ChangeHead(_head);
        ChangeBody(_body);
        ChangeLeg(_leg);
        ChangeShoe(_shoe);
        ChangeFace();

    }

    public void ApplyProps(Texture2D prop_L, Texture2D prop_R) {
        if (prop_L == null) propL.gameObject.SetActive(false);
        else {
            propL.material.mainTexture = null;
            propL.material.mainTexture = prop_L;
            propL.gameObject.SetActive(true);
        }
        if (prop_R == null) propR.gameObject.SetActive(false);
        else {
            propR.material.mainTexture = null;
            propR.material.mainTexture = prop_R;
            propR.gameObject.SetActive(true);
        }
    }

    public void ChangeHead(string _head) {
        if (gender == NameList.Gender.Boy || gender == NameList.Gender.Man || gender == NameList.Gender.Grandpa)
            male_head = _head;
        else
            female_head = _head;
        Texture2D tex = Resources.Load<Texture2D>(age + "/Head/Head_" + gender + _head);
        head[0].material.mainTexture = null;
        head[0].material.mainTexture = tex;
    }

    public void ChangeBody(string _body) {
        if (gender == NameList.Gender.Boy || gender == NameList.Gender.Man || gender == NameList.Gender.Grandpa)
            male_body = _body;
        else
            female_body = _body;
        Texture2D tex = Resources.Load<Texture2D>(age + "/Body/Body_" + gender + _body);
        for (int i = 0; i < body.Length; i++) {
            body[i].material.mainTexture = null;
            body[i].material.mainTexture = tex;
        }
    }

    public void ChangeLeg(string _leg) {
        if (gender == NameList.Gender.Boy || gender == NameList.Gender.Man || gender == NameList.Gender.Grandpa)
            male_leg = _leg;
        else
            female_leg = _leg;
        Texture2D tex = Resources.Load<Texture2D>(age + "/Leg/Leg_" + gender + _leg);
        for (int i = 0; i < leg.Length; i++) {
            leg[i].material.mainTexture = null;
            leg[i].material.mainTexture = tex;
        }
    }

    public void ChangeShoe(string _shoe) {
        if (gender == NameList.Gender.Boy || gender == NameList.Gender.Man || gender == NameList.Gender.Grandpa)
            male_shoe = _shoe;
        else
            female_shoe = _shoe;
        Texture2D tex = Resources.Load<Texture2D>(age + "/Shoe/Shoe_" + gender + _shoe);
        for (int i = 0; i < shoe.Length; i++) {
            shoe[i].material.mainTexture = null;
            shoe[i].material.mainTexture = tex;
        }
    }

    public void ChangeFace(NameList.Emo emo = NameList.Emo.Normal) {
        Texture2D tex = Resources.Load<Texture2D>(age + "/Face/Face_" + gender +"_"+emo);
        face[0].material.mainTexture = null;
        face[0].material.mainTexture = tex;
    }

    public void ChangeSex(bool male, bool forceChange=false) {
        if (isMale == male && !forceChange) return;

        _isMale = male;

        if(age == NameList.Age.Kid) {
            if (isMale) gender = NameList.Gender.Boy;
            else gender = NameList.Gender.Girl;
        }else if (age == NameList.Age.Adult) {
            if (isMale) gender = NameList.Gender.Man;
            else gender = NameList.Gender.Woman;
        } else if (age == NameList.Age.Elder) {
            if (isMale) gender = NameList.Gender.Grandpa;
            else gender = NameList.Gender.Grandma;
        }

        if (isMale) ApplyAvatar(male_head, male_body, male_leg, male_shoe);
        else ApplyAvatar(female_head, female_body, female_leg, female_shoe);
    }

    public void StartIdle() {
        animator.Play(age + "_Idle", 0, 0);
    }

    public void StartEvent(NameList.AnimationSet animation) {
        print(age + "_Event_" + animation);
        if (animation == NameList.AnimationSet.Study) {
            propL.material.mainTexture = null;
            propL.material.mainTexture = Resources.Load<Texture2D>("Props/Book_Open");
        }
        animator.Play(age + "_EventShow_" + animation, 0, 0);
    }

    public void StartMove() {
        loopEvent = 0;
        indPath = 0;
        transform.position = selectedPoint.InOut[indPath].position;
        if (animationSet == NameList.AnimationSet.Food)
            animator.SetInteger("Random", UnityEngine.Random.Range(0, 2));
        indPath++;
        startMove();
        animator.SetInteger("AnimationSet", (int)animationSet);
    }

    void startMove() {
        Vector3 pos = selectedPoint.InOut[indPath].position;
        float scale = Mathf.Abs(transform.localScale.x);
        if (transform.position.x > pos.x) transform.localScale = new Vector3(scale * -1, scale);
        else transform.localScale = new Vector3(scale, scale);

        float speed = (Vector3.Magnitude(pos - transform.position) / maxDist) * maxSpeed;
        transform.DOMove(pos, speed).OnComplete(onReachTargetPoint).SetEase(Ease.Linear);
        //print("startMove "+pos + " " + speed);
    }

    private void onReachTargetPoint() {
        if(selectedPoint.InOut[indPath].name == "Queue" || selectedPoint.InOut[indPath].name == "Friend") {
            selectedPoint.onProgress = false;
            float scale = Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(scale * selectedPoint.direction, scale);
            animator.SetBool("isTargetPoint", true);
            if (animationSet == NameList.AnimationSet.Study) {
                propL.material.mainTexture = Resources.Load<Texture2D>("Props/Book_Open");
            }
        }else {
            if(indPath >= selectedPoint.InOut.Length-1) {
                selectedPoint = null;
                Destroy(gameObject);
            } else {
                indPath++;
                startMove();
            }
        }
    }

    public void StartOut() {
        ++loopEvent;
        //print("loop " + loopEvent + " " + numLoopEvent);
        if (loopEvent > numLoopEvent) {
            indPath++;
            if (animationSet == NameList.AnimationSet.Study)
                propL.material.mainTexture = Resources.Load<Texture2D>("Props/Book_Close");
            startMove();
        } else if (loopEvent == numLoopEvent) {
            selectedPoint.qCount = -1;
            selectedPoint.onProgress = false;
            selectedPoint.avatar = null;
            loopEvent = numLoopEvent;
            animator.SetBool("isTargetPoint", false);
            animator.SetBool("isOut", true);
            if (friend != null)
                friend.StartOutForce();
        } 
        
    }

    public void StartOutForce() {
        selectedPoint.qCount = -1;
        selectedPoint.onProgress = false;
        selectedPoint.avatar = null;
        loopEvent = numLoopEvent;
        animator.SetBool("isTargetPoint", false);
        animator.SetBool("isOut", true);
        //print(age + "_EventStop_" + animationSet);
        animator.Play(age + "_EventStop_" + animationSet, 0, 0);
        if (friend != null)
            friend.StartOutForce();
    }
}
