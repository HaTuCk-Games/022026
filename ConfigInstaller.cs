using UnityEngine;
using Zenject;

public class ConfigInstaller :MonoInstaller
{
    [SerializeField] private bool useScriptableObject = true;

    [SerializeField] private Settings scriptableConfig;

    public override void InstallBindings()
    {
        if (useScriptableObject)
        {
            Container.BindInstance(scriptableConfig);
            Container
                .Bind<IGameConfig>()
                .To<ScriptableObjectConfig>()
                .AsSingle();
        }
        else
        {
            Container
                .Bind<IGameConfig>()
                .To<DummyConfig>()
                .AsSingle();
        }
    }
}
