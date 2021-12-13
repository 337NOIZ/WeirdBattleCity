
[System.Serializable]

public sealed class MovementInfo
{
    private float _movingSpeed_Walk_Origin;

    private float _movingSpeed_Walk_;

    public float movingSpeed_Walk
    {
        get => _movingSpeed_Walk_;

        private set
        {
            _movingSpeed_Walk_Origin = value;

            _movingSpeed_Walk_ = _movingSpeed_Walk_Origin * movingSpeed_Multiply;
        }
    }

    private float _movingSpeed_Run_Origin;

    private float _movingSpeed_Run_;

    public float movingSpeed_Run
    {
        get => _movingSpeed_Run_;

        private set
        {
            _movingSpeed_Run_Origin = value;

            _movingSpeed_Run_ = _movingSpeed_Run_Origin * movingSpeed_Multiply;
        }
    }

    private float _movingSpeed_Multiply_;

    public float movingSpeed_Multiply
    {
        get => _movingSpeed_Multiply_;

        set
        {
            _movingSpeed_Multiply_ = value;

            _movingSpeed_Walk_ = _movingSpeed_Walk_Origin * _movingSpeed_Multiply_;

            _movingSpeed_Run_ = _movingSpeed_Run_Origin * _movingSpeed_Multiply_;
        }
    }

    private int _jumpCount_Max_Origin;

    private int _jumpCount_Max_;

    public int jumpCount_Max
    {
        get => _jumpCount_Max_;

        private set
        {
            _jumpCount_Max_Origin = value;

            _jumpCount_Max_ = _jumpCount_Max_Origin + _jumpCount_Max_Extra_;
        }
    }

    private int _jumpCount_Max_Extra_;

    public int jumpCount_Max_Extra
    {
        get => _jumpCount_Max_Extra_;

        set
        {
            _jumpCount_Max_Extra_ = value;

            _jumpCount_Max_ = _jumpCount_Max_Origin + _jumpCount_Max_Extra_;
        }
    }

    private float _jumpForce_Origin;

    private float _jumpForce_;

    public float jumpForce
    {
        get => _jumpForce_;

        private set
        {
            _jumpForce_Origin = value;

            _jumpForce_ = _jumpForce_Origin * _jumpForce_Multiply_;
        }
    }

    private float _jumpForce_Multiply_;

    public float jumpForce_Multiply
    {
        get => _jumpForce_Multiply_;

        set
        {
            _jumpForce_Multiply_ = value;

            _jumpForce_ = _jumpForce_Origin * _jumpForce_Multiply_;
        }
    }

    public int jumpCount { get; set; }

    public MovementInfo(MovementData movementData)
    {
        _movingSpeed_Walk_Origin = movementData.movingSpeed_Walk;

        _movingSpeed_Walk_ = movementData.movingSpeed_Walk;

        _movingSpeed_Run_Origin = movementData.movingSpeed_Run;

        _movingSpeed_Run_ = movementData.movingSpeed_Run;

        _movingSpeed_Multiply_ = 1f;

        _jumpCount_Max_Origin = movementData.jumpCount_Max;

        _jumpCount_Max_ = movementData.jumpCount_Max;

        _jumpCount_Max_Extra_ = 0;

        _jumpForce_Origin = movementData.jumpForce;

        _jumpForce_ = movementData.jumpForce;

        _jumpForce_Multiply_ = 1f;

        Initialize();
    }

    public MovementInfo(MovementInfo movementInfo)
    {
        _movingSpeed_Walk_Origin = movementInfo._movingSpeed_Walk_Origin;

        _movingSpeed_Walk_ = movementInfo._movingSpeed_Walk_;

        _movingSpeed_Run_Origin = movementInfo._movingSpeed_Run_Origin;

        _movingSpeed_Run_ = movementInfo._movingSpeed_Run_;

        _movingSpeed_Multiply_ = movementInfo._movingSpeed_Multiply_;

        _jumpCount_Max_Origin = movementInfo._jumpCount_Max_Origin;

        _jumpCount_Max_ = movementInfo._jumpCount_Max_;

        _jumpCount_Max_Extra_ = movementInfo._jumpCount_Max_Extra_;

        _jumpForce_Origin = movementInfo._jumpForce_Origin;

        _jumpForce_ = movementInfo._jumpForce_;

        _jumpForce_Multiply_ = movementInfo._jumpForce_Multiply_;

        jumpCount = movementInfo.jumpCount;
    }

    public void Initialize()
    {
        jumpCount = 0;
    }
}