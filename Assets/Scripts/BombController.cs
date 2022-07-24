using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;



namespace Bomberman
{
    public class BombController : MonoBehaviour
    {

        [Header ("Bomb Settings")]
        public GameObject Bombprefab;
        public KeyCode Keyinput = KeyCode.Space;
        public float bombFuseTime = 3f;
        public int bombAmount = 1;
        private int bombsRemaining;

        [Header ("Explosion Settings")]
        public Explosion explosionPrefab;
        public LayerMask explosionLayerMask;
        public float explosionDuration = 1f;
        public int explosionRadius = 1;

        [Header ("Destructibles")]

        public Destructible destructiblePrefab;
        private Tilemap destructibleTiles;

        [Header("Sound Setting")]

        [Header ("Sound Used")]
        public AudioSource bombplaceSound;
        public AudioSource bombExplode;
        public AudioSource itemPickup;

        private void Awake()
        {
            destructibleTiles = FindObjectOfType<Tilemap>();
        }

        private void OnEnable()
        {
            bombsRemaining = bombAmount;
        }

        private void Update()
        {
            if(bombsRemaining > 0 && Input.GetKeyDown(Keyinput))
            {
               bombplaceSound.Play();
               StartCoroutine(BombPlace());
            }
        }

        private IEnumerator BombPlace()
        {
            Vector2 position = transform.position;
            
           // position.x = Mathf.Round(position.x);
           // position.y = Mathf.Round(position.y);

            //Rounds the bombs position to whole number and not place it on Decimal positions
          

            GameObject bomb = Instantiate(Bombprefab,position,Quaternion.identity);

            /// Decreases the amount of bomb to avoid players dropping two bombs at the same time
            bombsRemaining--;


            yield return new WaitForSeconds(bombFuseTime);

            position = bomb.transform.position;
            //position.x = Mathf.Round(position.x);
            //position.y = Mathf.Round(position.y);

            bombExplode.Play();
            Explosion explosion = Instantiate(explosionPrefab, position,Quaternion.identity);
            explosion.SetActiveRenderer(explosion.start);
            explosion.DestroyAfter(explosionDuration);
            Destroy(explosion.gameObject,explosionDuration);

            Explode(position,Vector2.up,explosionRadius);
            Explode(position,Vector2.down,explosionRadius);
            Explode(position,Vector2.left,explosionRadius);
            Explode(position,Vector2.right,explosionRadius);


            Destroy(bomb);
            /// Increases the bomb amount again so it can drop again without doubling the amount of bombs
            bombsRemaining++;
       
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            ///Disables the Ontrigger event on bombs when you walk past throught it
            
            if(other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
            {
                other.isTrigger = false;
            }
        }

        private void Explode(Vector2 position, Vector2 direction,int explosionLenght)
        {
            if(explosionLenght <= 0)
            {
                return;
            }
            position += direction;

            if(Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
            {
                ClearDestructible(position);
                return;
            }
            Explosion explosion = Instantiate(explosionPrefab, position,Quaternion.identity);

                                       ///CONDITION======     CONDITON = TRUE===CONDITION = FALSE;
            explosion.SetActiveRenderer(explosionLenght > 1 ? explosion.middle : explosion.end);
            explosion.SetDirection(direction);
            explosion.DestroyAfter(explosionDuration);
            Explode(position,direction,explosionLenght - 1);
        }

         private void ClearDestructible(Vector2 position)
         {
             Vector3Int cell = destructibleTiles.WorldToCell(position);
             TileBase tile = destructibleTiles.GetTile(cell);

             if(tile != null)
             {
                 Instantiate(destructiblePrefab,position,Quaternion.identity);
                 destructibleTiles.SetTile(cell,null);
             }
         }


         public void AddBomb()
         {
             bombAmount++;
             bombsRemaining++;
         }

         private void OnTriggerEnter2D(Collider2D item)
         {
             if(item.gameObject.tag == "Items")
             {
                 itemPickup.Play();
             }

         }
         
         
    }   

}
