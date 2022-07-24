using UnityEngine;

namespace Bomberman
{
    public class Destructible : MonoBehaviour
    {

        [Header ("Settings")]

        
        public float destructionTime = 1f;

        [Range(0f, 1f)] ///Creates a controllable Slider on Inspector ///Note** this should be on top of float or int you want to control through slider
        public float itemspawnChance = 0.2f;

        public GameObject[] spawnableObjects;


        private void Start()
        {
            Destroy(gameObject);
            //Destroy(gameObject,1.25f);
        }

        private void OnDestroy()
        {
            if(spawnableObjects.Length > 0 && Random.value < itemspawnChance) /// Generates a Random value via Random.value and if it is less than the itemspawnchance it generates the item
            {
                int randomIndex = Random.Range(0,spawnableObjects.Length); /// picks a random value and translates that to the number the object it is assigned
                Instantiate(spawnableObjects[randomIndex],transform.position,Quaternion.identity);
            }
            
        }


   
    }

}

