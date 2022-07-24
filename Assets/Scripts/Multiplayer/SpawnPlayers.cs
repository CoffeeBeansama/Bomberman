using UnityEngine;
using Photon.Pun;


namespace Bomberman
{
    public class SpawnPlayers : MonoBehaviourPunCallbacks
    {   
        
        
        private bool IsPlayersInstatiated;

         [Header ("Players")]
         public GameObject player1Prefab;
         public GameObject player2Prefab;

    //public GameObject[] playerPrefab;

        

        private void Update()
        {
            
            if(!IsPlayersInstatiated)
            {
            
            PhotonNetwork.Instantiate(player1Prefab.name,new Vector3(-6.52f,4.48f,0),Quaternion.identity);
            PhotonNetwork.Instantiate(player2Prefab.name,new Vector3(5.42f,-5.48f,0),Quaternion.identity);
            IsPlayersInstatiated = true;
            }

        }

       
        

       

    

    }
}