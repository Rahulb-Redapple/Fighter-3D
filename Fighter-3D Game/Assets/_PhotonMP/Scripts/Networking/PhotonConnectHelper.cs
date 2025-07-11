using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;

namespace PhotonMultiplayer
{
    public class PhotonConnectHelper : MonoBehaviourPunCallbacks
    {
        public static PhotonConnectHelper instance;

        public delegate void ConnectionSuccessAction();
        public event ConnectionSuccessAction OnConnectionSuccess;

        public delegate void ConnectionToLobbySuccessAction();
        public event ConnectionToLobbySuccessAction OnConnectionToLobbySuccess;


        private void Awake()
        {
            instance = this;
        }

        internal void ConnectToServer()
        {
            PhotonNetwork.SendRate = 30;
            PhotonNetwork.SerializationRate = 10;
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.NickName = "Player " + Random.Range(1000, 10000);
            PhotonNetwork.ConnectUsingSettings(); 
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log($"<color=green> CONNECTED TO SERVER SUCCESSFULLY </color>");

            OnConnectionSuccess?.Invoke();

            if (!PhotonNetwork.InLobby)
                JoinLobby();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log($"<color=red> Disconnected from server due to {cause} </color>");
        }

        private void JoinLobby()
        {
            PhotonNetwork.JoinLobby();            
        }

        public override void OnJoinedLobby()
        {
            Debug.Log($"<color=yellow> CONNECTED TO LOBBY SUCCESSFULLY </color>");

            OnConnectionToLobbySuccess?.Invoke();
        }

        internal void DisconnectFromServer()
        {
            PhotonNetwork.Disconnect();
        }
    }
}
