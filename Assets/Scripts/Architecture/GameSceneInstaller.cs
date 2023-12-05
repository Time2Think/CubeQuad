using GamePlay;
using UI;
using Zenject;

namespace DefaultNamespace
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<HashStringToInt>().AsSingle();
            Container.Bind<ProgressManager>().AsSingle();
        }
    }
}