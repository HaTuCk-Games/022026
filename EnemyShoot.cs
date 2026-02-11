using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform player;
    public float minDistance = 5f;
    public GameObject bullet;
    public float shootDelay;
    private float _shootTime = float.MinValue;

    void Update()
    {
        if (player == null)
        {
            return;
        }
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= minDistance)
        {
            if (Time.time < _shootTime + shootDelay) return;
            _shootTime = Time.time;
            if (bullet != null)
            {
                Instantiate(bullet, transform.position, transform.rotation);
            }
            else
            {
                Debug.Log("No bullet prefab link!");
            }
        }
    }
}
