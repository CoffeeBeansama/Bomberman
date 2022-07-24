using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace Bomberman
{
public class Connection : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");

    }


    
   
}

}