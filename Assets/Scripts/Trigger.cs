
public class Trigger
{
    public bool reloaded { get; private set; }

    public bool Bang()
    {
        if (reloaded == true)
        {
            reloaded = false;

            return true;
        }

        return false;
    }

    public void Reload()
    {
        reloaded = true;
    }
}
