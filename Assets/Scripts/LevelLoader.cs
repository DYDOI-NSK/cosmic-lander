using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private LandingPadLoader landingPadLoader;

    [SerializeField]
    private FuelLoader fuelLoader;

    private void Start()
    {
        landingPadLoader.Init();
        fuelLoader.Init();
    }
}
