using ScriptableVariable.Unite2017.Variables;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header("Variable")]
    [SerializeField] FloatVariable score;
    [SerializeField] FloatVariable timer;
    [SerializeField] FloatVariable argent;

    [Header("mise en forme")]
    [SerializeField] string preffix;
    [SerializeField] string suffix;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI scoreDisplay;

    private void OnEnable()
    {
        ThisIsTheEnd();
    }


    public void ThisIsTheEnd()
    {
        Time.timeScale = 0;
        float finalScore = 0;
        finalScore += (timer.Value / 2);
        finalScore += score.Value;
        finalScore += (argent.Value * 1.5f);
        finalScore = Mathf.Round(finalScore);

        scoreDisplay.text = preffix + finalScore + suffix;
    }
}
