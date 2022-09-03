using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public KeyCode InputKey = KeyCode.Space;
    public GameObject BombPrefab;
    public float BombFuseTime = 3f;
    public int BombAmount = 1;
    private int BombsRemaining;

    [Header("Explosion")]
    public Explosion ExplosionPrefab;
    public LayerMask ExplosionLayerMask;
    public float ExplosionDuration = 1f;
    public int ExplosionRadius = 1;

    [Header("Destructible")]
    public Tilemap DestructibleTiles;
    public Destructible DestructiblePrefabs;

    private void OnEnable()
    {
        BombsRemaining = BombAmount;
    }

    private void Update()
    {
        if(BombsRemaining > 0 && Input.GetKeyDown(InputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(BombPrefab, position, Quaternion.identity);
        BombsRemaining--;

        yield return new WaitForSeconds(BombFuseTime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Explosion explosion = Instantiate(ExplosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.Start);
        explosion.DestroyAfter(ExplosionDuration);

        Explode(position, Vector2.up, ExplosionRadius);
        Explode(position, Vector2.down, ExplosionRadius);
        Explode(position, Vector2.left, ExplosionRadius);
        Explode(position, Vector2.right, ExplosionRadius);

        Destroy(bomb);
        BombsRemaining++;
    }  

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if(length <= 0)
        {
            return;
        }

        position += direction;

        if(Physics2D.OverlapBox(position, Vector2.one /2f, 0f, ExplosionLayerMask))
        {
            ClearDestructible(position);
            return;
        }

        Explosion explosion = Instantiate(ExplosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.Middle : explosion.End);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(ExplosionDuration);

        Explode(position, direction, length - 1);
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = DestructibleTiles.WorldToCell(position);
        TileBase tile = DestructibleTiles.GetTile(cell);

        if(tile != null)
        {
            Instantiate(DestructiblePrefabs, position, Quaternion.identity);
            DestructibleTiles.SetTile(cell, null);
        }
    }

    public void AddBomb()
    {
        BombAmount++;
        BombsRemaining++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }
}
