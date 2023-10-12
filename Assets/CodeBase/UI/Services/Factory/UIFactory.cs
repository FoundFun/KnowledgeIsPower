using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;

        private Transform _uiRoot;

        public UIFactory(IAssets assets, IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _assets = assets;
            _staticData = staticData;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);
            window.Construct(_progressService);
        }

        public void CreateUIRoot() => 
            _uiRoot = _assets.Instantiate(AssetPath.UIRoot).transform;
    }
}