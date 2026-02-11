using UnityEngine;
using Photon.Pun;


public class ItemSpawner : MonoBehaviourPun
{
    public GameObject itemPrefab;
    public int spawnCount = 1;
    public Transform spawnArea;
    public float padding = 1f;
    public float heightOffset = 0f;
    public bool autoSpawnOnStart = true;
    public bool usePhysics = false;
    public bool randomRotation = false;
    public bool isNetworked = true;

    private void Start()
    {
        if (autoSpawnOnStart)
        {
            SpawnItems();
        }
    }
    public void SpawnItems()
    {
        if (itemPrefab == null)
        {
            Debug.LogError("ItemSpawner: Не задан префаб предмета!");
            return;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition();
            Quaternion spawnRotation = GetSpawnRotation();

            GameObject spawnedItem;

            if (isNetworked && PhotonNetwork.IsConnected)
            {
                spawnedItem = PhotonNetwork.Instantiate(itemPrefab.name, spawnPosition, spawnRotation);
            }
            else
            {
                spawnedItem = Instantiate(itemPrefab, spawnPosition, spawnRotation);
            }
            if (usePhysics && spawnedItem != null)
            {
                var rb = spawnedItem.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 position;

        if (spawnArea != null)
        {
            Bounds bounds = new Bounds(spawnArea.position, spawnArea.localScale);
            position = new Vector3(
                Random.Range(bounds.min.x + padding, bounds.max.x - padding),
                bounds.center.y,
                Random.Range(bounds.min.z + padding, bounds.max.z - padding)
            );
        }
        else
        {
            position = transform.position;
        }
        position.y += heightOffset;

        return position;
    }

    private Quaternion GetSpawnRotation()
    {
        if (randomRotation)
        {
            return Quaternion.Euler(
                0f,
                Random.Range(0f, 360f),
                0f
            );
        }
        return transform.rotation;
    }
}
