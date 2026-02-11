
using System.Collections.Generic;
using UnityEngine;



public class TrapAbility: CollisionAbility
{
        public int Damage = 10;

        public void Execute()
        {
            foreach (var target in Ñollisions)
            {
                var targetHealth = target?.gameObject?.GetComponent<CharacterHealth> ();
                if(targetHealth != null)
                {
                targetHealth.Health -= Damage;
                }
            }
        }
 
}


