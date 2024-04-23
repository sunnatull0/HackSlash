using UnityEngine;
using UnityEngine.UI;

public class CustomButtonArea : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }
}
