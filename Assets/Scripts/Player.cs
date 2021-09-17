
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

public enum PlayerState
{
    standing, walking, running, attacking, jumping
}

public class Player : MonoBehaviour
{
    [SerializeField] private Transform cameraArm = null;

    [SerializeField] private Transform body = null;

    [SerializeField] private TouchScreen touchScreen = null;

    [SerializeField] private VirtualJoystick virtualJoystick = null;

    [SerializeField] private Button attackButton = null;

    [SerializeField] private Button jumpButton = null;

    private new Rigidbody rigidbody;

    private Vector3 cameraRotation;

    public float cameraRotation_x_min = -55;

    public float cameraRotation_x_max = 50f;

    public float cameraRotation_Sensitivity = 2f;

    public float walkingSpeed = 5f;

    public float runningSpeed = 5f;

    public int jumpCount = 0;

    public int jumpCount_max = 1;

    public int jumpForce = 5;

    private Dictionary<PlayerState, bool> playerStates = new Dictionary<PlayerState, bool>();

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        touchScreen.onDragDelegate += RotateCamera;

        virtualJoystick.onDragDelegate += Move;

        jumpButton.onClick.AddListener(Jump);

        attackButton.onClick.AddListener(Attack);

        cameraRotation = Vector3.zero;

        playerStates.Add(PlayerState.standing, false);

        playerStates.Add(PlayerState.walking, false);

        playerStates.Add(PlayerState.running, false);

        playerStates.Add(PlayerState.attacking, false);

        playerStates.Add(PlayerState.jumping, false);

    }

    private void Update()
    {
#if UNITY_EDITOR

        var inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(inputDirection != Vector2.zero)
        {
            Move(inputDirection);
        }

#endif
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            playerStates[PlayerState.jumping] = false;

            jumpCount = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (!playerStates[PlayerState.jumping])
            {
                playerStates[PlayerState.jumping] = true;

                //jumpCount += 1;
            }
        }
    }

    private void RotateCamera()
    {
        cameraRotation += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f) * cameraRotation_Sensitivity;

        if(cameraRotation.x < cameraRotation_x_min)
        {
            cameraRotation.x = cameraRotation_x_min;
        }

        else if (cameraRotation.x > cameraRotation_x_max)
        {
            cameraRotation.x = cameraRotation_x_max;
        }

        cameraArm.localRotation = Quaternion.Euler(cameraRotation);
    }

    private void Move(Vector2 moveDirection)
    {
        body.forward = (new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z) * moveDirection.y + new Vector3(cameraArm.right.x, 0f, cameraArm.right.z) * moveDirection.x).normalized;

        transform.position += body.forward * Time.deltaTime * walkingSpeed;
    }

    private void Attack()
    {
        Debug.Log("Attack");
    }

    private void Jump()
    {
        if(jumpCount < jumpCount_max)
        {
            ++jumpCount;

            playerStates[PlayerState.jumping] = true;

            rigidbody.velocity = Vector3.zero;

            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}