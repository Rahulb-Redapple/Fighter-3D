using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhotonMultiplayer
{
    public class PhotonCustomProperties : MonoBehaviour
    {
        private ExitGames.Client.Photon.Hashtable _profileIconProperties = new ExitGames.Client.Photon.Hashtable();

        internal void SetProfileIcon(int index)
        {
            _profileIconProperties[GameConstants.IconKey] = index;
            PhotonNetwork.LocalPlayer.CustomProperties = _profileIconProperties;

            //PhotonNetwork.SetPlayerCustomProperties(_profileIconProperties);
        }
    }
}
