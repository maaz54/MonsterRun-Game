using System;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Extensions;

namespace Gameplay.UI
{
    /// <summary>
    /// Manages the UI elements for displaying round results, including rankings and elapsed time.
    /// </summary>
    public class UiResultScreen : MonoBehaviour
    {
        [SerializeField] GameObject roundCompleteTextPanel;
        [SerializeField] GameObject rankingPanel;
        [SerializeField] TextMeshProUGUI playersRankingText;
        [SerializeField] TextMeshProUGUI totalTimeText;

        /// <summary>
        /// Shows the round result including rankings and elapsed time.
        /// </summary>
        public async Task ShowResult(string[] rankingName, float totalTime)
        {
            rankingPanel.SetActive(false);
            roundCompleteTextPanel.gameObject.SetActive(true);
            totalTimeText.text = "Elapsed time: " + totalTime.DisplayTime();
            await Task.Delay(TimeSpan.FromSeconds(3));

            roundCompleteTextPanel.gameObject.SetActive(false);
            string rankingText = "";
            int rank = 1;
            rankingName.ToList().ForEach(r =>
            {
                rankingText += rank.ToString() + ": " + r + "\n";
                rank++;
            });
            playersRankingText.text = rankingText;
            rankingPanel.SetActive(true);
        }
    }
}