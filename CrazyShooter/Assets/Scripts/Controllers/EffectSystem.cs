using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CrazyShooter.Enums;
using CrazyShooter.Signals;
using UnityEngine;
using Zenject;

namespace CrazyShooter.EffectSystem
{
    public class EffectSystem : MonoBehaviour
    {
        [SerializeField] private List<EnemyDieEffectColor> enemiesEffectColor;
        [SerializeField] private ParticleSystem dieEffect;
        [Inject] private DiContainer _diContainer;
        [Inject] private SignalBus _signalBus;

        private void Awake()
        {
            _signalBus.Subscribe<EnemyDieEffectSignal>(PlayDieEnemyEffect);
        }

        private void PlayDieEnemyEffect(EnemyDieEffectSignal signal)
        {
            var effect = Instantiate(dieEffect, signal.EnemyTransform.position, Quaternion.identity);
            var color = enemiesEffectColor.First(x => x.EnemyType == signal.EnemyType).Color;
            effect.startColor = color;
            effect.Play();
        }
    }

    [Serializable]
    public class EnemyDieEffectColor
    {
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private Color color;

        public EnemyType EnemyType => enemyType;
        public Color Color => color;
    }
    
}
