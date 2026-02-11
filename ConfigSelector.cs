using UnityEditor;
using UnityEngine;

public class ConfigSelector : MonoBehaviour
{
    public Settings currentConfig;
    public string configPath = "";

    public void ApplyConfig(Settings config)
    {
        currentConfig = config;
        configPath = config ? AssetDatabase.GetAssetPath(config) : "";
        Debug.Log("Applied config: " + config.configName + " | Path: " + configPath);
    }

    private void OnDrawGizmos()
    {
        if (currentConfig != null)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.yellow;
            Handles.Label(transform.position + Vector3.up * 2f,
                "Config: " + currentConfig.configName, style);
        }
    }

}