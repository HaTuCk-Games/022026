using UnityEngine;

[CreateAssetMenu]
public class Settings : ScriptableObject
{
    public string configName = "Config";
    public int HeroHealth = 100;
    public float PlayerSpeed = 5f;
}
