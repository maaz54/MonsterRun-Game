using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Extensions;

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
            timeText.text = timeElapsed.DisplayTime();

            while (isPlaying)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                timeElapsed++;
                timeText.text = timeElapsed.DisplayTime();
            }

        }

        public void StopTimer(out float timeElapsed)
        {
            isPlaying = false;
            timeElapsed = this.timeElapsed;
        }

    }

}