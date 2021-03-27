using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchingSceneBtn : MonoBehaviour
{
    public void SwitchMapFromOne()
    {
        SceneManager.LoadScene("Map2");
    }
    public void SwitchMapFromTwo()
    {
        SceneManager.LoadScene("Map1");
    }
}
