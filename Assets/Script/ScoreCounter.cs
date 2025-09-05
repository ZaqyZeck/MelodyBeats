using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Text successCounter;
    [SerializeField] private Text perfectCounter;
    [SerializeField] private Text failedCounter;
    [SerializeField] private TMP_Text[] scoreCounter;
    [SerializeField] private TMP_Text timeCounter;

    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private GameObject textname, textscore, texttime;

    [SerializeField] private ScoreController scoreController;

    public int success, failed, perfect;

    public int combo;
    [SerializeField] private TMP_Text textCombo;

    public void AddSuccess()
    {
        success++;
        successCounter.text = $"sukses = {success}";
        scoreController.currentScore += 100;
        UpdateScoreCounter();
    }
    public void AddPerfect()
    {
        perfect++;
        perfectCounter.text = $"perfect = {perfect}";
        scoreController.currentScore += 200;
        UpdateScoreCounter();
    }

    public void AddFailed()
    {
        failed++;
        failedCounter.text = $"gagal = {failed}";
        if(scoreController.currentScore <= 50) scoreController.currentScore = 0 ;
        else scoreController.currentScore -= 50;
        UpdateScoreCounter();
    }

    public void AddCombo(bool success)
    {
        if (success)
        {

            combo++;
            if (combo > 5) scoreController.currentScore += 100;

        }
        else combo = 0;
        if (textCombo == null) return;
        textCombo.text = $"Combo = {combo}";
    }

    public void UpdateScoreCounter()
    {
        foreach (var text in scoreCounter)
        {
            if (text != null)
                text.text = $"Score = {scoreController.currentScore}";
        }
    }

    public void SetScore()
    {
        string name = playerNameInput.text;
        scoreController.playerName = name;
        scoreController.LoadSaveScores();
        scoreController.AddScore();
        scoreController.SaveScores();
    }

    public void SetScoreTime()
    {
        scoreController.finishTime = DateTime.Now;
        timeCounter.text = $"Time = {scoreController.finishTime:HH:mm:ss}";
    }

    public void ShowAllScores()
    {
        List<Score> scores = scoreController.scores;

        for (int i = 0; i < scores.Count && i < 5; i++)
        {
            Score s = scores[i];

            // Buat UI untuk nama
            GameObject nameObj = Instantiate(textname);
            nameObj.GetComponent<TMP_Text>().text = s.name;
            nameObj.transform.SetParent(textname.transform.parent);

            // Buat UI untuk score
            GameObject scoreObj = Instantiate(textscore);
            scoreObj.GetComponent<TMP_Text>().text = s.score.ToString();
            scoreObj.transform.SetParent(textscore.transform.parent);

            // Buat UI untuk waktu
            GameObject timeObj = Instantiate(texttime);
            timeObj.GetComponent<TMP_Text>().text = s.scoreDate.ToString("dd/MM/yyyy HH:mm");
            timeObj.transform.SetParent(texttime.transform.parent);
        }
    }
}
