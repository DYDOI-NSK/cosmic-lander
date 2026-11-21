using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FuelSpawnOptions : SpawnOptions
{
    public float fuelAmount;
}

public class FuelLoader : BaseEntityLoader<Fuel, FuelSpawnOptions>
{
    [SerializeField]
    private List<FuelSpawnOptions> spawnOptions = new()
    {
        new FuelSpawnOptions { position = new Vector3(15, 6, 0), fuelAmount = 10f },
    };

    protected override IEnumerable<FuelSpawnOptions> GetSpawnOptions() => spawnOptions;

    protected override void Setup(Fuel entity, FuelSpawnOptions options)
    {
        entity.Init(options.fuelAmount);
    }
}
