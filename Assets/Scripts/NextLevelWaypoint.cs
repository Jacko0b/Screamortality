using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelWaypoint : MonoBehaviour
{
    [SerializeField] private SceneCollector sceneManager;
    [SerializeField] private SymbolCircle symbolCircle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (sceneManager.getCurrentSceneName() == "Lvl9")
            {
                if(symbolCircle.ActiveSymbols == symbolCircle.AmountOfSymbols)
                {
                    sceneManager.WinScreen();
                }
                else
                {
                    sceneManager.LoseScreen();
                }
            }
            else
            {
                if (symbolCircle.ActiveSymbols == symbolCircle.AmountOfSymbols)
                {
                    // sceneManager.WinScreen();
                    sceneManager.NextLevel();

                }
                else
                {
                    sceneManager.NextLevel();
                }
            }
        }
    }
}
