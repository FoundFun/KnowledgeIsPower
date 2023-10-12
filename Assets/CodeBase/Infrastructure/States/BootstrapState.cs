using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Inputs;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            IStaticDataService staticData = RegisterStaticData();
            IRandomService randomService = new UnityRandomService();
            IAssets assets = new AssetProvider();
            IPersistentProgressService progressService = new PersistentProgressService();

            _services.RegisterSingle(InputService());
            _services.RegisterSingle(randomService);
            _services.RegisterSingle(assets);
            _services.RegisterSingle(progressService);

            IUIFactory uiFactory = new UIFactory(assets, staticData, progressService);
            
            _services.RegisterSingle(uiFactory);
            _services.RegisterSingle<IWindowService>(new WindowService(uiFactory));

            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>(),
                    progressService, 
                    staticData, 
                    randomService,
                    _services.Single<IWindowService>()));
            _services.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(progressService,
                    _services.Single<IGameFactory>()));
        }

        private IStaticDataService RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadMonsters();
            _services.RegisterSingle(staticData);

            return staticData;
        }

        private static IInputService InputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}