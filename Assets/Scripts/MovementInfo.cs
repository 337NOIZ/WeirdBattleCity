using System;

[Serializable]

public sealed class MovementInfo
{
    private float _movingSpeed_Walk_Origin;

    private float _movingSpeed_Walk;

    public float movingSpeed_Walk
    {
        get => _movingSpeed_Walk;

        private set
        {
            _movingSpeed_Walk_Origin = value;

            _movingSpeed_Walk = _movingSpeed_Walk_Origin * movingSpeed_Multiply;
        }
    }

    private float _movingSpeed_Run_Origin;

    private float _movingSpeed_Run;

    public float movingSpeed_Run
    {
        get => _movingSpeed_Run;

        private set
        {
            _movingSpeed_Run_Origin = value;

            _movingSpeed_Run = _movingSpeed_Run_Origin * movingSpeed_Multiply;
        }
    }

    private float _movingSpeed_Multiply;

    public float movingSpeed_Multiply
    {
        get => _movingSpeed_Multiply;

        set
        {
            _movingSpeed_Multiply = value;

            _movingSpeed_Walk = _movingSpeed_Walk_Origin * _movingSpeed_Multiply;

            _movingSpeed_Run = _movingSpeed_Run_Origin * _movingSpeed_Multiply;
        }
    }

    private int _jumpCount_Max_Origin;

    private int _jumpCount_Max;

    public int jumpCount_Max
    {
        get => _jumpCount_Max;

        private set
        {
            _jumpCount_Max_Origin = value;

            _jumpCount_Max = _jumpCount_Max_Origin + _jumpCount_Max_Extra;
        }
    }

    private int _jumpCount_Max_Extra;

    public int jumpCount_Max_Extra
    {
        get => _jumpCount_Max_Extra;

        set
        {
            _jumpCount_Max_Extra = value;

            _jumpCount_Max = _jumpCount_Max_Origin + _jumpCount_Max_Extra;
        }
    }

    private float _jumpForce_Origin;

    private float _jumpForce;

    public float jumpForce
    {
        get => _jumpForce;

        private set
        {
            _jumpForce_Origin = value;

            _jumpForce = _jumpForce_Origin * _jumpForce_Multiply;
        }
    }

    private float _jumpForce_Multiply;

    public float jumpForce_Multiply
    {
        get => _jumpForce_Multiply;

        set
        {
            _jumpForce_Multiply = value;

            _jumpForce = _jumpForce_Origin * _jumpForce_Multiply;
        }
    }

    public int jumpCount { get; set; }

    public MovementInfo(MovementData movementData)
    {
        _movingSpeed_Walk_Origin = movementData.movingSpeed_Walk;

        _movingSpeed_Walk = movementData.movingSpeed_Walk;

        _movingSpeed_Run_Origin = movementData.movingSpeed_Run;

        _movingSpeed_Run = movementData.movingSpeed_Run;

        _movingSpeed_Multiply = 1f;

        _jumpCount_Max_Origin = movementData.jumpCount_Max;

        _jumpCount_Max = movementData.jumpCount_Max;

        _jumpCount_Max_Extra = 0;

        _jumpForce_Origin = movementData.jumpForce;

        _jumpForce = movementData.jumpForce;

        _jumpForce_Multiply = 1f;

        Initialize();
    }

    public MovementInfo(MovementInfo movementInfo)
    {
        _movingSpeed_Walk_Origin = movementInfo._movingSpeed_Walk_Origin;

        _movingSpeed_Walk = movementInfo._movingSpeed_Walk;

        _movingSpeed_Run_Origin = movementInfo._movingSpeed_Run_Origin;

        _movingSpeed_Run = movementInfo._movingSpeed_Run;

        _movingSpeed_Multiply = movementInfo._movingSpeed_Multiply;

        _jumpCount_Max_Origin = movementInfo._jumpCount_Max_Origin;

        _jumpCount_Max = movementInfo._jumpCount_Max;

        _jumpCount_Max_Extra = movementInfo._jumpCount_Max_Extra;

        _jumpForce_Origin = movementInfo._jumpForce_Origin;

        _jumpForce = movementInfo._jumpForce;

        _jumpForce_Multiply = movementInfo._jumpForce_Multiply;

        jumpCount = movementInfo.jumpCount;
    }

    public void Initialize()
    {
        jumpCount = 0;
    }
}