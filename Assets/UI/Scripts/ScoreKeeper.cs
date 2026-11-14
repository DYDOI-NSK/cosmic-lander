using System;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public int Score { get; private set; }
    public event Action<int> OnScoreChanged;

    public void Add(int amount)
    {
        Score += amount;
        OnScoreChanged?.Invoke(Score);
    }

    public void ResetScore()
    {
        Score = 0;
        OnScoreChanged?.Invoke(Score);
    }

    [ContextMenu("Add 100 (test)")]
    private void AddTest() => Add(100);
}
