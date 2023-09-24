using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    public List<GameObject> images;
    public bool helpState;
    public void OnHelpButton(){
        if (helpState == false) {
            helpState = true;
        } else {
            helpState = false;
        }

        foreach(GameObject element in images){
            element.SetActive(helpState);
        }
    }
}
