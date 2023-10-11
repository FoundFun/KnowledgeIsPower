﻿using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressesReaders { get; }
        List<ISavedProgress> ProgressesWriters { get; }
        
        void Register(ISavedProgressReader progressReader);

        GameObject CreateMonster(MonsterTypeId typeId, EnemySpawner parent);
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
        LootPiece CreateLoot();

        void Cleanup();
    }
}