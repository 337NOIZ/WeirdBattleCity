
[System.Serializable]

public sealed class MovementInfo
{
    public float movingSpeed_Walk
    {
        get => movingSpeed_Walk_Calculated;

        private set
        {
            movingSpeed_Walk_Origin = value;

            movingSpeed_Walk_Calculated = movingSpeed_Walk_Origin * movingSpeed_Multiply;
        }
    }

    private float movingSpeed_Walk_Origin;

    private float movingSpeed_Walk_Calculated;

    public float movingSpeed_Run
    {
        get => movingSpeed_Run_Calculated;

        private set
        {
            movingSpeed_Run_Origin = value;

            movingSpeed_Run_Calculated = movingSpeed_Run_Origin * movingSpeed_Multiply;
        }
    }

    private float movingSpeed_Run_Origin;

    private float movingSpeed_Run_Calculated;

    public float movingSpeed_Multiply
    {
        get => movingSpeed_Multiply_Origin;

        set
        {
            movingSpeed_Multiply_Origin = value;

            movingSpeed_Walk = movingSpeed_Walk_Origin * movingSpeed_Multiply_Origin;

            movingSpeed_Run = movingSpeed_Run_Origin * movingSpeed_Multiply_Origin;
        }
    }

    private float movingSpeed_Multiply_Origin;

    public int jumpCount_Max
    {
        get => jumpCount_Max_Calculated;

        private set
        {
            jumpCount_Max_Origin = value;

            jumpCount_Max_Calculated = jumpCount_Max_Origin + jumpCount_Max_Extra_Origin;
        }
    }

    private int jumpCount_Max_Origin;

    private int jumpCount_Max_Calculated;

    public int jumpCount_Max_Extra
    {
        get => jumpCount_Max_Extra_Origin;

        set
        {
            jumpCount_Max_Extra_Origin = value;

            jumpCount_Max_Calculated = jumpCount_Max_Origin + jumpCount_Max_Extra_Origin;
        }
    }

    private int jumpCount_Max_Extra_Origin;

    public float jumpForce
    {
        get => jumpForce_Calculated;

        private set
        {
            jumpForce_Origin = value;

            jumpForce_Calculated = jumpForce_Origin * jumpForce_Multiply_Origin;
        }
    }

    private float jumpForce_Origin;

    private float jumpForce_Calculated;

    public float jumpForce_Multiply
    {
        get => jumpForce_Multiply_Origin;

        set
        {
            jumpForce_Multiply_Origin = value;

            jumpForce_Calculated = jumpForce_Origin * jumpForce_Multiply_Origin;
        }
    }

    private float jumpForce_Multiply_Origin;

    public int jumpCount { get; set; }

    public MovementInfo(MovementData movementData)
    {
        movingSpeed_Walk_Origin = movementData.movingSpeed_Walk;

        movingSpeed_Walk_Calculated = movementData.movingSpeed_Walk;

        movingSpeed_Run_Origin = movementData.movingSpeed_Run;

        movingSpeed_Run_Calculated = movementData.movingSpeed_Run;

        movingSpeed_Multiply_Origin = 1f;

        jumpCount_Max_Origin = movementData.jumpCount_Max;

        jumpCount_Max_Calculated = movementData.jumpCount_Max;

        jumpCount_Max_Extra_Origin = 0;

        jumpForce_Origin = movementData.jumpForce;

        jumpForce_Calculated = movementData.jumpForce;

        jumpForce_Multiply_Origin = 1f;

        Initialize();
    }

    public MovementInfo(MovementInfo movementInfo)
    {
        movingSpeed_Walk_Origin = movementInfo.movingSpeed_Walk_Origin;

        movingSpeed_Walk_Calculated = movementInfo.movingSpeed_Walk_Calculated;

        movingSpeed_Run_Origin = movementInfo.movingSpeed_Run_Origin;

        movingSpeed_Run_Calculated = movementInfo.movingSpeed_Run_Calculated;

        movingSpeed_Multiply_Origin = movementInfo.movingSpeed_Multiply_Origin;

        jumpCount_Max_Origin = movementInfo.jumpCount_Max_Origin;

        jumpCount_Max_Calculated = movementInfo.jumpCount_Max_Calculated;

        jumpCount_Max_Extra_Origin = movementInfo.jumpCount_Max_Extra_Origin;

        jumpForce_Origin = movementInfo.jumpForce_Origin;

        jumpForce_Calculated = movementInfo.jumpForce_Calculated;

        jumpForce_Multiply_Origin = movementInfo.jumpForce_Multiply_Origin;

        jumpCount = movementInfo.jumpCount;
    }

    public void Initialize()
    {
        jumpCount = 0;
    }
}