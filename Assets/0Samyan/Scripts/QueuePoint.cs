using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueuePoint : MonoBehaviour
{
    [HideInInspector]
    public AvatarCreator avatar;

    public int direction = 1;
    public List<NameList.Activity> activities;
    public QueuePoint friendPoint;
    [HideInInspector]
    public int qCount = -1;
    [HideInInspector]
    public bool onProgress = false;
    public Transform[] InOut;
}
