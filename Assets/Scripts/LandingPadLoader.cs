using UnityEngine;

public class LandingPadLoader : MonoBehaviour
{
    [SerializeField]
    private LandingPad landingPadPrefab;

    public void Init()
    {
        SpawnLandingPad(new Vector3(27, 4.2f, 0), new Vector3(0.6f, 0.6f, 0.6f), 2);
        SpawnLandingPad(new Vector3(56, 10.75f, 0), new Vector3(0.3f, 0.6f, 0.6f), 5);
    }

    private void SpawnLandingPad(Vector3 position, Vector3 scale, int scoreMultiplier)
    {
        LandingPad pad = Instantiate(landingPadPrefab, position, Quaternion.identity);
        pad.Initialize(scale, scoreMultiplier);
    }
}
