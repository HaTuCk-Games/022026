using DefaultNamespace.Components.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoostSpeedAbility : MonoBehaviour, IAbilityTarget
{
    public List<GameObject> Targets { get; set; } = new List<GameObject>();
    

    public void Execute()
    {
        foreach(var target in Targets)
        {
            var character = target.GetComponent<PlayerMovement>();
            if (character == null) return;
            character._speed += 5;
        }
        Destroy(this.gameObject);
    }
}
