using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Installers/GameConfigInstaller")]
public class GameConfigScriptableInstaller : ScriptableObjectInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameConfigProvider>()
            .AsSingle()
            .NonLazy();
    }
}
