using UnityEngine;

public class LandingPad : MonoBehaviour
{
    private int scoreMultiplier;

    public void Initialize(int multiplier)
    {
        Debug.Log(multiplier);
        scoreMultiplier = multiplier;
        GetComponent<LandingPadScoreText>().UpdateText();
    }

    public int GetLandingScore()
    {
        return scoreMultiplier;
    }
}
