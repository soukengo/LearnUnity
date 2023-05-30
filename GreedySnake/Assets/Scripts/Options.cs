using System;

public abstract class Options
{
    [Flags]
    public enum Theme
    {
        Blue = 1,
        Yellow = 2
    }

    [Flags]
    public enum Mode
    {
        Freedom = 1,
        Border = 2
    }
}