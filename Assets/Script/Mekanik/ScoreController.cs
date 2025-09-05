using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static int highScore;
    public int currentScore;
    public string playerName;
    public char grade = 'A';
    public DateTime finishTime;

    public List<Score> scores = new(); 

    public void AddScore()
    {
        scores.Add(new Score(currentScore, playerName, finishTime, grade));
        scores.Sort((a, b) => b.score.CompareTo(a.score));
    }

    public void SaveScores(int musicNumber)
    {
        if (musicNumber == 1) SaveSystem.SaveScoresData(scores);
        else SaveSystem.SaveScoresDataDua(scores);
    }

    public void DeleteAllScores()
    {
        SaveSystem.DeleteScoresData();
    }

    public void LoadSaveScores(int musicNumber)
    {
        ScoresData scoresData;
        if (musicNumber == 1) scoresData = SaveSystem.LoadScores();
        else scoresData = SaveSystem.LoadScoresDua();
        scores.Clear();

        if (scoresData == null) return;

        for (int i = 0; i < scoresData.score.Count; i++)
        {
            Score newScore = new Score(
                scoresData.score[i],
                scoresData.name[i],
                scoresData.scoreDate[i],
                scoresData.grade[i]
            );
            scores.Add(newScore);
        }

        // sort lagi supaya tetap urut
        scores.Sort((a, b) => b.score.CompareTo(a.score));
    }
}

[System.Serializable]
public class ScoresData
{
    public List<int> score;
    public List<string> name;
    public List<DateTime> scoreDate;
    public List<char> grade;

    public ScoresData()
    {
        score = new List<int>();
        name = new List<string>();
        scoreDate = new List<DateTime>();
        grade = new List<char>();
    }

    public ScoresData(List<Score> Scores)
    {
        score = new List<int>();
        name = new List<string>();
        scoreDate = new List<DateTime>();
        grade = new List<char>();

        foreach (Score s in Scores)
        {
            this.score.Add(s.score);
            this.name.Add(s.name);
            this.scoreDate.Add(s.scoreDate);
            this.grade.Add(s.grade);
        }
    }
}

[Serializable]
public class Score
{
    public int score;
    public string name;
    public DateTime scoreDate;
    public char grade;

    public Score(int score, string name, DateTime scoreDate, char grade)
    {
        this.score = score;
        this.name = name;
        this.scoreDate = scoreDate;
        this.grade = grade;
    }
}
