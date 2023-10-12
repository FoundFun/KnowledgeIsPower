using CodeBase.UI.Services.Factory;

namespace CodeBase.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uifactory)
        {
            _uiFactory = uifactory;
        }
        
        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.Shop:
                    _uiFactory.CreateShop();
                    break;
            }
        }
    }
}