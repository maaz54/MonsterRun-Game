using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class GameStartCounter : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI counterText;

        public async Task StartCounter(int counterTime)
        {
            gameObject.SetActive(true);
            await CounterText(counterTime);
            gameObject.SetActive(false);

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