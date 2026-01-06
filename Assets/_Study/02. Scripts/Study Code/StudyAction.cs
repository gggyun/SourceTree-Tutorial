using System;
using UnityEngine;

public class StudyAction : MonoBehaviour
{
    void Start()
    {
        StudyScore.TriggerScore(); // 몬스터 잡았을 때

        StudyScore.onScore(10, true); // 점수 10 증가
        StudyScore.onScore(1, false); // 점수 1 감소

    }
}
