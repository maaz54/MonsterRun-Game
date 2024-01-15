using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Extensions;

namespace Gameplay.UI
{
    /// <summary>
    /// Manages a timer displayed in the UI to track elapsed time.
    /// </summary>
    public class UiTimer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI timeText;
        private float timeElapsed;
        private bool isPlaying;

        /// <summary>
        /// Starts the timer and updates the elapsed time every second.
        /// </summary>
        public async Task StartTimer()
        {
            isPlaying = true;
            timeElapsed = 0;
            timeText.text = timeElapsed.DisplayTime();

            while (isPlaying)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                timeElapsed++;
                timeText.text = timeElapsed.DisplayTime();
            }
        }

        /// <summary>
        /// Stops the timer and retrieves the total elapsed time.
        /// </summary>
        public void StopTimer(out float timeElapsed)
        {
            isPlaying = false;
            timeElapsed = this.timeElapsed;
        }
    }

}