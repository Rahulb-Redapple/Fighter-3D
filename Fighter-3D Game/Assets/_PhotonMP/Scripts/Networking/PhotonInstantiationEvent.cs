using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
//using Cinemachine;

namespace PhotonMultiplayer
{
    public class PhotonInstantiationEvent : MonoBehaviour
    {
        public Transform localPlayerPos;
        public Transform remotePlayerPos;

        [SerializeField] private GameObject _player;
        [SerializeField] private TMP_Text _pingText;
        //[SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;


        private void OnEnable()
        {
            SubscribeEvent();
        }

        private void OnDisable()
        {
            UnsubscribeEvent();
        }

        private void Start()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            GameObject player = Instantiate(_player, localPlayerPos.position, localPlayerPos.rotation);
            PhotonView view = player.GetComponent<PhotonNetworkSync>().GetPhotonView();

            if(PhotonNetwork.AllocateViewID(view))
            {
                object data = view.ViewID;

                RaiseEventOptions op = new RaiseEventOptions()
                {
                    CachingOption = EventCaching.AddToRoomCache,
                    Receivers = ReceiverGroup.Others
                };
                PhotonNetwork.RaiseEvent(GameConstants.instantiationEventCode, data, op, SendOptions.SendReliable);
            }
        }

        private void OnEvent(EventData eventData)
        {
            if(eventData.Code == GameConstants.instantiationEventCode)
            {
                object data = eventData.CustomData;
                GameObject remotePlayer = Instantiate(_player, remotePlayerPos.position, remotePlayerPos.rotation);
                remotePlayer.GetComponent<PhotonNetworkSync>().SetViewId((int)data);
            }
        }   

        internal void SubscribeEvent()
        {
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        }

        internal void UnsubscribeEvent()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        }
    }
}
