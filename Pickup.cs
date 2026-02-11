using UnityEngine;
using Photon.Pun;

public class Pickup : MonoBehaviourPun
{
    public enum ItemType { Heal, Boost }
    public ItemType type;
    public string iconPrefabName;

    private PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    void OnTriggerEnter(Collider other)
    {
        PhotonView playerPV = other.GetComponent<PhotonView>();
        if (playerPV == null || !playerPV.IsMine) return;
        photonView.RPC("RequestPickup", RpcTarget.MasterClient, playerPV.ViewID);
    }

    [PunRPC]
    private void RequestPickup(int playerViewID)
    {
         PhotonView targetPV = PhotonView.Find(playerViewID);
         if (targetPV != null)
         {
             targetPV.RPC("AddItem", RpcTarget.All, (int)type, iconPrefabName);
         }

         if (PhotonNetwork.IsMasterClient)
         {
             PhotonNetwork.Destroy(gameObject);
             Debug.Log("[Pickup] Предмет удалён MasterClient.");
         }
         else
         {
             PhotonNetwork.Destroy(gameObject);
         }
    }

}