using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

class ResultsMenu : MonoBehaviour
{
    [SerializeField] TMP_Text results;

    public void activateAndUpdateVlaues(PracticeStats stats)
    {
        results.text = "Average Overshoot: " + stats.overShoot +
            "\nAverage Shot Delay: " + stats.shotDelay +
            "\nAverage Miss Fires: " + stats.missFires +
            "\nAverage Time to Kill Target: " + stats.timeToKillTarget;

        gameObject.SetActive(true);
    }
}

