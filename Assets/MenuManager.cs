using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public Slider brightness;
    public float boundVariable;

        public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene( "CabinLevel");
    }

        public void OnSliderValueChanged(float value)
    {
        boundVariable = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        brightness.value = boundVariable;
    }
}