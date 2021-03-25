using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public InputField playerName;
    public bool saved = false;

    public void Save()
    {
        PlayerPrefs.SetString("PlayerName", playerName.text);
        saved = true;
    }
}
