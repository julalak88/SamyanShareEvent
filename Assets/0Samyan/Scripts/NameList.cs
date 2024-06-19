using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameList : MonoBehaviour
{
    public enum Activity {
        Food,
        Movie,
        Study,
        Friend,
        Ball,
        Pray
    }

    public enum AnimationSet {
        Food,  // 0
        Movie, // 1
        Study, // 2
        Friend, // 3
        Ball, // 4
        Pray // 5
    }

    public enum Age {
        Kid,
        Adult,
        Elder
    }

    public enum Gender {
        Boy,
        Girl,
        Man,
        Woman,
        Grandpa,
        Grandma
    }

    public enum Emo {
        Normal,
        Smell,
        Wow
    }
}
