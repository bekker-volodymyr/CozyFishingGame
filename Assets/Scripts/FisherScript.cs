using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisherScript : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void ResetAnimatorState()
    {
        animator.SetInteger("State", 0);
    }
}
