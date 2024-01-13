using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPool.Interface;
using System;

namespace Gameplay.Monsters
{
    public class Monster : MonoBehaviour, IMonster
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private float velocity;
        private float timeToChangeVelocity;

        public int ObjectID => "Monster".GetHashCode();
        public Transform Transform => transform;
        public Action<IMonster> OnFinished { get; set; }
        Camera camera;
        private bool canMove;

        public void StartRound()
        {
            camera = Camera.main;
            ChangeVelocity();
            canMove = true;
        }

        void Update()
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
            velocity = UnityEngine.Random.Range(3f, 6f);
        }

        void ObjectLeftScreen()
        {
            canMove = false;
            OnFinished?.Invoke(this);
        }

    }

}