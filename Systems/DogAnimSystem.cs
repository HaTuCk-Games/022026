using System;
using Unity.Entities;
using UnityEngine;
using Unity.Mathematics;

public class DogAnimSystem : ComponentSystem
{
    private EntityQuery _animQuery;

    protected override void OnCreate()
    {
        _animQuery = GetEntityQuery(ComponentType.ReadOnly<AnimData>(), ComponentType.ReadOnly<Animator>());     
    }
    protected override void OnUpdate()
    {
        Entities.With(_animQuery).ForEach((Entity entity, ref InputData move, Animator animator, UserInputData inputData) =>
        {
            animator.SetBool(inputData.moveAnimHash, Mathf.Abs(move.Move.x) > 0.05f || Math.Abs(move.Move.y) > 0.05f);
            if (inputData.moveAnimSpeedHash == String.Empty) return;

            animator.SetFloat(inputData.moveAnimSpeedHash, inputData.speed*math.distance(move.Move.x, move.Move.y));
        });
        
    }
}
