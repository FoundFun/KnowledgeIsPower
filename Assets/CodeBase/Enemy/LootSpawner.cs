using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyDeath EnemyDeath;

        private IGameFactory _gameFactory;

        public void Construct(IGameFactory gameFactory) => 
            _gameFactory = gameFactory;

        private void Start()
        {
            EnemyDeath.Happened += SpawnLoot;
        }

        private void SpawnLoot()
        {
            GameObject loot = _gameFactory.CreateLoot();
            loot.transform.position = EnemyDeath.transform.position;
        }
    }
}