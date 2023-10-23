using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]private Animator animator;

    private void Start()
    {
        GameManager.GameStart += StartAnimation;
        animator = GetComponentInChildren<Animator>();
    }

    void StartAnimation()
    {
        animator.SetBool("Walking", true);
    }

    public void ToggleRoll()
    {
        animator.SetBool("Rolling", !animator.GetBool("Rolling"));
    }

    private void OnDestroy()
    {
        GameManager.GameStart -= StartAnimation;
    }

}
