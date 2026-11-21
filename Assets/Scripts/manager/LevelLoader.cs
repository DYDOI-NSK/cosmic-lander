using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private LandingPadLoader landingPadLoader;

    [SerializeField]
    private FuelLoader fuelLoader;

    [SerializeField]
    private CoinLoader coinLoader;

    private void Start()
    {
        landingPadLoader.Init();
        fuelLoader.Init();
        coinLoader.Init();
    }
}
