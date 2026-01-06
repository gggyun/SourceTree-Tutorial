using System;
using UnityEngine;

public class StudyScore : MonoBehaviour
{
    public static Action onScoreUp;
    public static Action onScoreDown;

    public static Action<int, bool> onScore;

    private int score;

    void Start()
    {
        onScoreUp += ScoreUp;
        onScoreDown += ScoreDown;

        onScore += ScoreUpDown;

    }
    private void ScoreUpDown(int score, bool isUp)
    {
        if (isUp) this.score += score;
        else this.score -= score;
    }

    private void ScoreUp()
    {
        // 점수 증가
        score++;
    }

    private void ScoreDown()
    {
        // 점수 감소
        score--;
    }


    public static void TriggerScore()
    {
        onScoreUp?.Invoke();
    }
}
