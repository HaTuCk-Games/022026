using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InjectionTest : MonoBehaviour
{
    private ITest _test;
    
    [Inject] private GameConfigProvider _settingsProvider;
    [Inject]
    public void Init(ITest t)
    {
        _test = t;
    }
    private void Start()
    {
        _test.Echo();

        if (_settingsProvider == null)
        {
            Debug.LogError("Config provider is not injected!");
            return;
        }
        else
        {
            float speed = _settingsProvider.GetPlayerSpeed();
            int HeroHealth = _settingsProvider.GetHealth();
            Debug.Log($"Player speed: {speed}, Health: {HeroHealth}");
        }

        
    }
}
