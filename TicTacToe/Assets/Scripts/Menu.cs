using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Menu Component for handling Menu Buttons
/// </summary>
public class Menu : MonoBehaviour
{
    /// <summary>
    /// Button Component references are set in editor
    /// </summary>
    public Button easy;
    public Button hard;
    public Button impossible;

    /// <summary>
    /// Buttons added event listeners when script started
    /// </summary>
    void Start ()
    {
        easy      .onClick.AddListener(onEasyClick);
        hard      .onClick.AddListener(onHardClick);
        impossible.onClick.AddListener(onImpossibleClick);
    }

    private void onEasyClick()
    {
        PlayerPrefs.SetInt("difficulty", 0);
        SceneManager.LoadScene("Game");
    }

    private void onHardClick()
    {
        PlayerPrefs.SetInt("difficulty", 1);
        SceneManager.LoadScene("Game");
    }

    private void onImpossibleClick()
    {
        PlayerPrefs.SetInt("difficulty", 2);
        SceneManager.LoadScene("Game");
    }
}
