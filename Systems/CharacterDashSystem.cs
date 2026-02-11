using DefaultNamespace.Components.Interfaces;
using Unity.Entities;
using UnityEngine;

public class CharacterDashSystem : ComponentSystem
{
    private EntityQuery _dashQuery;
    protected override void OnCreate()
    {
        _dashQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<UserInputData>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_dashQuery).ForEach(
             (Entity entity, UserInputData inputData, ref InputData input) =>
             {
                 if (input.Dash > 0f && inputData.DashAction != null && inputData.DashAction is IAbility ability)
                 {
                   ability.Execute();
                 }
             });
    }
}
