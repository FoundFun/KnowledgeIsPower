using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;
        
        private IGameFactory _gameFactory;
        private IRandomService _randomService;
        private int _minValue;
        private int _maxValue;

        public void Construct(IGameFactory gameFactory, IRandomService randomService)
        {
            _gameFactory = gameFactory;
            _randomService = randomService;
        }

        private void Start()
        {
            EnemyDeath.Happened += SpawnLoot;
        }

        public void SetLootValue(int min, int max)
        {
            _minValue = min;
            _maxValue = max;
        }

        private void SpawnLoot()
        {
            EnemyDeath.Happened -= SpawnLoot;

            LootPiece lootPiece = _gameFactory.CreateLoot();
            lootPiece.transform.position = transform.position;
            lootPiece.GetComponent<UniqueId>().GenerateId();

            Loot loot = GenerateLoot();
      
            lootPiece.Initialize(loot);
        }

        private Loot GenerateLoot()
        {
            return new Loot()
            {
                Value = _randomService.Next(_minValue, _maxValue)
            };
        }
    }
}