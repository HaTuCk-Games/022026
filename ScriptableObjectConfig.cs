using UnityEngine;

public class ScriptableObjectConfig : IGameConfig
{
    [SerializeField] private Settings _settings;
    public int HeroHealth => _settings.HeroHealth;
    public float PlayerSpeed => _settings.PlayerSpeed;

    public ScriptableObjectConfig(Settings settings)
    {
        _settings = settings;
    }
}
