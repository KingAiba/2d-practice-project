using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    public Animator playerAnimator;

    private PlayerController2D playerController;
    private Rigidbody2D playerRB;
    private PlayerManager playerManager;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();

        playerManager = GetComponent<PlayerManager>();
        playerController = GetComponent<PlayerController2D>();

        playerAnimator = GetComponent<Animator>();
    }

    public void PlayRun(float val)
    {
        playerAnimator.SetFloat("speedf", val);
    }

    public void PlayJump()
    {
        playerAnimator.SetTrigger("jump");
    }

    public void AnimUpdate()
    {
        if(Mathf.Abs(playerRB.velocity.x) > 0)
        {
            PlayRun(0.6f);
        }else
        {
            PlayRun(0.0f);
        }

        if(playerController.PressedJump && !playerController.isClimbing)
        {
            PlayJump();
        }
    }

    private void LateUpdate()
    {
        AnimUpdate();
    }



}
