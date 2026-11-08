using TMPro;
using UnityEngine;

public class LandingPadScoreText : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro mesh;

    public void UpdateText()
    {
        mesh.text = "x" + GetComponent<LandingPad>().GetLandingScore();
    }
}
