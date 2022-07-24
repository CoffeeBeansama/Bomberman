using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


namespace Bomberman
{

    public class CreateandJoinRoom : MonoBehaviourPunCallbacks
    {
        
        public InputField createRoom;
        public InputField joinRoom;
        public bool IsotherPlayerJoining;
        

        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(createRoom.text);
            IsotherPlayerJoining = false;
        }

        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(joinRoom.text);
            IsotherPlayerJoining = true;
        }

        public override void OnJoinedRoom()
        {
           PhotonNetwork.LoadLevel("Bomberman");
        }

    }
}

