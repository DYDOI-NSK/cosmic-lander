using UnityEngine;

public class LandingPad : MonoBehaviour
{
    private int scoreMultiplier;

    public void Initialize(Vector3 scale, int multiplier)
    {
        Debug.Log(multiplier);
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        scoreMultiplier = multiplier;
        spriteRenderer.transform.localScale = scale;
        GetComponent<LandingPadScoreText>().UpdateText();

        Vector3 spriteSize = spriteRenderer.sprite.bounds.size;

        collider.size = new Vector2(spriteSize.x * scale.x, spriteSize.y * scale.y - 0.1f);
    }

    public int GetLandingScore()
    {
        return scoreMultiplier;
    }
}
