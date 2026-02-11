using System.Collections;
using Unity.Entities;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public float bulletSpeed;
    public float destroyDelay = 2f;
    private bool isDestroyScheduled = false;

    public void Update()
    {
        if (isDestroyScheduled)
            return;
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    public void Start()
    {
        if (gameObject != null)
        {
            isDestroyScheduled = true;
            Destroy(gameObject, destroyDelay);
        }
    }
}


