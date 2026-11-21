using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CoinSpawnOptions : SpawnOptions
{
    public float value;
}

public class CoinLoader : BaseEntityLoader<Coin, CoinSpawnOptions>
{
    [SerializeField]
    private List<CoinSpawnOptions> spawnOptions = new()
    {
        new CoinSpawnOptions { position = new Vector3(27, 12, 0), value = 200 },
        new CoinSpawnOptions { position = new Vector3(56, 18, 0), value = 500 },
    };

    protected override IEnumerable<CoinSpawnOptions> GetSpawnOptions() => spawnOptions;

    protected override void Setup(Coin entity, CoinSpawnOptions options)
    {
        entity.Init(options.value);
    }
}
