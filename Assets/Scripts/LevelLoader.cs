using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private LandingPadLoader landingPadLoader;

    private void Start()
    {
        landingPadLoader.Init();
    }
}
