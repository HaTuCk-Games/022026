using UnityEngine;
using Photon.Pun;

public class TriggerDestroyer : MonoBehaviour
{
    private PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
            photonView.RPC("DestroyObjectRPC", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void DestroyObjectRPC()
    {
        Destroy(gameObject);
    }
}
