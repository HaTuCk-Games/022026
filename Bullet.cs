using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;          
    public float lifetime = 3f;       
    public Rigidbody rb;           

    void Start()
    { 
        rb = GetComponent<Rigidbody>();     
        rb.velocity = transform.forward * speed;
        Invoke("DestroyBullet", lifetime);
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}