using Zenject;
using UnityEngine;

public class Playerinfo : MonoBehaviour
{
    private IGameConfig _config;

    [Inject]
    public void Construct(IGameConfig config)
    {
        _config = config;
    }

    private void Start()
    {
        
    }
}