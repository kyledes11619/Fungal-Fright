using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public bool hard;
    public bool mute;

    public Toggle DifficultyToggle;
    public Toggle SoundToggle;

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void PlayButton()
    {
        //Menu.LoadScene("CabinLevel");
        UnityEngine.SceneManagement.SceneManager.LoadScene( "CabinLevel");

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hard = DifficultyToggle.isOn;
        mute = SoundToggle.isOn;
    }
}