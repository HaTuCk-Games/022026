using System;
using UnityEngine.Tilemaps;
using Unity.Mathematics;
using Unity.Entities;
using UnityEngine;
using static CollisionAbility;


namespace DefaultNamespace.Systems
{
    public class CollisionSystem : ComponentSystem
    {
        private EntityQuery _collisionQuery;
        private Collider[] _results = new Collider[50];

        protected override void OnCreate()
        {
            _collisionQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new ComponentType[]
                {
                    ComponentType.ReadOnly<ActorColliderData>(),
                    ComponentType.ReadOnly<Transform>()
                }
            });
        }

        protected override void OnUpdate()
        {
            var dstManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            Entities.With(_collisionQuery).ForEach(
            (Entity entity, CollisionAbility abilityCollision, ref ActorColliderData colliderData) =>
            {
                if (abilityCollision == null) return;

                var gameObject = abilityCollision.gameObject;
                float3 position = gameObject.transform.position;
                Quaternion rotation = gameObject.transform.rotation;

                abilityCollision.Ñollisions?.Clear();

                int size = 0;

                bool isEntityPlayer = dstManager.HasComponent<PlayerTag>(entity);
                bool isEntityOther = dstManager.HasComponent<OtherTag>(entity);
                switch (colliderData.ColliderType)
                {
                    case (Tile.ColliderType)ColliderType.Sphere:
                        size = Physics.OverlapSphereNonAlloc(
                            colliderData.SphereCenter + position,
                            colliderData.SphereRadius,
                            _results);
                        break;

                    case (Tile.ColliderType)ColliderType.Capsule:
                        var center = ((colliderData.CapsuleStart + position) + (colliderData.CapsuleEnd + position)) / 2f;
                        var point1 = colliderData.CapsuleStart + position;
                        var point2 = colliderData.CapsuleEnd + position;
                        point1 = (float3)(rotation * (point1 - center)) + center;
                        point2 = (float3)(rotation * (point2 - center)) + center;
                        size = Physics.OverlapCapsuleNonAlloc(
                            point1,
                            point2,
                            colliderData.CapsuleRadius, _results);
                        break;

                    case (Tile.ColliderType)ColliderType.Box:
                        size = Physics.OverlapBoxNonAlloc(
                            colliderData.BoxCenter + position,
                            colliderData.BoxHalfExtents, 
                            _results, 
                            colliderData.BoxOrientation * rotation);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (size > 0)
                {
                    foreach (var result in _results)
                    {
                        abilityCollision?.Ñollisions?.Add(result);
                    }
                        abilityCollision.Execute();
                }

                if (isEntityPlayer)
                {
                    foreach (var result in _results)
                    {
                        abilityCollision?.Ñollisions?.Add(result);
                    }
                    abilityCollision.Execute();
                }
            });
        }
    }
}