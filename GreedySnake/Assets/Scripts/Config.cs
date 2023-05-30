using System;

[Serializable]
public class Config
{
    public Options.Theme theme = Options.Theme.Blue;

    public Options.Mode mode = Options.Mode.Border;

    public Config()
    {
    }

    public Config(Options.Theme theme, Options.Mode mode)
    {
        this.theme = theme;
        this.mode = mode;
    }
}