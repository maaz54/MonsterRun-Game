using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class UiTimer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI timeText;
        private float timeElapsed;
        private bool isPlaying;
        public async Task StartTimer()
        {
            isPlaying = true;
            timeElapsed = 0;
            timeText.text = DisplayTime(timeElapsed);

            while (isPlaying)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                timeElapsed++;
                timeText.text = DisplayTime(timeElapsed);
            }

        }

        public void StopTimer(out float timeElapsed)
        {
            isPlaying = false;
            timeElapsed = this.timeElapsed;
        }

        /// <summary>
        /// Formats the time in minutes and seconds for display
        /// </summary>
        string DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

}