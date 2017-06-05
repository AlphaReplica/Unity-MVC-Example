using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// C in MVC
/// </summary>
public class AppController : MonoBehaviour
{
    public AppModel model;

    /// <summary>
    /// When started defaultly player is first, cells get resetted
    /// </summary>
    void Start ()
    {
        model.playerturn = true;
        model.resetCells();
	}

    /// <summary>
    /// returns true if all cells are filled, required to detect Draw
    /// </summary>
    private bool allCellsFilled
    {
        get
        {
            bool hasEmpty = false;
            for (int num = 0; num < model.cells.Length; num++)
            {
                if (model.cells[num] == -1)
                {
                    hasEmpty = true;
                }
            }
            return hasEmpty;
        }
    }

    /// <summary>
    /// detects win by 3 same id on row/col/diagonally
    /// </summary>
    private int detectWin()
    {
        int won = -1;
        int player = (model.playerIsX) ? 0 : 1;
        int ai     = (model.playerIsX) ? 1 : 0;

        for (int num = 2; num < model.rows.Length; num += 3)
        {
            int playerScore = 0;
            int aiScore     = 0;
            if (model.cells[model.rows[num    ]] == player) { playerScore++; }
            if (model.cells[model.rows[num - 1]] == player) { playerScore++; }
            if (model.cells[model.rows[num - 2]] == player) { playerScore++; }

            if (model.cells[model.rows[num    ]] == ai) { aiScore++; }
            if (model.cells[model.rows[num - 1]] == ai) { aiScore++; }
            if (model.cells[model.rows[num - 2]] == ai) { aiScore++; }

            if (playerScore == 3) { won = 0;}
            if (aiScore     == 3) { won = 1;}
            if (won>-1)
            {
                break;
            }
        }
        if (won < 0 && !allCellsFilled)
        {
            won = 2;
        }
        return won;
    }


    /// <summary>
    /// gets random cell, if nothing is returned calls same method to take first empty cell and returns
    /// </summary>
    private int getCellRandom(bool getFirstEmpty = false)
    {
        for (int num = 0; num < model.cells.Length; num ++)
        {
            if (model.cells[num] < 0)
            {
                int val = Random.Range(-1, 1);

                if (val > -1 || getFirstEmpty)
                {
                    return num;
                }
            }
        }
        return getCellRandom(true);
    }

    /// <summary>
    /// gets cell by difficulty, in most cases it'll return random cell index if difficulty criteria doesn't match
    /// </summary>
    private int getCell(int difficulty)
    {
        int index   = -1;
        int player  = (model.playerIsX) ? 0 : 1;
        
        for(int num = 2; num < model.rows.Length; num+=3)
        {
            int score = 0;
            if (model.cells[model.rows[num  ]] == player){score++;}
            if (model.cells[model.rows[num-1]] == player){score++;}
            if (model.cells[model.rows[num-2]] == player){score++;}

            if (score < 3 && score >= difficulty)
            {
                if (model.cells[model.rows[num    ]] == -1) { index = model.rows[num    ];}
                if (model.cells[model.rows[num - 1]] == -1) { index = model.rows[num - 1];}
                if (model.cells[model.rows[num - 2]] == -1) { index = model.rows[num - 2];}
            }

            if(index>-1)
            {
                break;
            }
        }
        return (index < 0) ? getCellRandom() : index;
    }

    /// <summary>
    /// chooses cell for AI and checks winner
    /// </summary>
    private void aiSelection()
    {
        model.updateCell(getCell(model.difficulty), (model.playerIsX) ? 1 : 0);
        model.updateGame(detectWin());
    }

    /// <summary>
    /// waits for 1 second and calls AI celection method
    /// </summary>
    private IEnumerator waitAndSelect()
    {
        yield return new WaitForSeconds(1);

        aiSelection();
    }

    /// <summary>
    /// called from view cell click
    /// </summary>
    public void onCellClicked(int num)
    {
        model.updateCell(num, ((model.playerturn && model.playerIsX) ? 0 : 1));
        model.updateGame(detectWin());

        if (!model.playerturn && model.winner<0)
        {
            StartCoroutine(waitAndSelect());
        }
    }
    
    /// <summary>
    /// resets cells and starts game with AI if player turn is false
    /// </summary>
    public void restart()
    {
        model.resetCells();
        if (!model.playerturn && model.winner < 0)
        {
            StartCoroutine(waitAndSelect());
        }
    }

    /// <summary>
    /// called when menu button get clicked in view
    /// </summary>
    public void goToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}