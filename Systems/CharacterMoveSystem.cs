using Unity.Entities;
using UnityEngine;

public class CharacterMoveSystem : ComponentSystem
{
    private EntityQuery _moveQuery;

    protected override void OnCreate()
    {
        _moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<MoveData>(),
            ComponentType.ReadOnly<Transform>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_moveQuery).ForEach(
            (Entity entity, Transform transform, ref InputData inputData, ref MoveData move) =>
            {
                if (transform == null) return;
                var pos = transform.position;
                pos += new Vector3(inputData.Move.x * move.Speed, 0, inputData.Move.y * move.Speed);
                transform.position = pos;
                
                Vector3 moveDirection = new Vector3(inputData.Move.x, 0, inputData.Move.y);
                if (moveDirection.sqrMagnitude > 0.1f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1f);
                }
            });
        
    }
}
