using TMPro;

namespace CodeBase.UI.Windows
{
    class ShopWindow : WindowBase
    {
        public TextMeshProUGUI MoneyText;

        protected override void Initialize() => 
            RefreshMoneyText();

        protected override void SubscribeUpdates() => 
            Progress.WorldData.LootData.Changed += RefreshMoneyText;

        protected override void Cleanup()
        {
            base.Cleanup();
            Progress.WorldData.LootData.Changed -= RefreshMoneyText;
        }

        private void RefreshMoneyText() => 
            MoneyText.text = Progress.WorldData.LootData.Collected.ToString();
    }
}