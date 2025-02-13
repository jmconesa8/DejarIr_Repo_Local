using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoLoader : MonoBehaviour
{
    // Start is called before the first frame update
public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay_Mainmenu");
    }
}
