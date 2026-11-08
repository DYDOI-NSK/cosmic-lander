using UnityEngine;

public class LandingPadLoader : MonoBehaviour
{
    [SerializeField]
    private LandingPad landingPadPrefab;

    public void Init()
    {
        SpawnLandingPad(new Vector3(27, 4.2f, 0), 2);
    }

    private void SpawnLandingPad(Vector3 position, int scoreMultiplier)
    {
        LandingPad pad = Instantiate(landingPadPrefab, position, Quaternion.identity);
        pad.Initialize(scoreMultiplier);
    }
}
