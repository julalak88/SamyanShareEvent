using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    public Animator animator;

    public void PlayAnimation(string animName) {
        animator.Play(animName, 0, 0);
    }
}
