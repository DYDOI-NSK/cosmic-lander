using System.Collections.Generic;
using UnityEngine;

public class LandingPadSpawnOptions : SpawnOptions
{
    public Vector3 scale;
    public int scoreMultiplier;
}

public class LandingPadLoader : BaseEntityLoader<LandingPad, LandingPadSpawnOptions>
{
    protected override IEnumerable<LandingPadSpawnOptions> GetSpawnOptions()
    {
        yield return new LandingPadSpawnOptions
        {
            position = new Vector3(27, 4.2f, 0),
            scale = new Vector3(0.6f, 0.6f, 0.6f),
            scoreMultiplier = 2,
        };
        yield return new LandingPadSpawnOptions
        {
            position = new Vector3(56, 10.75f, 0),
            scale = new Vector3(0.3f, 0.6f, 0.6f),
            scoreMultiplier = 5,
        };
    }

    protected override void Setup(LandingPad pad, LandingPadSpawnOptions options)
    {
        pad.Initialize(options.scale, options.scoreMultiplier);
    }
}
