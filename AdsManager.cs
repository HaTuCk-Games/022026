using System.Diagnostics;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private const string INTERSTITIAL_ANDROID = "Interstitial_Android";
    private const string INTERSTITIAL_IOS = "Interstitial_iOS";

    [SerializeField] private string androidGameID = "ANDROID_GAME_ID";
    [SerializeField] private string iOSGameID = "IOS_GAME_ID";
    [SerializeField] private bool testMode = true;
    [SerializeField] private Text textComponent;
    [SerializeField] private string baseText = "Монеты: ";

    private string gameID;
    private string adPlacementID;
    private bool isAdReady = false;
    private int coins = 0;
    private bool isLoadingAd = false; 
    private static readonly System.TimeSpan reloadDelay = System.TimeSpan.FromSeconds(5);

    private void Awake()
    {
        InitializePlatformSettings();
        Advertisement.Initialize(gameID, testMode, this);
    }

    private void InitializePlatformSettings()
    {
        gameID = Application.platform == RuntimePlatform.IPhonePlayer ? iOSGameID : androidGameID;
        adPlacementID = Application.platform == RuntimePlatform.IPhonePlayer ? INTERSTITIAL_IOS : INTERSTITIAL_ANDROID;
    }

    public void ShowAd()
    {
        if (isAdReady)
        {
            Advertisement.Show(adPlacementID, this);
            isAdReady = false; 
        }
        else if (!isLoadingAd)
        {
            Debug.LogWarning("Реклама не загружена. Инициируем загрузку...");
            LoadAd();
        }
    }

    public void LoadAd()
    {
        if (isLoadingAd) return;

        isLoadingAd = true;
        Advertisement.Load(adPlacementID, this);
    }

    public int Coins
    {
        get => coins;
        private set
        {
            coins = value;
            UpdateUIText();
        }
    }

    public void AddCoins(int amount = 100)
    {
        Coins += amount;
    }

    public void RemoveCoins(int amount = 100)
    {
        Coins -= amount;
    }

    private void UpdateUIText()
    {
        if (textComponent != null)
        {
            textComponent.text = $"{baseText}{coins}";
        }
        else
        {
            Debug.LogWarning("TextComponent не назначен в инспекторе!");
        }
    }

    #region IUnityAdsInitializationListener
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads инициализирован успешно. Загружаем рекламу...");
        LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Ошибка инициализации Unity Ads: {error} - {message}");
    }
    #endregion

    #region IUnityAdsLoadListener
    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId == adPlacementID)
        {
            isAdReady = true;
            isLoadingAd = false;
            Debug.Log("Реклама успешно загружена!");
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        isLoadingAd = false;
        Debug.LogError($"Ошибка загрузки рекламы {placementId}: {error} - {message}. Повторная попытка через {reloadDelay.Seconds} сек.");
        Invoke(nameof(LoadAd), (float)reloadDelay.TotalSeconds);
    }
    #endregion

    #region IUnityAdsShowListener
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Ошибка показа рекламы {placementId}: {error} - {message}. Загружаем новую рекламу...");
        Invoke(nameof(LoadAd), (float)reloadDelay.TotalSeconds);
    }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"Пользователь кликнул по рекламе: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.COMPLETED:
                AddCoins();
                Debug.Log("Пользователь посмотрел рекламу до конца. Начислены монеты!");
                break;
            case UnityAdsShowCompletionState.SKIPPED:
                Debug.Log("Пользователь пропустил рекламу.");
                break;
        }

        LoadAd(); 
    }
    #endregion
}