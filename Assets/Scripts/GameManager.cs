using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Bomberman
{
    public class GameManager : MonoBehaviour
    {   
        
        [Header ("Players")]
        public GameObject[] players;

        [Header ("Sounds Used")]
        public AudioSource RoundWin;

        public void CheckWinState()
        {
            int aliveCount = 0;
            
            foreach(GameObject player in players) ///for each object in Players array checks if player game object is SetActive then increments the Players alivecount
            {
                if(player.activeSelf)
                {
                    aliveCount++;
                }
            }

            if(aliveCount <= 1) /// if there's only one player left, restarts the game by reloading the whole Scene
            {
                RoundWin.Play();
                Invoke(nameof(NewRound) ,3f);
            }

        }

        private void NewRound()
        {
            SceneManager.LoadScene("Bomberman");
        }

   
    }
}
