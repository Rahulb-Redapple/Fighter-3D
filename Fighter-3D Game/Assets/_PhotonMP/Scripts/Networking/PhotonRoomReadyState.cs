using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhotonMultiplayer
{
    public class PhotonRoomReadyState : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _buttonStatusText;

        internal void UpdateButtonState(string text)
        {
            _buttonStatusText.text = text;
        }
    }
}
