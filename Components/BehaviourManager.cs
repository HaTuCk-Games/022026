using DefaultNamespace.Components.Interfaces;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BehaviourManager : MonoBehaviour, IConvertGameObjectToEntity
{
    public List<MonoBehaviour> behaviours;
    public IBehaviour activeBehaviour;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponent<AIAgent>(entity);
    }
}
public struct AIAgent: IComponentData
{

}