using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class UiResultScreen : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI playersRankingText;
        [SerializeField] TextMeshProUGUI totalTimeText;

        public void ShowResult(string[] rankingName, string totalTime)
        {
            string rankingText = "";
            totalTimeText.text = "Elapsed time: " + totalTime.ToString();

            int rank = 1;
            rankingName.ToList().ForEach(r =>
            {
                rankingText += rank.ToString() + ": " + r + "\n";
                rank++;
            });

            playersRankingText.text = rankingText;
        }



    }
}