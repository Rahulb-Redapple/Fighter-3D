using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class PhotonRotationSync : MonoBehaviourPunCallbacks
{
    private PhotonView _photonView;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();        
    }

    
    void Update()
    {
        if (_photonView.IsMine) 
        {
            RotationOverNetwork(transform.rotation);
        }
    }

    private void RotationOverNetwork(Quaternion rotation)
    {
        object[] data = new object[] { rotation.eulerAngles };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
        PhotonNetwork.RaiseEvent(1, data, raiseEventOptions, SendOptions.SendReliable);
    }   

    private void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if (eventCode == 1)
        {
            object[] data = (object[])photonEvent.CustomData;
            Vector3 receivedRotation = (Vector3)data[0];
            transform.rotation = Quaternion.Euler(receivedRotation);
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }
}
