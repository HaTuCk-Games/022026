using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;
using DefaultNamespace.Components.Interfaces;

[System.Serializable]
public class PlayerInventory : MonoBehaviourPun
{
    public Transform inventoryGrid;
    private Dictionary<string, GameObject> iconPrefabs = new Dictionary<string, GameObject>();
    private List<GameObject> itemIcons = new List<GameObject>();

    [PunRPC]
    public void AddItem(int itemTypeInt, string iconPrefabName)
    {
        if (!iconPrefabs.ContainsKey(iconPrefabName))
        {
            GameObject prefab = Resources.Load<GameObject>(iconPrefabName);
            if (prefab == null)
            {
                Debug.LogError("Не найден префаб иконки: " + iconPrefabName);
                return;
            }
            iconPrefabs[iconPrefabName] = prefab;
        }

        GameObject iconPrefab = iconPrefabs[iconPrefabName];

        if (iconPrefab != null && inventoryGrid != null)
        {
            GameObject icon = Instantiate(iconPrefab, inventoryGrid);
            itemIcons.Add(icon);

            IAbilityTarget ability = icon.GetComponent<IAbilityTarget>();
            if (ability != null)
            { 
                if (!ability.Targets.Contains(this.gameObject))
                {
                    ability.Targets.Add(this.gameObject); 
                }
            }
        }
    }
    public void ClearInventory()
    {
        foreach (GameObject icon in itemIcons)
        {
            Destroy(icon);
        }
        itemIcons.Clear();
    }
}