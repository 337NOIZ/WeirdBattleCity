
using UnityEngine;

using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

    public Vector2 lookDirection { get; private set; } = Vector2.zero;

    public Vector2 moveDirection { get; private set; } = Vector2.zero;

    public bool isAttackKeyPressed { get; private set; } = false;

    public bool isJumpKeyPressed { get; set; } = false;

    public bool isRunKeyPressed { get; private set; } = false;

    private void Awake()
    {
        instance = this;
    }

    public void OnLook(InputValue value)
    {
        Look(value.Get<Vector2>());
    }

    public void Look(Vector2 direction)
    {
        lookDirection = direction;
    }

    public void OnMove(InputValue value)
    {
        Move(value.Get<Vector2>());
    }

    public void Move(Vector2 direction)
    {
        moveDirection = direction;
    }

    public void OnAttack(InputValue value)
    {
        Attack(value.isPressed);
    }

    public void Attack(bool state)
    {
        isAttackKeyPressed = state;
    }

    public void OnJump(InputValue value)
    {
        Jump(value.isPressed);
    }

    public void Jump(bool state)
    {
        isJumpKeyPressed = state;
    }

    public void OnRun(InputValue value)
    {
        Run(value.isPressed);
    }

    public void Run(bool state)
    {
        isRunKeyPressed = state;
    }
}