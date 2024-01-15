using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool.Interface;
using System;
using TMPro;
using System.Threading.Tasks;
using Gameplay.Interface;

namespace Gameplay.Monsters
{
    public class Monster : MonoBehaviour, IMonster
    {
        [SerializeField] private TextMeshPro nameText;
        [SerializeField] private SpriteRenderer spriteRenderer;
        private float velocity;
        private float timeToChangeVelocity;

        public int ObjectID => "Monster".GetHashCode();
        public Transform Transform => transform;
        public Action<IMonster> OnFinished { get; set; }
        public string MonsterName { get; set; }
        private bool canMove = false;

        public void Initialize(ref Action CanMove, string MonsterName)
        {
            canMove = false;
            this.MonsterName = MonsterName;
            nameText.text = MonsterName;
            CanMove += StartRound;
        }

        private void StartRound()
        {
            ChangeVelocity();
            canMove = true;
        }

        public void UpdateCall()
        {
            if (canMove)
            {
                transform.Translate(Vector2.right * velocity * Time.deltaTime);
                timeToChangeVelocity -= Time.deltaTime;
                if (timeToChangeVelocity <= 0)
                {
                    ChangeVelocity();
                }

                if (!spriteRenderer.isVisible)
                {
                    ObjectLeftScreen();
                }
            }
        }

        private void ChangeVelocity()
        {
            timeToChangeVelocity = UnityEngine.Random.Range(.1f, .8f);
            velocity = UnityEngine.Random.Range(.5f, 2f);
        }

        void ObjectLeftScreen()
        {
            _ = Destory(1);
            OnFinished?.Invoke(this);
        }

        private async Task Destory(float Delay)
        {
            await Task.Delay(TimeSpan.FromSeconds(Delay));

            // we can add more visuals for destroy monster
            canMove = false;
            gameObject.SetActive(false);

        }

    }

}