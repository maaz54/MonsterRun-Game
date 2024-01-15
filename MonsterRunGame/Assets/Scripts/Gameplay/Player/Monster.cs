using UnityEngine;
using System;
using System.Threading.Tasks;
using Gameplay.Interface;

namespace Gameplay.Monsters
{
    public class Monster : MonoBehaviour, IMonster
    {
        /// <summary>
        /// Represents a monster in the game with movement and destruction behavior.
        /// </summary>
        public int ObjectID => "Monster".GetHashCode();

        /// <summary>
        /// Reference to the monster's Transfoorm
        /// </summary>
        public Transform Transform => transform;

        /// <summary>
        /// invoked when the monster has finished its movement.
        /// </summary>
        public Action<IMonster> OnFinished { get; set; }

        /// <summary>
        /// The name of the monster.
        /// </summary>
        public string MonsterName { get; set; }

        [SerializeField] private SpriteRenderer spriteRenderer;

        private float velocity;

        private float timeToChangeVelocity;

        private bool canMove = false;

        /// <summary>
        /// Initializes the monster with the provided action and name.
        /// </summary>
        public void Initialize(ref Action CanMove, string MonsterName)
        {
            canMove = false;
            this.MonsterName = MonsterName;
            CanMove += StartRound;
        }

        /// <summary>
        /// Starts the monster's movement when the round begins.
        /// </summary>
        private void StartRound()
        {
            ChangeVelocity();
            canMove = true;
        }

        /// <summary>
        /// Updates the monster's position and checks for visibility and destruction conditions.
        /// </summary>
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

        /// <summary>
        /// Changes the monster's velocity and resets the time interval.
        /// </summary>
        private void ChangeVelocity()
        {
            timeToChangeVelocity = UnityEngine.Random.Range(.1f, .8f);
            velocity = UnityEngine.Random.Range(.5f, 2f);
        }

        /// <summary>
        /// Invoked when the monster leaves the screen, triggers destruction and the OnFinished event.
        /// </summary>
        void ObjectLeftScreen()
        {
            _ = Destory(1);
            OnFinished?.Invoke(this);
        }

        /// <summary>
        /// Destroys the monster after a specified delay.
        /// </summary>
        private async Task Destory(float Delay)
        {
            await Task.Delay(TimeSpan.FromSeconds(Delay));

            // we can add more visuals for destroy monster
            canMove = false;
            gameObject.SetActive(false);

        }
    }
}