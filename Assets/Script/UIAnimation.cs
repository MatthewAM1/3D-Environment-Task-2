using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    private Animator animator;
    public string triggerReset;
    public string triggerContinue;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimationReset()
    {
        if (animator != null && !string.IsNullOrEmpty(triggerReset))
        {
            animator.SetTrigger(triggerReset);
        }
    }

    public void PlayAnimationContinue()
    {
        if (animator != null && !string.IsNullOrEmpty(triggerContinue))
        {
            animator.SetTrigger(triggerContinue);
        }
    }
}
