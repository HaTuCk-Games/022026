using DefaultNamespace.Components.Interfaces;
using System.Collections.Generic;
using UnityEngine;
public class ApplyHeal : MonoBehaviour, IAbilityTarget
{
    public int Heal = 100;
    public List<GameObject> Targets { get; set; }
    public void Execute()
    {
        foreach (var target in Targets)
        {
            var health = target.GetComponent<CharacterHealth>();
            if (health != null)
            {
                health.Health += Heal;
                Destroy(gameObject); 
            } 
        }
    }
}
