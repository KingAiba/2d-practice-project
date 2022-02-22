using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 20f;
    public float doubleJumpForce = 5f;

    public bool isGrounded = true;
    public bool canDoubleJump = true;
    public bool PressedJump = false;

    public LayerMask groundLayer;

    private Rigidbody2D characterRB;
    private Collider2D characterCollider;

    public float climbSpeed = 5f;
    public bool isLadder { get; private set; }
    public bool isClimbing { get; private set; }

    public float verticalInput;

    private PlayerManager playerManager;
    

    void Start()
    {
        characterRB = GetComponent<Rigidbody2D>();
        characterCollider = GetComponent<BoxCollider2D>();
        playerManager = GetComponent<PlayerManager>();
        
    }

   
    void Update()
    {
        UpdateJumpStatus();
        if(!playerManager.isDead)
        {
            ClimbLadder();
        }      
    }

    private void FixedUpdate()
    {
        if(!playerManager.isDead)
        {
            movePlayer();
            ClimbLadderPhysics();
        }
    }

    private void ClimbLadder()
    {
        verticalInput = Input.GetAxis("Vertical");
        if(isLadder && Mathf.Abs(verticalInput) > 0f)
        {
            isClimbing = true;
        }
    }

    private void ClimbLadderPhysics()
    {
        if (isClimbing)
        {
            characterRB.gravityScale = 0f;
            characterRB.velocity = new Vector2(characterRB.velocity.x, verticalInput*5f);
            characterCollider.isTrigger = true;
        }
        else
        {
            characterRB.gravityScale = 1f;
            characterCollider.isTrigger = false;
        }

    }

    private void UpdateJumpStatus()
    {
        isGrounded = GroundCheck();

        if (!canDoubleJump)
        {
            canDoubleJump = GroundCheck();
        }

        if (!PressedJump)
        {
            PressedJump = Input.GetKeyDown(KeyCode.Space);
        }
    }

    private void movePlayer()
    {
        TryJump();

        float horizontalInput = Input.GetAxis("Horizontal");
        characterRB.velocity = new Vector2(horizontalInput*moveSpeed, characterRB.velocity.y);       
    }

    private void TryJump()
    {
        if(PressedJump && isGrounded)
        {
            characterRB.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            PressedJump = false;
        }
        else if(PressedJump && !isGrounded && canDoubleJump)
        {
            characterRB.AddForce(Vector3.up * doubleJumpForce, ForceMode2D.Impulse);
            canDoubleJump = false;
            PressedJump = false;
        }
    }

    private bool GroundCheck()
    {
        float castDistance=0.2f;
        RaycastHit2D hit;
        hit = Physics2D.BoxCast(characterCollider.bounds.center, characterCollider.bounds.size, 0f, Vector2.down, castDistance, groundLayer);

        return hit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ladder"))
        {
            isLadder = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }

}
