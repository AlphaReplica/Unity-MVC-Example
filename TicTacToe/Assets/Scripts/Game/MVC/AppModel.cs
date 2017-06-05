using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// M in MVC
/// </summary>
public class AppModel : MonoBehaviour
{
    public  AppView view;

    public  int   difficulty;
    public  bool  playerturn; 
    public  bool  playerIsX;
    public  int   winner;
    public  int[] cells; // grid cells for game
    public  int[] rows = new int[] {0,1,2, 3,4,5, 6,7,8,  // Horizontal
                                    0,3,6, 1,4,7, 2,5,8,  // Vertical
                                    0,4,8, 2,4,6};        // Diagonal 
                                                          // All possible linear variations for grid, represented as cell indexes

    /// <summary>
    /// Resets cells and player states and updated view
    /// </summary>
    public void resetCells()
    {
        difficulty = PlayerPrefs.GetInt("difficulty");

        playerIsX  = true;
        playerturn = true;
        switch(winner)
        {
            case 1:
                {
                    playerIsX  = false;
                    playerturn = false;
                    break;
                }
            case 2:
                {
                    playerIsX  = !playerIsX;
                    playerturn = !playerturn;
                    break;
                }
        }

        winner     = -1;
        cells      = new int[9];
        for (int num = 0; num < cells.Length; num++)
        {
            cells[num] = -1;
        }
        view.updateWinner(winner);
        view.updateView(cells, playerturn);
    }

    /// <summary>
    /// sets cell id by index and updated view
    /// </summary>
    public void updateCell(int num,int id)
    {
        // 0 Means [X]
        // 1 Means [O]

        cells[num] = id;
        playerturn = !playerturn;
        view.updateView(cells, playerturn);
    }

    /// <summary>
    /// sets winner and updated view
    /// </summary>
    public void updateGame(int won)
    {
        winner = won;
        view.updateWinner(winner);
    }
}
