
using UnityEngine;

public sealed class PlayerMovement : MonoBehaviour
{
    private GroundedCheckSphere groundedCheckSphere;

    private Player player;

    private PlayerData playerData;

    private PlayerInfo playerInfo;

    private MovementData movementData;

    private MovementInfo movementInfo;

    private Vector3 movePosition = Vector3.zero;

    private Vector2 moveDirection = Vector2.zero;

    private Vector2 lookDirection = Vector2.zero;

    private bool isGrounded = false;

    private bool isRunKeyPressed = false;

    private void Awake()
    {
        groundedCheckSphere = GetComponent<GroundedCheckSphere>();
    }

    public void Initialize(Player player)
    {
        this.player = player;

        playerData = player.playerData;

        playerInfo = player.playerInfo;

        movementData = player.characterData.movementData;

        movementInfo = player.characterInfo.movementInfo;
    }

    private void Update()
    {
        GroundedCheck();

        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        int layer = collision.gameObject.layer;

        if (layer == groundedCheckSphere.nameToLayer)
        {
            isGrounded = true;
        }

        if (layer == groundedCheckSphere.nameToLayer)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == groundedCheckSphere.nameToLayer)
        {
            isGrounded = false;
        }
    }

    private void GroundedCheck()
    {
        if (isGrounded == true)
        {
            if (groundedCheckSphere.Check() == true)
            {
                movementInfo.jumpCount = 0;

                player.animator.SetBool("isGrounded", true);
            }
        }

        else
        {
            if (groundedCheckSphere.Check() == false)
            {
                if (movementInfo.jumpCount == 0)
                {
                    ++movementInfo.jumpCount;
                }

                player.animator.SetBool("isGrounded", false);
            }
        }
    }

    public void Move(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection;
    }

    private void Move()
    {
        Vector3 cameraPivot_Forward = new Vector3(player.cameraPivot.forward.x, 0f, player.cameraPivot.forward.z).normalized;

        Vector3 cameraPivot_Right = new Vector3(player.cameraPivot.right.x, 0f, player.cameraPivot.right.z).normalized;

        if (moveDirection == Vector2.zero)
        {
            player.animator.SetFloat("movingSpeed", 0f);
        }

        else
        {
            movePosition = cameraPivot_Forward * moveDirection.y + cameraPivot_Right * moveDirection.x;

            float movingSpeed = movementData.movingSpeed_Walk * movementInfo.movingSpeed_Multiply;

            player.animator.SetFloat("movingSpeed", movementInfo.movingSpeed_Multiply);

            if (player.animator.GetBool("isAiming") == false)
            {
                if (isRunKeyPressed == false)
                {
                    player.animator.SetFloat("movingDirection_X", 1f);
                }

                else
                {
                    movingSpeed = movementData.movingSpeed_Run * movementInfo.movingSpeed_Multiply;

                    player.animator.SetFloat("movingDirection_X", 2f);
                }

                player.animator.SetFloat("movingDirection_Y", 0f);

                player.animator.transform.forward = movePosition;
            }

            else
            {
                player.animator.SetFloat("movingDirection_X", movePosition.z);

                player.animator.SetFloat("movingDirection_Y", -movePosition.x);
            }

            player.rigidbody.MovePosition(player.rigidbody.position + movePosition * movingSpeed * movementInfo.movingSpeed_Multiply * Time.deltaTime);
        }

        if (player.animator.GetBool("isAiming") == true)
        {
            player.animator.transform.forward = cameraPivot_Forward;
        }

        playerInfo.animator_LocalEulerAngles = player.animator.transform.localEulerAngles;
    }

    public void Look(Vector2 lookDirection)
    {
        this.lookDirection = lookDirection;
    }

    private void Look()
    {
        if (lookDirection != Vector2.zero)
        {
            playerInfo.cameraPivot_LocalEulerAngles += new Vector3(lookDirection.y, lookDirection.x, 0f) * playerInfo.cameraPivot_Sensitivity * Time.deltaTime;

            playerInfo.cameraPivot_LocalEulerAngles = new Vector3(Mathf.Clamp(playerInfo.cameraPivot_LocalEulerAngles.x, playerData.cameraPivot_LocalEulerAngles_Min.x, playerData.cameraPivot_LocalEulerAngles_Max.x), playerInfo.cameraPivot_LocalEulerAngles.y, playerInfo.cameraPivot_LocalEulerAngles.z);
        }

        player.cameraPivot.localEulerAngles = playerInfo.cameraPivot_LocalEulerAngles;
    }

    public void Run()
    {
        isRunKeyPressed = !isRunKeyPressed;
    }

    public void Jump()
    {
        if (movementInfo.jumpCount < movementData.jumpCount_Max + movementInfo.jumpCount_Max_Addition)
        {
            ++movementInfo.jumpCount;

            player.rigidbody.velocity = Vector3.zero;

            player.rigidbody.AddForce(new Vector3(0f, movementData.jumpForce * movementInfo.jumpForce_Multiply, 0f), ForceMode.Impulse);

            player.animator.SetBool("isGrounded", false);

            player.animator.SetTrigger("jump");
        }
    }
}