
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform cameraArm = null;

    [SerializeField] private Transform body = null;

    [Space]

    [SerializeField] private LayerMask groundedCheckLayerMask;

    private CharacterController characterController;

    private InputManager inputManager;

    [SerializeField] private float walkingSpeed = 4f;

    [SerializeField] private float runningSpeed = 6f;

    private Vector2 cameraArmRotation;

    [SerializeField] private float minCameraArmRotationX = -65f;

    [SerializeField] private float maxCameraArmRotationX = 65f;

    [SerializeField] private float cameraRotationSensitivity = 1f;

    [SerializeField] private int jumpCount = 0;

    [SerializeField] private int maxJumpCount = 1;

    [SerializeField] private float jumpForce = 1f;

    [SerializeField] private float gravity = 15f;

    private float groundedCheckSphereOffset;

    private float groundedCheckSphereRadius;

    private float verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        inputManager = GetComponent<InputManager>();
    }

    private void Start()
    {
        cameraArmRotation = Vector2.zero;

        cameraArm.transform.localRotation = Quaternion.Euler(cameraArmRotation.x, cameraArmRotation.y, 0f);

        groundedCheckSphereOffset = characterController.radius;

        groundedCheckSphereRadius = characterController.radius + 0.001f;
    }

    private void Update()
    {
        CharacterControllerMove();

        Attack();
    }

    private void LateUpdate()
    {
        RotateCamera();
    }

    private void OnDrawGizmosSelected()
    {
        if (jumpCount == 0)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.25f);
        }

        else
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
        }

        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + groundedCheckSphereOffset, transform.position.z), groundedCheckSphereRadius);
    }

    private void CharacterControllerMove()
    {
        var moveDirection = Vector3.zero;

        if (inputManager.moveDirection != Vector2.zero)
        {
            var inputDirection = (cameraArm.transform.forward * inputManager.moveDirection.y + cameraArm.transform.right * inputManager.moveDirection.x).normalized;

            moveDirection = inputDirection * (inputManager.isRunKeyPressed ? runningSpeed : walkingSpeed);

            body.forward = new Vector3(cameraArm.transform.forward.x, 0f, cameraArm.transform.forward.z).normalized;
        }

        var spherePosition = new Vector3(transform.position.x, transform.position.y + groundedCheckSphereOffset, transform.position.z);

        if (Physics.CheckSphere(spherePosition, groundedCheckSphereRadius, groundedCheckLayerMask, QueryTriggerInteraction.Ignore))
        {
            jumpCount = 0;

            verticalVelocity = 0f;
        }

        else
        {
            if (jumpCount == 0)
            {
                ++jumpCount;
            }

            verticalVelocity -= gravity * Time.deltaTime;
        }

        if (inputManager.isJumpKeyPressed)
        {
            inputManager.isJumpKeyPressed = false;

            if (jumpCount < maxJumpCount)
            {
                ++jumpCount;

                verticalVelocity = Mathf.Sqrt(jumpForce * gravity * 2f);
            }
        }

        characterController.Move(new Vector3(moveDirection.x, verticalVelocity, moveDirection.z) * Time.deltaTime);
    }

    private void Attack()
    {
        if (inputManager.isAttackKeyPressed)
        {
            Debug.Log("Attack");
        }
    }

    private void RotateCamera()
    {
        if (inputManager.lookDirection != Vector2.zero)
        {
            cameraArmRotation += new Vector2(inputManager.lookDirection.y, inputManager.lookDirection.x) * Time.deltaTime * cameraRotationSensitivity;

            cameraArmRotation.x = Mathf.Clamp(cameraArmRotation.x, minCameraArmRotationX, maxCameraArmRotationX);

            cameraArm.transform.localRotation = Quaternion.Euler(cameraArmRotation.x, cameraArmRotation.y, 0f);
        }
    }
}