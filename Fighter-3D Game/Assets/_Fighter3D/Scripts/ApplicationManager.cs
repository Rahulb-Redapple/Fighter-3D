using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fighting3D
{
    public class ApplicationManager : MonoBehaviour
    {
        [SerializeField] private AIController aIController;
        [SerializeField] private FighterController playerController;

        private EssentialConfigData _essentialConfigData;

        private void Awake()
        {
            _essentialConfigData = Resources.Load<EssentialConfigData>(nameof(EssentialConfigData));
            _essentialConfigData.Init();

            aIController.Init(_essentialConfigData);
            //playerController.Init(_essentialConfigData);    
        }
    }
}
