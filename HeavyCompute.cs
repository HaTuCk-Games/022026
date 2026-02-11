using System.Threading.Tasks;
using UnityEngine;
using System.IO;

public class HeavyCompute : MonoBehaviour
{
    private AppConfig _config;
    public AppConfig Config => _config;
    void Start()
    {
        LoadConfigAsync();
        LoadSuperHeavyConfigAsync();
    }
    private async void LoadConfigAsync()
    {
        _config = await LoadConfigFromFileAsync();
        ApplySettings();
    }
    private async void LoadSuperHeavyConfigAsync()
    {
        await Task.Delay(10000);
        _config = await LoadSuperHeavyConfigFromFileAsync();
        ApplySettings();
    }
    private async Task<AppConfig> LoadConfigFromFileAsync()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("config");

        if (textAsset == null)
        {
           throw new FileNotFoundException("Файл конфигураций не найден в Resources!");
        }

        AppConfig config = JsonUtility.FromJson<AppConfig>(textAsset.text);
        return config;
    }
    private async Task<AppConfig> LoadSuperHeavyConfigFromFileAsync()
    {
         TextAsset textAsset = Resources.Load<TextAsset>("config2");

         if (textAsset == null)
         {
             throw new FileNotFoundException("Файл конфигураций не найден в Resources!");
         }

         AppConfig config = JsonUtility.FromJson<AppConfig>(textAsset.text);
         return config;
    }
    private void ApplySettings()
    {
        if (_config == null) return;
        AudioListener.volume = _config.volume;
        Screen.fullScreen = _config.fullscreen;
        if (!_config.fullscreen)
        {
            Screen.SetResolution(_config.resolutionX, _config.resolutionY, false);
        }
        Debug.Log($"Громкость: {_config.volume}");
        Debug.Log($"Полноэкранный режим: {_config.fullscreen}");
        Debug.Log($"Разрешение: {_config.resolutionX}x{_config.resolutionY}");
        Debug.Log($"Язык: {_config.language}");
    }
}
