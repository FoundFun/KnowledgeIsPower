using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressesReaders { get; }
        List<ISavedProgress> ProgressesWriters { get; }
        
        GameObject CreateHero(GameObject at);
        void CreateHud();
        void Cleanup();
    }
}