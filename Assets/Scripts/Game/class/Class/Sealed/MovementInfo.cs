
[System.Serializable]

public sealed class MovementInfo
{
    public float movingSpeed_Walk { get; private set; } = 0;

    public float movingSpeed_Run { get; private set; } = 0;

    public int jumpCount_Max { get; private set; } = 0;

    public float jumpForce { get; private set; } = 0f;

    private float _movingSpeed_Multiply = 1f;

    public float movingSpeed_Multiply
    {
        get { return _movingSpeed_Multiply; }

        set
        {
            if (value > 0f)
            {
                movingSpeed_Walk /= _movingSpeed_Multiply;

                movingSpeed_Run /= _movingSpeed_Multiply;

                _movingSpeed_Multiply = value;

                movingSpeed_Walk *= _movingSpeed_Multiply;

                movingSpeed_Run *= _movingSpeed_Multiply;
            }
        }
    }

    private int _jumpCount_Max_Extra = 0;

    public int jumpCount_Max_Extra
    {
        get { return _jumpCount_Max_Extra; }

        set
        {
            if (value > 0)
            {
                jumpCount_Max -= _jumpCount_Max_Extra;

                _jumpCount_Max_Extra = value;

                jumpCount_Max += _jumpCount_Max_Extra;
            }
        }
    }

    private float _jumpForce_Multiply = 1f;

    public float jumpForce_Multiply
    {
        get { return _jumpForce_Multiply; }

        set
        {
            if (value > 0f)
            {
                jumpForce /= _jumpForce_Multiply;

                _jumpForce_Multiply = value;

                jumpForce *= _jumpForce_Multiply;
            }
        }
    }

    public int jumpCount { get; set; }

    public sealed class LevelUpData
    {
        private int _level = 1;

        public int level
        {
            get
            {
                return _level;
            }

            set
            {
                if (value > 0 && value != _level)
                {
                    movingSpeed_Multiply /= _level;

                    _level = value;

                    movingSpeed_Multiply *= _level;
                }
            }
        }

        public float movingSpeed_Multiply { get; set; }

        public LevelUpData(float movingSpeed_Multiply)
        {
            this.movingSpeed_Multiply = movingSpeed_Multiply;
        }

        public LevelUpData(LevelUpData levelUpData)
        {
            _level = levelUpData._level;

            movingSpeed_Multiply = levelUpData.movingSpeed_Multiply;
        }
    }

    public MovementInfo(MovementData movementData)
    {
        movingSpeed_Walk = movementData.movingSpeed_Walk;

        movingSpeed_Run = movementData.movingSpeed_Run;

        jumpCount_Max = movementData.jumpCount_Max;

        jumpForce = movementData.jumpForce;

        Initialize();
    }

    public MovementInfo(MovementInfo movementInfo)
    {
        movingSpeed_Multiply = movementInfo.movingSpeed_Multiply;

        jumpCount = movementInfo.jumpCount;

        jumpCount_Max_Extra = movementInfo.jumpCount_Max_Extra;

        jumpForce_Multiply = movementInfo.jumpForce_Multiply;
    }

    public void Initialize()
    {
        jumpCount = 0;
    }

    public void LevelUp(LevelUpData levelUpData)
    {
        movingSpeed_Multiply += levelUpData.movingSpeed_Multiply;
    }
}