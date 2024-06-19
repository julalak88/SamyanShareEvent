using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Ins = null;

    [FoldoutGroup("All Part List")]
    public PartData boy_part, girl_part, man_part, woman_part, grandpa_part, grandma_part;

    [FoldoutGroup("Food Database")]
    public List<ItemData> food;
    [FoldoutGroup("Lifestyle Database")]
    public List<ItemData> lifestyle;

    private void Awake() {
        Ins = this;
    }

}

[System.Serializable]
public class PartData {
    public List<string> head, body, leg, shoe;
}

[System.Serializable]
public class ItemData {
    public Sprite textSprite_th, textSprite_en;
    public NameList.AnimationSet animation;
    public NameList.Activity activity;
    public Texture2D prop_L, prop_R;
    public int numLoopEvent = 5;
}