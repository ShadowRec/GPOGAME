using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolow : MonoBehaviour
{
    [SerializeField]
    private Transform thirdPerxonTransformView;

    private void Update()
    {
        transform.position = thirdPerxonTransformView.position;
    }
}
