using System;

public class CheatingDeath : Singleton<CheatingDeath>
{
    public AntiSpeedUp mAntiSpeedUp { get; private set; }

    public CheatingDeath()
    {
        mAntiSpeedUp = new AntiSpeedUp();
    }
}