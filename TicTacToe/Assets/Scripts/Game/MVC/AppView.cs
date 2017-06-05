using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// V in MVC
/// </summary>
public class AppView : MonoBehaviour
{
    /// <summary>
    /// GameObject references that are set in editor
    /// </summary>
    public  AppController controller;
    public  Transform     gameCont;
    public  Transform     buttonsCont;
    public  Text          turnLabel;
    
    public  Transform     popupCont;
    public  Text          popUpLabel;
    public  Button        restartBtn;
    public  Button        menutBtn;

    public   Sprite[]     icons;

    private Button[]      buttons;
    
    /// <summary>
    /// Creates Buttons array from buttonsCont children and assigns event listeners to all buttons
    /// </summary>
    private void setView()
    {
        restartBtn.onClick.AddListener(controller.restart);
        menutBtn  .onClick.AddListener(controller.goToMenu);

        buttons = new Button[buttonsCont.childCount];

        for (int num = 0; num < buttonsCont.childCount; num++)
        {
            buttons[num] = buttonsCont.GetChild(num).GetComponent<Button>();
            addListenerToButton(num);
        }
    }

    /// <summary>
    /// Adds event listener to button by array index
    /// </summary>
    private void addListenerToButton(int num)
    {
        buttons[num].onClick.AddListener(() =>
        {
            controller.onCellClicked(num);
        });
    }

    /// <summary>
    /// Updates view by model data
    /// </summary>
    public void updateView(int[] cells, bool isPlayerTurn)
    {
        if(buttons == null)
        {
            setView();
        }

        for (int num = 0; num < buttons.Length; num++)
        {
            buttons[num].image.sprite = (cells[num] > -1) ? ((cells[num] == 0) ? icons[0] : icons[1]) : null;
            buttons[num].interactable = (cells[num] > -1) ? false : isPlayerTurn;
        }

        turnLabel.text = ((isPlayerTurn) ? "Player" : "AI") + " Turn";
    }
    
    /// <summary>
    /// shows corresponding view by won type
    /// </summary>
    public void updateWinner(int won)
    {
        gameCont .gameObject.SetActive(false);
        popupCont.gameObject.SetActive(false);
        switch (won)
        {
            case -1:
                {
                    gameCont.gameObject.SetActive(true);
                    break;
                }
            case  0:
                {
                    popupCont.gameObject.SetActive(true);
                    popUpLabel.text = "Player Won!";
                    break;
                }
            case  1:
                {
                    popupCont.gameObject.SetActive(true);
                    popUpLabel.text = "AI Won!";
                    break;
                }
            case  2:
                {
                    popupCont.gameObject.SetActive(true);
                    popUpLabel.text = "DRAW!";
                    break;
                }
        }
    }
}
