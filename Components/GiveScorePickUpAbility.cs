using DefaultNamespace.Components.Interfaces;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GiveScorePickUpAbility : MonoBehaviour, IAbilityTarget, IConvertGameObjectToEntity
{
    public List<GameObject> Targets { get; set; }

    private Entity _entity;
    private EntityManager _dstManager;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _entity = entity;
        _dstManager = dstManager;
    }

    public void Execute()
    {
        foreach (var target in Targets)
        {
            var character = target.GetComponent<CharacterData>();
            if (character != null) character.Score(3);
            Destroy(this.gameObject);
            _dstManager.DestroyEntity(_entity);
        }
    }

}
