using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCanvas : MonoBehaviour
{

    private void LateUpdate(){
        if (Camera.main is{ }) transform.LookAt(Camera.main.transform);
        transform.Rotate(0,180,0);
    }
}
