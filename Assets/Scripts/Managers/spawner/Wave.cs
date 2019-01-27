using System;
using UnityEngine;

namespace Managers.spawner
{
    [Serializable]
    public class Wave
    {
        public string name;
        public AbstractBug enemy;
        public int enemyCount;
        public float spawnRate;
    }
}