
public class SettingsBase 
{
    public SettingsBase Instance;

    public SettingsData Data { get; private set; }

    public SettingsBase(SettingsData settings)
    {
        Instance = this;
        Data = settings;
    }
}
