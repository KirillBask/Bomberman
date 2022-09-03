using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType 
    {
        BlastRadius,
        ExtraBomb,
        SpeedIncrease,
    }

    public ItemType type;

    private void OnItemPickup(GameObject player)
    {
       switch (type)
       {
        case ItemType.BlastRadius:
            player.GetComponent<BombController>().ExplosionRadius++;
            FindObjectOfType<AudioManager>().PlaySound("BlastRadius");
            break;

        case ItemType.ExtraBomb:
            player.GetComponent<BombController>().AddBomb();
            FindObjectOfType<AudioManager>().PlaySound("ExtraBomb");
            break;

        case ItemType.SpeedIncrease:
            player.GetComponent<MovementController>().Speed++;
            FindObjectOfType<AudioManager>().PlaySound("SpeedIncrease");
            break;
       }

       Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            OnItemPickup(other.gameObject);
        }
    }
}
