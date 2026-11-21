using System;
using UnityEngine;

[Serializable]
public class ScoreSettings
{
    public float maxAngleScore = 100f;
    public float maxSpeedScore = 100f;
}

public struct LandingResult
{
    public bool success;
    public int angleScore;
    public int speedScore;
    public int multiplier;
    public int total;
}

public static class ScoreCalculator
{
    public static LandingResult Calculate(
        LandingData landing,
        LanderSettings landerSettings,
        ScoreSettings scoreSettings
    )
    {
        float minDot = landerSettings.accesiblaLandingDegree;
        float maxVelocity = landerSettings.accesiblaLandingVelocity;

        bool success = landing.velocity < maxVelocity && landing.angleDot >= minDot;

        if (!success)
        {
            return new LandingResult { success = false };
        }

        int angleScore = Mathf.RoundToInt(
            Mathf.InverseLerp(minDot, 1f, landing.angleDot) * scoreSettings.maxAngleScore
        );
        int speedScore = Mathf.RoundToInt(
            (1f - landing.velocity / maxVelocity) * scoreSettings.maxSpeedScore
        );
        int multiplier = landing.pad.GetLandingScore();

        return new LandingResult
        {
            success = true,
            angleScore = angleScore,
            speedScore = speedScore,
            multiplier = multiplier,
            total = (angleScore + speedScore) * multiplier,
        };
    }
}
