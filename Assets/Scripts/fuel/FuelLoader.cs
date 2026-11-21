using System.Collections.Generic;
using UnityEngine;

public class FuelSpawnOptions : SpawnOptions
{
    public float fuelAmount;
}

public class FuelLoader : BaseEntityLoader<Fuel, FuelSpawnOptions>
{
    protected override void Setup(Fuel entity, FuelSpawnOptions options)
    {
        entity.Init(options.fuelAmount);
    }

    protected override IEnumerable<FuelSpawnOptions> GetSpawnOptions()
    {
        yield return new FuelSpawnOptions { position = new Vector3(15, 6, 0), fuelAmount = 10f };
    }
}
