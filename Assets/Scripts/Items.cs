using UnityEngine;

namespace Bomberman {


public class Items : MonoBehaviour
{

    public enum ItemType
    {
        BlastPower,
        ExtraBomb,
        SpeedBoost,
    }



    public ItemType type;
   


    private void OnItemPickUp(GameObject player)
    {

        
        switch(type)
        {
            case ItemType.BlastPower:
            player.GetComponent<BombController>().explosionRadius ++;
            
            break;

            case ItemType.ExtraBomb:
            player.GetComponent<BombController>().AddBomb();

            break;

            case ItemType.SpeedBoost:
            player.GetComponent<MovementController>().movementSpeed ++;

            break;
        }

        Destroy(gameObject);


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            
            OnItemPickUp(other.gameObject);
            
        }

        

    }
    
    
    
}

}