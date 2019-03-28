using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine; 


namespace Concordia1.Gamelab
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
   
        [SerializeField]
        private byte maxPlayersPerRoom = 2;
        bool isConnecting;
        string gameVersion = "1";
        [SerializeField]
        private GameObject controlPanel;
        [SerializeField]
        private GameObject progressLabel;

        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        public void Connect()
        {
            
            isConnecting = true;
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            if (PhotonNetwork.IsConnected)
            {
                //PhotonNetwork.JoinRandomRoom();
                PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom }, null, null);

            }
            else
            {
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Joined Master room");
            if (isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
                //PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom }, null, null);
            }
        }


        public override void OnDisconnected(DisconnectCause cause)
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            Debug.LogWarningFormat("Disconnected");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("COnnection Failed");
            PhotonNetwork.CreateRoom(null, new RoomOptions{ MaxPlayers = maxPlayersPerRoom }, null, null);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("Room gen failed " + returnCode + "  " + message);
            base.OnCreateRoomFailed(returnCode, message);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Connected as Client");
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("RoomGenerator");
            }

        }

    }
}