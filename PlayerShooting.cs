using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;    
    public Transform firePoint;         
    public float fireRate = 0.5f;    
    private float nextFireTime = 0f;  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot(); 
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );
        nextFireTime = Time.time + fireRate;
    }
}