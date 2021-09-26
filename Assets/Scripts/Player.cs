
using UnityEngine;

public class Player : MonoBehaviour
{
    [Space]

    [SerializeField] private Transform cameraArm = null;

    [SerializeField] private Animator animator = null;

    private new Rigidbody rigidbody = null;

    private InputManager inputManager;

    [Space]

    [SerializeField] private LayerMask groundedCheckLayerMask;

    [Space]

    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private float jumpForceMultiply = 1f;

    [Space]

    [SerializeField] private int jumpCount = 0;

    [SerializeField] private int maxJumpCount = 1;

    [Space]

    [SerializeField] private float movingSpeed = 3f;

    [SerializeField] private float movingSpeedMultiply = 1f;

    [Space]

    [SerializeField] private float runningSpeed = 2f;

    [Space]

    [SerializeField] private Vector2 cameraArmRotation;

    [Space]

    [SerializeField] private float cameraArmRotationSensitivity = 1f;

    [Space]

    [SerializeField] private float minCameraArmRotationX = -55f;

    [SerializeField] private float maxCameraArmRotationX = 55f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        inputManager = InputManager.instance;

        cameraArmRotation = Vector2.zero;

        cameraArm.transform.localRotation = Quaternion.Euler(cameraArmRotation.x, cameraArmRotation.y, 0f);
    }

    private void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        Jump();

        Move();
    }

    private void LateUpdate()
    {
        Look();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpCount = 0;

            animator.SetBool("isGrounded", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (jumpCount == 0)
            {
                ++jumpCount;
            }

            animator.SetBool("isGrounded", false);
        }
    }

    private void Attack()
    {
        if (inputManager.isAttackKeyPressed)
        {
            Debug.Log("Attack");
        }
    }

    private void Jump()
    {
        if (inputManager.isJumpKeyPressed)
        {
            inputManager.isJumpKeyPressed = false;

            if (jumpCount < maxJumpCount)
            {
                ++jumpCount;

                rigidbody.velocity = Vector3.zero;

                rigidbody.AddForce(Vector3.up * jumpForce * jumpForceMultiply, ForceMode.Impulse);

                animator.SetTrigger("jump");
            }
        }
    }

    private void Move()
    {
        var movePosition = Vector3.zero;

        if (inputManager.moveDirection == Vector2.zero)
        {
            animator.SetFloat("movingSpeed", 0f);
        }

        else
        {
            movePosition = (cameraArm.transform.forward * inputManager.moveDirection.y + cameraArm.transform.right * inputManager.moveDirection.x).normalized;

            animator.transform.forward = new Vector3(movePosition.x, 0f, movePosition.z);

            movePosition *= movingSpeed * movingSpeedMultiply;

            if (inputManager.isRunKeyPressed == false)
            {
                animator.SetFloat("movingSpeed", movingSpeedMultiply);
            }

            else
            {
                movePosition *= runningSpeed;

                animator.SetFloat("movingSpeed", movingSpeedMultiply * runningSpeed);
            }
        }

        rigidbody.MovePosition(rigidbody.position + movePosition * Time.deltaTime);
    }

    private void Look()
    {
        if (inputManager.lookDirection != Vector2.zero)
        {
            cameraArmRotation += new Vector2(inputManager.lookDirection.y, inputManager.lookDirection.x) * Time.deltaTime * cameraArmRotationSensitivity;

            cameraArmRotation.x = Mathf.Clamp(cameraArmRotation.x, minCameraArmRotationX, maxCameraArmRotationX);

            cameraArm.transform.localRotation = Quaternion.Euler(cameraArmRotation.x, cameraArmRotation.y, 0f);
        }
    }
}