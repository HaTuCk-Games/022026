using UnityEngine;
using Zenject;

public class GameConfigProvider : IInitializable
{
    private Settings _settings;

    public void Initialize()
    {
        _settings = Resources.Load<Settings>("GameSettings");
        Debug.Log("Load GameConfig");

        if (_settings == null)
        {
            Debug.LogError("GameConfig not found in Resources!");
            _settings = ScriptableObject.CreateInstance<Settings>();
        }
        else
        {
            Debug.Log("GameConfig loaded successfully!");
        }
    }
    public float GetPlayerSpeed() => _settings.PlayerSpeed;
    public int GetHealth() => _settings.HeroHealth;
    public Settings GetConfig() => _settings;
}