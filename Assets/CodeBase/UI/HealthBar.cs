using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class HealthBar : MonoBehaviour
    {
        public Image ImageCurrent;

        public void SetValue(float current, float max) =>
            ImageCurrent.fillAmount = current / max;
    }
}