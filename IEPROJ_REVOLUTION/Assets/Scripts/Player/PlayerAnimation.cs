using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        GameManager.GameStart += StartAnimation;
    }

    void StartAnimation()
    {
        animator.SetBool("Walking", true);
    }

    public void ToggleRoll()
    {
        animator.SetBool("Rolling", !animator.GetBool("Rolling"));
    }

}
