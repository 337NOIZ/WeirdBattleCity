
public class MovementData
{
    public float movingSpeed_walk { get; private set; } = 0;

    public float movingSpeed_run { get; private set; } = 0;

    public int jumpCount_Max { get; set; } = 0;

    public float jumpForce { get; private set; } = 0f;

    public MovementData()
    {

    }

    public MovementData(float movingSpeed_walk, float movingSpeed_run)
    {
        this.movingSpeed_walk = movingSpeed_walk;

        this.movingSpeed_run = movingSpeed_run;
    }

    public MovementData(float movingSpeed_walk, float movingSpeed_run, int jumpCount_Max, float jumpForce)
    {
        this.movingSpeed_walk = movingSpeed_walk;

        this.movingSpeed_run = movingSpeed_run;

        this.jumpCount_Max = jumpCount_Max;

        this.jumpForce = jumpForce;
    }

    public MovementData(MovementData movementData)
    {
        movingSpeed_walk = movementData.movingSpeed_walk;

        movingSpeed_run = movementData.movingSpeed_run;

        jumpCount_Max = movementData.jumpCount_Max;

        jumpForce = movementData.jumpForce;
    }
}