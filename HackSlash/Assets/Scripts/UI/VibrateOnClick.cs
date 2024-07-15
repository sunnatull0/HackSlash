using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class VibrateOnClick : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            Vibration.VibrateLight();
        }
    }
}
