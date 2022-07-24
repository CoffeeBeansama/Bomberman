using UnityEngine;
using Photon.Pun;

namespace Bomberman
{
    

    public class MovementController : MonoBehaviour
    {

        public new Rigidbody2D rigidbody2D {get; private set;} 
        private Vector2 direction = Vector2.down;
        public float movementSpeed = 5f;
        public KeyCode inputUp = KeyCode.W;
        public KeyCode inputDown = KeyCode.S;
        public KeyCode inputLeft = KeyCode.A;
        public KeyCode inputRight = KeyCode.D;
        public SpriteAnimator spriteRendererUp;
        public SpriteAnimator spriteRendererDown;
        public SpriteAnimator spriteRendererLeft;
        public SpriteAnimator spriteRendererRight;
        public SpriteAnimator spriteRendererDeath;
        private SpriteAnimator activeSpriteRenderer;
    
        
        



       

         private void Awake()
         {
             rigidbody2D = GetComponent<Rigidbody2D>();
             activeSpriteRenderer = spriteRendererDown;
             

         }

  
        private void Update()
        {
            ///1
         
                if(Input.GetKey(inputUp))
                {
                SetDirection(Vector2.up,spriteRendererUp);
                }else if(Input.GetKey(inputDown))
                {
                SetDirection(Vector2.down,spriteRendererDown);
                }else if(Input.GetKey(inputLeft))
                {
                SetDirection(Vector2.left,spriteRendererLeft);
                }else if(Input.GetKey(inputRight))
                {
                SetDirection(Vector2.right,spriteRendererRight);
                }else
                {
                SetDirection(Vector2.zero,activeSpriteRenderer);
                }
            
        }

        private void FixedUpdate()
        {
            //3
            Vector2 position = rigidbody2D.position;
            Vector2 translation = direction * movementSpeed * Time.fixedDeltaTime;

            rigidbody2D.MovePosition(position + translation);
        }

        private void SetDirection(Vector2 newdirection,SpriteAnimator animation)
        {
            ///2
            direction = newdirection;
            spriteRendererUp.enabled = animation == spriteRendererUp;
            spriteRendererDown.enabled = animation == spriteRendererDown;
            spriteRendererLeft.enabled = animation == spriteRendererLeft;
            spriteRendererRight.enabled = animation == spriteRendererRight;
            activeSpriteRenderer = animation;
            activeSpriteRenderer.idle = direction == Vector2.zero;
        }

        private void OnTriggerEnter2D(Collider2D fire)
         {   
            if(fire.gameObject.tag == "Fire")
             {
             DeathSequence(spriteRendererDeath);
            }

        

         }   

        private void DeathSequence(SpriteAnimator death)
        {
            spriteRendererDeath.enabled = death == spriteRendererDeath;

            //Disables movement and BombControl script
            enabled = false;
            GetComponent<BombController>().enabled = false;
            spriteRendererUp.enabled = false;
            spriteRendererDown.enabled = false;
            spriteRendererLeft.enabled = false;
            spriteRendererRight.enabled = false;
           
           ///Calls when Death Animation has ended and Set the game object to not Active
           Invoke(nameof(OnDeathSequenceEnded), 1.25f);
        }

        private void OnDeathSequenceEnded()
        {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().CheckWinState();
        }
    }

}
