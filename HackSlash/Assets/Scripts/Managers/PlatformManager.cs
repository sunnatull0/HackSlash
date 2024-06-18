using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlatformManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _mobileControls;
        [SerializeField] private bool _checkPlatform;

        private void Start()
        {
            if(!_checkPlatform)
                return;
            
            if (Application.isMobilePlatform) 
                return;
        
            foreach (var control in _mobileControls)
            {
                control.SetActive(false);
            }
        }
    }
}