using System.Collections;
using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        public GameObject Skull;
        public GameObject PickUpFx;
        public GameObject PickupPopup;
        public TMP_Text LootText;

        private Loot _loot;
        private WorldData _worldData;
        private bool _picked;

        private void OnTriggerEnter(Collider other) => Pickup();

        public void Construct(WorldData worldData) => 
            _worldData = worldData;

        public void Initialize(Loot loot) => 
            _loot = loot;

        private void Pickup()
        {
            if(_picked)
                return;
            
            _picked = true;

            UpdateWorldData();
            HideSkull();
            PlayPickupFx();
            ShowText();
            StartCoroutine(StartDestroyTimer());
        }

        private void UpdateWorldData() => 
            _worldData.LootData.Collect(_loot);

        private GameObject PlayPickupFx() => 
            Instantiate(PickUpFx, transform.position, Quaternion.identity);

        private void HideSkull() => 
            Skull.SetActive(false);

        private void ShowText()
        {
            LootText.text = $"{_loot.Value}";
            PickupPopup.SetActive(true);
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1.5f);
            
            Destroy(gameObject);
        }
    }
}