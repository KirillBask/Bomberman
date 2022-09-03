using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimatedSpriteRenderer Start;
    public AnimatedSpriteRenderer Middle;
    public AnimatedSpriteRenderer End;

    public void SetActiveRenderer(AnimatedSpriteRenderer renderer)
    {
        Start.enabled = renderer == Start;
        Middle.enabled = renderer == Middle;
        End.enabled = renderer == End;
    }

    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);      
    }

    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
