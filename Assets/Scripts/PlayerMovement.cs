using UnityEngine;
using UnityEngine.InputSystem;
/*TODO 
- OverlapBox
- przy malym obrocie gałki na padzie nie powinno sie ruszac
- przyspieszenie i wychamowanie

 */
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera cam;
    [SerializeField] private Player player;
    [SerializeField] private MainMenu ui;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject fakeFlashlight;
    [SerializeField] private GameObject menu;

    [SerializeField] private Flashlight flashlightScript;
    [SerializeField] private Collider2D playerUpperCollider;
    [SerializeField] private Collider2D playerBottomCollider;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private Animator anim;

    [SerializeField] private AudioSource flashlightClick;

    [SerializeField] private float angle;

    [SerializeField] private float movementSpeed;
    
    [SerializeField] private float jumpCutRatio = 0.5f;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpCooldown =1;
    [SerializeField] private float jumpCurrentCooldown;

    private bool playerJumped = false;
    private bool isGrounded;
    private bool isCrouching;
    private float playerInputXAxis;
    private float playerGroundedRemember;
    private float playerGroundedRememberTimer = 0.08f;
    private float playerJumpedRemember = 0;
    private float playerJumpedRememberTimer = 0.06f;
    private float movementSpeedRunning;
    private float movementSpeedCrouching;
    private Vector2 mousePos;
    private Vector2 givenCoord;
    //private bool goingRight;
   // private bool lookingRight;
    private bool idle;

    private void Awake()
    {
      

        movementSpeedCrouching = movementSpeed / 2;
        movementSpeedRunning = movementSpeed;
        jumpCurrentCooldown = 0;
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
        Move();
        Jump();
        Crouch();
        LookingAngle();
        CheckPlayerFlip();
        Animate();


    }
    private void Animate()
    {
        if (idle)
        {
            anim.SetBool("Run", false);
        }
        else
        {
            anim.SetBool("Run", true);
        }
        if(rb.velocity.y == 0)
        {
            anim.SetBool("Jumped", false);
        }
        else
        {
            anim.SetBool("Jumped", true);
        }
        if (isCrouching)
        {
            anim.SetBool("Crouch", true);
        }
        else
        {
            anim.SetBool("Crouch", false);
        }

    }
    private void CheckPlayerFlip()
    {
        if (angle > -90 && angle < 90)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 0);
          //  lookingRight = true;
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
          //  lookingRight = false;
        }
    }

    public void LookingAngle()
    {
        mousePos = cam.ScreenToWorldPoint(givenCoord);
        Vector2 lookDir = mousePos - (Vector2)transform.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        flashlightScript.Angle = angle;
    }
    private void Move()
    {
        rb.velocity = new Vector2(playerInputXAxis * movementSpeed, rb.velocity.y);
        if (rb.velocity.x > 0)
        {
          //  goingRight = true;
            idle = false;

        }
        else if (rb.velocity.x < 0) 
        {
          //  goingRight = false;
            idle = false;
        }
        else
        {
           // goingRight = false;
            idle = true;
        }
    }
        
    public void OnMove(InputAction.CallbackContext context)
    {
        playerInputXAxis = context.ReadValue<float>();
    }
    private void Jump()
    {
        playerJumpedRemember -= Time.deltaTime;
        jumpCurrentCooldown -= Time.deltaTime;
        if (playerJumped && !isCrouching && jumpCurrentCooldown<0)
        {
            playerJumpedRemember = playerJumpedRememberTimer;
            jumpCurrentCooldown = jumpCooldown;
        }
        if(isGrounded && (playerJumpedRemember > 0) && rb.velocity.y <= 0)
        {
            playerJumpedRemember = 0;
            playerGroundedRemember = 0;
            rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * jumpHeight);
        }
    }
    private void CutJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y / jumpCutRatio);
    }
    private bool IsGrounded()
    {
        float extraCastDistance = .05f;

        playerGroundedRemember -= Time.deltaTime;

        // TODO //lepszy tutaj bedzie OverlapBox
        RaycastHit2D raycastHit = Physics2D.BoxCast(playerBottomCollider.bounds.center, playerBottomCollider.bounds.size, 0f, Vector2.down, extraCastDistance, platformLayerMask);
       
        if (raycastHit.collider != null)
        {
            playerGroundedRemember = playerGroundedRememberTimer;
        }

        if (playerGroundedRemember > 0)
        {
            return true;
        }
        else
        {
            return false; ;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerJumped = true;
        }

        if (context.canceled)
        {
            if(rb.velocity.y > 0)
            {
                CutJump();
            }
            playerJumped = false;
        }
    }
    private void Crouch()
    {
        if (isGrounded)
        {
            if (isCrouching)
            {
                playerUpperCollider.enabled = false;
                movementSpeed = movementSpeedCrouching;
            }
            else
            {
                playerUpperCollider.enabled = true;
                movementSpeed = movementSpeedRunning;
            }
        }
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCrouching = true;
        }

        if (context.canceled)
        {
            isCrouching = false;
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed && !PlayerStats.FirstFlashlightPickup && !menu.activeInHierarchy)
        {
            flashlightClick.Play();

            if (flashlight.activeInHierarchy)
            { 
                flashlight.SetActive(false);
                fakeFlashlight.SetActive(true);
            } 
            else
            {
                flashlight.SetActive(true);
                fakeFlashlight.SetActive(false);
            }
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        givenCoord=context.ReadValue<Vector2>();
    }
    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.ActionPerformed = true;
        }
    }
    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ui.OpenMenuIngame();
        }
    }
    public void OnRetry(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ui.OpenMenuIngame();
        }
    }
    public void OnExitToMainMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ui.OpenMenuIngame();
        }
    }

}
