
public sealed class MovementInfo_LevelUpData
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
            if (value != _level)
            {
                if (_level > 1)
                {
                    movingSpeed_Multiply /= _level;

                    jumpForce_Multiply /= _level;
                }

                _level = value;

                movingSpeed_Multiply *= _level;

                jumpForce_Multiply *= _level;
            }
        }
    }

    public float movingSpeed_Multiply { get; set; }

    public float jumpForce_Multiply { get; set; }

    public MovementInfo_LevelUpData(float movingSpeed_Multiply, float jumpForce_Multiply)
    {
        this.movingSpeed_Multiply = movingSpeed_Multiply;

        this.jumpForce_Multiply = jumpForce_Multiply;
    }

    public MovementInfo_LevelUpData(MovementInfo_LevelUpData movementInfo_LevelUpData)
    {
        _level = movementInfo_LevelUpData._level;

        movingSpeed_Multiply = movementInfo_LevelUpData.movingSpeed_Multiply;

        jumpForce_Multiply = movementInfo_LevelUpData.jumpForce_Multiply;
    }
}