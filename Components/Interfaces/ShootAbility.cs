using System;
using DefaultNamespace;
using DefaultNamespace.Components.Interfaces;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    public GameObject bullet;
    public float shootDelay;
    private float _shootTime = float.MinValue;
    public PlayerStats stats;
    private CharacterData _character;
    private void Start()
    {
        _character = GetComponent<CharacterData>();

        var jsonString = PlayerPrefs.GetString("Stats");

        if(!jsonString.Equals(String.Empty, StringComparison.Ordinal))
        {
            stats = JsonUtility.FromJson<PlayerStats>(jsonString);
        }
        else
        {
            stats = new PlayerStats();
        }
    }

    public void Execute()
    {
        if (Time.time < _shootTime + shootDelay) return;
        _shootTime = Time.time;
        if (bullet != null)
        {
            var t = transform;
            var newBullet = Instantiate(bullet, t.position, t.rotation);
            stats.shotsCount++;
            _character.Score(20);
        }
        else
        {
            Debug.LogError("[SHOOT ABILITY] No bullet prefab link!");
        }
    }
}

