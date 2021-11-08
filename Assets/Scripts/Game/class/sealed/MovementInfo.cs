
public sealed class MovementInfo
{
    public float movingSpeed_Multiply { get; set; } = 1f;

    public int jumpCount { get; set; } = 0;

    public int jumpCount_Max_Addition { get; set; } = 0;

    public float jumpForce_Multiply { get; set; } = 1f;

    public MovementInfo() { }

    public MovementInfo(MovementInfo movementInfo)
    {
        movingSpeed_Multiply = movementInfo.movingSpeed_Multiply;

        jumpCount = movementInfo.jumpCount;

        jumpCount_Max_Addition = movementInfo.jumpCount_Max_Addition;

        jumpForce_Multiply = movementInfo.jumpForce_Multiply;
    }

    public void Recycling()
    {
        jumpCount = 0;
    }

    public void LevelUp(MovementInfo_LevelUpData movementInfo_LevelUpData)
    {
        movingSpeed_Multiply += movementInfo_LevelUpData.movingSpeed_Multiply;

        jumpForce_Multiply += movementInfo_LevelUpData.jumpForce_Multiply;
    }
}