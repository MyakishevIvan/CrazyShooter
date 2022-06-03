using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Enums;
using UnityEngine;
using Zenject.SpaceFighter;

namespace CrazyShooter.Signals
{
    public class LoadedSceneInitializedSignal
    {
    }

    public class EnemyDieEffectSignal
    {
        public Transform EnemyTransform { get; }
        public EnemyType EnemyType { get; }

        public EnemyDieEffectSignal(Transform enemyTransform, EnemyType enemyType)
        {
            EnemyTransform = enemyTransform;
            EnemyType = enemyType;
        }
    }

    public class PlayerDiedSignal
    {
    }
}