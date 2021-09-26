
using System.Diagnostics;

public class FixedStopwatch : Stopwatch
{
    public float ElapsedSeconds
    {
        get
        {
            return ElapsedMilliseconds * 0.001f;
        }
    }
}