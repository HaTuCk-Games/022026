using UnityEngine;
using Photon.Pun;

public class TriggerHider : MonoBehaviour
{
    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            photonView.RPC("HideObject", RpcTarget.All);
        }
    }

    [PunRPC]
    private void HideObject()
    {
        gameObject.SetActive(false);
    }
}