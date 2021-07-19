using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCollector : MonoBehaviour
{
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private Animator transition;

    private void Awake()
    {
        if (getCurrentSceneName() == "Lvl1")
        {
            //reset
            for (int i = 0; i < PlayerStats.ActivatedSymbols.Length; i++)
            {
                PlayerStats.ActivatedSymbols[i] = false;
            }

            PlayerStats.FirstFlashlightPickup = true;
            PlayerStats.closedDoor = false;
            PlayerStats.monsterNoticed = false;
            PlayerStats.symbolNoticed = false;
            PlayerStats.symbolSolved = false;
}
    }
    private void FixedUpdate()
    {
       /* for (int i = 0; i < PlayerStats.ActivatedSymbols.Length; i++)
        {
            Debug.Log(PlayerStats.ActivatedSymbols[i]);
        }*/
    }
    public void NextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void RetryGame()
    {
        SceneManager.LoadScene("Lvl1");
    }
    public void WinScreen()
    {
        SceneManager.LoadScene("WinScreen");
    }
    public void LoseScreen()
    {
        SceneManager.LoadScene("LoseScreen");
    }
    public void DeathScreen()
    {
        SceneManager.LoadScene("DeathScreen");
    }
    private IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

    }
    public int getCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    public string getCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

}
