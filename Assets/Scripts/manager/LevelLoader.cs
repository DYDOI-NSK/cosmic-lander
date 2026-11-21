using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private Lander lander;

    [SerializeField]
    private LandingPadLoader landingPadLoader;

    [SerializeField]
    private FuelLoader fuelLoader;

    [SerializeField]
    private CoinLoader coinLoader;

    public Lander Lander => lander;

    public void Load()
    {
        landingPadLoader.Init();
        fuelLoader.Init();
        coinLoader.Init();
    }
}
