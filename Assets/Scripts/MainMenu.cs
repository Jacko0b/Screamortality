using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneCollector sceneManager;
    [SerializeField] private SceneAudio sceneAudio;
    [SerializeField] private AudioClip sound;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private GameObject ingameMenu;

    private void Start()
    {
        sound = (AudioClip)Resources.Load("menuClick");
        audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (sceneAudio == null)
        {
            sceneAudio = FindObjectOfType<SceneAudio>();

        }
    }
    public void playClick()
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
    public void B_NewGame()
    {
        playClick();
        sceneManager.NextLevel();
    }
    public void B_Quit()
    {
        playClick();
        Application.Quit();
    }
    public void B_Continue()
    {
        playClick();
        Time.timeScale = 1;
        //sceneAudio.audioPause();
        ingameMenu.SetActive(false);
    }
    public void B_BackToMenu()
    {
        playClick();
        Time.timeScale = 1;
        //sceneAudio.audioPause();
        sceneManager.LoadMainMenu();

    }

    public void OpenMenuIngame()
    {

        if(ingameMenu.activeInHierarchy == false)
        {
            Time.timeScale = 0;
            playClick();
            //sceneAudio.audioPause();
            ingameMenu.SetActive(true);
        }
        else
        {
            B_Continue();
        }
    }
}

