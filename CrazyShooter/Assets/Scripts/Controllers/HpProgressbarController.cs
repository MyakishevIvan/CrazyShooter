using System;
using System.Collections;
using System.Collections.Generic;
using CrazyShooter.Enums;
using UnityEngine;

namespace CrazyShooter.Progressbar
{
    public class HpProgressbarController : MonoBehaviour
    {
        [SerializeField] private Color enemyProgressbarColor;
        [SerializeField] private Color playerProgressbarColor;
        [SerializeField] private SpriteRenderer filler;
        [SerializeField] private SpriteRenderer damageView;
        [SerializeField] private float speed;
        private Transform _transform;
        private Vector2 _barSize;

        private void Start()
        {
            _barSize = filler.size;
            transform.localScale = _transform.lossyScale;
            
            if (_transform.lossyScale.x > .5f)
                transform.localScale = new Vector3(.5f, .5f, .5f);
            else
                transform.localScale = _transform.lossyScale;
         
        }

        private void Update()
        {
            transform.position = _transform.position;
        }

        public void Init(CharacterType characterType, Transform transform)
        {
            _transform = transform;
            var sortingLayerName = "";
            if (characterType == CharacterType.Enemy)
            {
                
                sortingLayerName= "EnemyHp";
                filler.color = enemyProgressbarColor;
            }
            else
            {
                sortingLayerName= "PlayerHp";

                filler.color = playerProgressbarColor;
            }

            var bar = GetComponent<SpriteRenderer>();
            bar.sortingLayerName = sortingLayerName;
            bar.sortingOrder = 0;
            damageView.sortingLayerName = sortingLayerName;
            damageView.sortingOrder = 1;
            filler.sortingLayerName = sortingLayerName;
            filler.sortingOrder = 2;
        }

        public void SetDamage(float value)
        {
            
           var size = new Vector2(_barSize.x, _barSize.y * value);
            filler.size = size;
            StopAllCoroutines();
            StartCoroutine(PlayDamageViewAnimation(size.y));
        }

        private IEnumerator PlayDamageViewAnimation(float newSize)
        {
            while (true)
            {
                var viewValue = Mathf.Lerp(damageView.size.y,newSize, Time.deltaTime * speed);
                damageView.size = new Vector2(damageView.lightmapIndex, viewValue);
                
                if(damageView.size.y == newSize)
                    StopAllCoroutines();
                    
                yield return null;
            }

        }
    }
}

