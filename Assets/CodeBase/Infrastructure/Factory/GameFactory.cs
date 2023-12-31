﻿using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressesReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressesWriters { get; } = new List<ISavedProgress>();

        private GameObject HeroGameObject { get; set; }

        private readonly IAssets _assets;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly IWindowService _windowService;

        public GameFactory(IAssets assets, IPersistentProgressService progressService,
            IStaticDataService staticData, IRandomService randomService, IWindowService windowService)
        {
            _assets = assets;
            _staticData = staticData;
            _randomService = randomService;
            _progressService = progressService;
            _windowService = windowService;
        }

        public GameObject CreateHero(GameObject at)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.Hero, at.transform.position);
            return HeroGameObject;
        }

        public GameObject CreateHud()
        {
            GameObject hud = InstantiateRegistered(AssetPath.Hud);

            hud.GetComponentInChildren<LootCounter>()
                .Construct(_progressService.Progress.WorldData);

            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(_windowService);

            return hud;
        }

        public GameObject CreateMonster(MonsterTypeId typeId, SpawnerPoint spawnerPoint)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(typeId);
            GameObject monster = Object.Instantiate(monsterData.Prefab, spawnerPoint.transform.position,
                Quaternion.identity, spawnerPoint.transform);

            IHealth health = monster.GetComponent<IHealth>();
            health.Current = monsterData.Health;
            health.Max = monsterData.Health;

            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToHero>().Construct(HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            EnemyDeath death = monster.GetComponent<EnemyDeath>();

            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this, _randomService);
            lootSpawner.SetLootValue(monsterData.MinLootValue, monsterData.MaxLootValue);

            Attack attack = monster.GetComponent<Attack>();
            attack.Construct(HeroGameObject.transform);
            attack.Damage = monsterData.Damage;
            attack.Cleavage = monsterData.Cleavage;
            attack.EffectiveDistance = monsterData.EffectiveDistance;

            monster.GetComponent<RotateToHero>()?.Construct(HeroGameObject.transform);

            return monster;
        }

        public LootPiece CreateLoot()
        {
            LootPiece lootPiece = InstantiateRegistered(AssetPath.Loot).GetComponent<LootPiece>();

            lootPiece.Construct(_progressService.Progress.WorldData);

            return lootPiece;
        }

        public void CreateSpawner(Vector3 at, string spawnerId, MonsterTypeId monsterTypeId)
        {
            SpawnerPoint spawnerPoint = InstantiateRegistered(AssetPath.Spawner, at)
                .GetComponent<SpawnerPoint>();

            spawnerPoint.Construct(this);
            spawnerPoint.Id = spawnerId;
            spawnerPoint.MonsterTypeId = monsterTypeId;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriters)
                ProgressesWriters.Add(progressWriters);

            ProgressesReaders.Add(progressReader);
        }

        public void Cleanup()
        {
            ProgressesReaders.Clear();
            ProgressesWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }
}