using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public Sprite IdleSprite;
    public Sprite[] AnimationSprites;

    public float AnimationTime = 0.25f;
    private int _animationFrame;
    
    public bool Loop = true;
    public bool Idle = true;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        _spriteRenderer.enabled = false;
    }

    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), AnimationTime, AnimationTime);
    }

    private void NextFrame()
    {
        _animationFrame++;

        if(Loop && _animationFrame >= AnimationSprites.Length)
        {
            _animationFrame = 0;
        }
        
        if(Idle)
        {
            _spriteRenderer.sprite = IdleSprite;
        }
        else if(_animationFrame >= 0 && _animationFrame < AnimationSprites.Length)
        {
            _spriteRenderer.sprite = AnimationSprites[_animationFrame];
        }
    }
}