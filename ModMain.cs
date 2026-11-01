namespace SaveMyEyes;

public class ModMain : IMod
{
    public static ModLog Log { get; private set; }

    public void Init(ModManifest manifest)
    {
        Log = new ModLog(manifest);
        Log.Log("Mod Initialized");
    }

    public void OnEnable()
    {
    }

    public void OnDisable()
    {
    }
}
