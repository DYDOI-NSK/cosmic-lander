using System.Collections.Generic;
using UnityEngine;

public class FuelSpawnOptions : SpawnOptions { }

public class FuelLoader : BaseEntityLoader<Fuel, FuelSpawnOptions>
{
    protected override IEnumerable<FuelSpawnOptions> GetSpawnOptions()
    {
        yield return new FuelSpawnOptions { position = new Vector3(15, 6, 0) };
    }
}
