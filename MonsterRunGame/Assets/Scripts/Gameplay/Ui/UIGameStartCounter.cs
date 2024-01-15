using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    /// <summary>
    /// Manages a countdown counter displayed during the game start.
    /// </summary>
    public class GameStartCounter : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI counterText;

        /// <summary>
        /// Initiates the countdown counter with the specified counter time.
        /// </summary>
        public async Task StartCounter(int counterTime)
        {
            gameObject.SetActive(true);
            await CounterText(counterTime);
            gameObject.SetActive(false);

            /// <summary>
            /// Displays the countdown on the counter text recursively until reaching zero.
            /// </summary>
            async Task CounterText(int counterTime)
            {
                if (counterTime > 0)
                {
                    counterText.text = counterTime.ToString();
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    await CounterText(counterTime - 1);
                }
            }
        }
    }

}