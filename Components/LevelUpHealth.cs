using System.Collections;
using DefaultNamespace.Components.Interfaces;
using UnityEngine;

public class LevelUpHealth : MonoBehaviour, ILevelUp
{
    public int MinLevel => _minLevel;

    public int _minLevel;

    private CharacterHealth _health;
    public void LevelUp(CharacterData data, int level)
    {
        if(_health == null)
        {
            _health = GetComponent<CharacterHealth>();
            if (_health == null) return;
        }
        if(data.CurrentLevel >= MinLevel)
        {
            _health.Health += 10;
        } 
    }
   
}
