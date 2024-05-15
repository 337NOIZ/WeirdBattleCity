public sealed class MovementData
{
    public float movingSpeed_Walk { get; private set; } = 0;

    public float movingSpeed_Run { get; private set; } = 0;

    public int jumpCount_Max { get; set; } = 0;

    public float jumpForce { get; private set; } = 0f;

    public MovementData() { }

    public MovementData(float movingSpeed_Walk, float movingSpeed_Run)
    {
        this.movingSpeed_Walk = movingSpeed_Walk;

        this.movingSpeed_Run = movingSpeed_Run;
    }

    public MovementData(float movingSpeed_Walk, float movingSpeed_Run, int jumpCount_Max, float jumpForce)
    {
        this.movingSpeed_Walk = movingSpeed_Walk;

        this.movingSpeed_Run = movingSpeed_Run;

        this.jumpCount_Max = jumpCount_Max;

        this.jumpForce = jumpForce;
    }

    public MovementData(MovementData movementData)
    {
        movingSpeed_Walk = movementData.movingSpeed_Walk;

        movingSpeed_Run = movementData.movingSpeed_Run;

        jumpCount_Max = movementData.jumpCount_Max;

        jumpForce = movementData.jumpForce;
    }
}