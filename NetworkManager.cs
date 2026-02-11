using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerSample;
    public GameObject Item1;
    public GameObject Item2;
    public InventoryManager inventoryManager;
    public List<Transform> SpawnPoints;

    private const int REQUIRED_SPAWN_POINTS = 5;
    private int _reconnectAttempts = 0;
    private float _reconnectDelay = 1f;
    private bool _isReconnecting = false;
    private bool _hasAttemptedJoin = false;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.BackgroundTimeout = 30000;
        PhotonNetwork.NetworkingClient.DisconnectTimeout = 30000;
    }

    public override void OnConnectedToMaster()
    {
        JoinRoom();
    }

    private void JoinRoom()
    {
        if (_hasAttemptedJoin) return;
        _hasAttemptedJoin = true;

        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 6,
            IsVisible = false
        };

        PhotonNetwork.JoinOrCreateRoom("test", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Успешно вошёл в комнату. Спавним игрока и предметы.");
        var id = PhotonNetwork.LocalPlayer.ActorNumber;
        SpawnPlayer(id);
        CheckAndSpawnItems();
    }

    private void SpawnPlayer(int playerId)
    {
        GameObject playerInstance = PhotonNetwork.Instantiate(PlayerSample.name, SpawnPoints[0].position, SpawnPoints[0].rotation);
       
        PlayerInventory playerInv = playerInstance.GetComponent<PlayerInventory>();
        if (playerInv == null)
        {
            Debug.LogError("PlayerInventory не найден на спавненном объекте!");
            return;
        }

        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager не назначен в NetworkManager!");
            return;
        }

        if (inventoryManager.inventoryGrid == null)
        {
            Debug.LogError("inventoryGrid не назначен в InventoryManager!");
            return;
        }

        inventoryManager.AssignInventoryToPlayer(playerInv);
        Debug.Log("Инвентарь подключен для игрока " + playerId);

        if (playerInv.inventoryGrid == null)
            Debug.LogError("ОШИБКА: inventoryGrid остался null!");
    }

    private void CheckAndSpawnItems()
    {
        if (SpawnPoints.Count < REQUIRED_SPAWN_POINTS)
        {
            return;
        }

        GameObject[] existingItem1s = GameObject.FindGameObjectsWithTag("Item1");
        GameObject[] existingItem2s = GameObject.FindGameObjectsWithTag("Item2");

        if (existingItem1s.Length < 2)
        {
            for (int i = 2; i <= 3; i++)
                PhotonNetwork.Instantiate(Item1.name, SpawnPoints[i].position, SpawnPoints[i].rotation);
        }

        if (existingItem2s.Length < 1)
        {
            PhotonNetwork.Instantiate(Item2.name, SpawnPoints[4].position, SpawnPoints[4].rotation);
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning($"Не удалось войти в комнату: {returnCode} {message}. Повторяем через 1 сек...");
        _hasAttemptedJoin = false;
        StartCoroutine(RetryJoinAfterDelay());
    }

    private IEnumerator RetryJoinAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        JoinRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        string causeStr = cause.ToString();
        Debug.LogWarning($"Отключено от Photon: {causeStr}");

        if (IsRecoverableDisconnect(causeStr))
        {
            AttemptReconnect();
        }
        else
        {
            Debug.Log("Критическое отключение. Требуется ручное переподключение.");
        }
    }

    private bool IsRecoverableDisconnect(string cause)
    {
        return cause == "ClientTimeout" ||
               cause == "ServerTimeout" ||
               cause == "DisconnectByServer" ||
               cause == "UnexpectedDisconnect" ||
               cause == "Timeout";
    }

    private void AttemptReconnect()
    {
        if (_isReconnecting) return;
        _isReconnecting = true;

        if (_reconnectAttempts < 3)
        {
            _reconnectAttempts++;
            float delay = Mathf.Pow(2, _reconnectAttempts - 1);

            Debug.Log($"Попытка переподключения #{_reconnectAttempts} через {delay} сек...");

            StartCoroutine(DoReconnectAfterDelay(delay));
        }
        else
        {
            Debug.LogError("Исчерпаны попытки переподключения. Используйте RestartConnection().");
            _isReconnecting = false;
        }
    }

    private IEnumerator DoReconnectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PhotonNetwork.ReconnectAndRejoin();

        yield return new WaitForSeconds(2f);

        if (!PhotonNetwork.IsConnectedAndReady)
        {
            _isReconnecting = false;
            AttemptReconnect();
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && PhotonNetwork.IsConnected)
        {
            PhotonNetwork.SendOutgoingCommands();
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && PhotonNetwork.IsConnected && !PhotonNetwork.InRoom)
        {
            _hasAttemptedJoin = false;
            JoinRoom();
        }
    }

    public void RestartConnection()
    {
        Debug.Log("Перезапуск подключения к Photon...");
        _reconnectAttempts = 0;
        _hasAttemptedJoin = false;
        _isReconnecting = false;

        PhotonNetwork.Disconnect();
        PhotonNetwork.ConnectUsingSettings();
    }
}