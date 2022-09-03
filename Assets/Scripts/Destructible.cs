
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float DestructionTime = 1f;

    [Range(0f, 1f)]
    public float ItemSpawnChance = 0.2f;
    public GameObject[] SpawnableItems;

    private void Start()
    {
        Destroy(gameObject, DestructionTime);
    }

    private void OnDestroy()
    {
        if(SpawnableItems.Length > 0 && Random.value < ItemSpawnChance)
        {
            int randomIndex = Random.Range(0, SpawnableItems.Length);
            Instantiate(SpawnableItems[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
