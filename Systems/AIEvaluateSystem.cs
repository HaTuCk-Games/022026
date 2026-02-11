using DefaultNamespace.Components.Interfaces;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    public class AIEvaluateSystem : ComponentSystem
    {
        private EntityQuery _evaluateQuery;

        protected override void OnCreate()
        {
            _evaluateQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());
        }
        protected override void OnUpdate()
        {
            Entities.With(_evaluateQuery).ForEach(
                (Entity entity, BehaviourManager manager) =>
                {
                    IBehaviour bestBehaviour;
                    float highScore = float.MinValue;

                    manager.activeBehaviour = null;
                    foreach (var behaviour in manager.behaviours)
                    {
                        if (behaviour is IBehaviour ai)
                        {
                            var currentScore = ai.Evaluate();
                            if (currentScore > highScore)
                            {
                                highScore = currentScore;
                                manager.activeBehaviour = ai;
                            }
                        }
                    }
                });
        }
    }
}
