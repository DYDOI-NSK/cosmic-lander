using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnOptions : SpawnOptions
{
    public float value;
}

public class CoinLoader : BaseEntityLoader<Coin, CoinSpawnOptions>
{
    protected override void Setup(Coin entity, CoinSpawnOptions options)
    {
        entity.Init(options.value);
    }

    protected override IEnumerable<CoinSpawnOptions> GetSpawnOptions()
    {
        yield return new CoinSpawnOptions { position = new Vector3(27, 12, 0), value = 200 };
        yield return new CoinSpawnOptions { position = new Vector3(56, 18, 0), value = 500 };
    }
}
