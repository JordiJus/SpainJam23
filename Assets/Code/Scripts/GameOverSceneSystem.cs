using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverSceneSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public Button but;

    public void OnClickBut(){
        SceneManager.LoadScene(0);
    }
}
