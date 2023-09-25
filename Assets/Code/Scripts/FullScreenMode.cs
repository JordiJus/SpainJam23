using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenMode : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}
