using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform inventoryGrid; 

    public void AssignInventoryToPlayer(PlayerInventory playerInv)
    {
        playerInv.inventoryGrid = inventoryGrid;
    }
}
