using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static bool hardMode, mute;
    public GameObject hardFire, muteFire;

    public void Awake() {
        AudioListener.volume = mute ? 0 : 1;
    }

    public void ToggleHard() {
        hardMode = !hardMode;
        hardFire.SetActive(hardMode);
    }

    public void ToggleMute() {
        mute = !mute;
        muteFire.SetActive(mute);
        AudioListener.volume = mute ? 0 : 1;
    }
}
