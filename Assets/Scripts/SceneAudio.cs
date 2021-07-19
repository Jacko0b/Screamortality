using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAudio : MonoBehaviour
{
    [SerializeField] private SceneCollector sceneManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private AudioSource audioSourceCultists;

    [SerializeField] private AudioClip sound;

    private bool paused = false;
    private int sceneIndex ;

    private void Awake()
    {
        sceneIndex = sceneManager.getCurrentSceneIndex();
    }
    private void FixedUpdate()
    {
        if (sceneIndex != sceneManager.getCurrentSceneIndex())
        {

            audioSourceCultists.Pause();

            if (sceneManager.getCurrentSceneName() == "MainMenu")
            {
                playAudio1("mainmenu");
                playAudio2("menuClick");
            }
            else if (sceneManager.getCurrentSceneName() == "Prolog")
            {
                playAudio1("rain");

            }
            else if (sceneManager.getCurrentSceneName() == "Lvl1")
            {
                playAudio1("loop0");
                playAudio2("Into_Room_1_0");

            }

            else if (sceneManager.getCurrentSceneName() == "Lvl4")
            {
                playAudio1("loop1");
                playAudio2("Into_Room_1_1");


            }
            else if (sceneManager.getCurrentSceneName() == "Lvl5")
            {
                playAudio1("loop1");
                playAudio2("Into_Room_2_1");


            }
            else if (sceneManager.getCurrentSceneName() == "Lvl7")
            {
                playAudio1("loop2");
                playAudio2("Into_The_Room_1_2");

                audioSourceCultists.volume = 0.2f;
                audioSourceCultists.Play();
                audioSourceCultists.UnPause();


            }
            else if (sceneManager.getCurrentSceneName() == "Lvl8")
            {
                audioSourceCultists.volume = 0.3f;
                audioSourceCultists.Play();
                audioSourceCultists.UnPause();


            }
            else if (sceneManager.getCurrentSceneName() == "Lvl9")
            {
                audioSourceCultists.volume = 0.4f;
                audioSourceCultists.Play();
                audioSourceCultists.UnPause();


            }
            else if (sceneManager.getCurrentSceneName() == "DeathScreen")
            {
                int i = Random.Range(0, 2);
                if(i==0)
                {
                    playAudio2("death1");

                }
                else if (i == 1)
                {
                    playAudio2("death2");

                }
                else if (i == 2)
                {
                    playAudio2("death3");

                }
            }
            else if (sceneManager.getCurrentSceneName() == "LoseScreen")
            {
                playAudio1("loop3");
                playAudio2("lose");
                
            }
            else if (sceneManager.getCurrentSceneName() == "WinScreen")
            {
                playAudio1("loop3");
                playAudio2("win");

            }
            sceneIndex = sceneManager.getCurrentSceneIndex();
        }
    }


    private void playAudio1(string name)
    {
        sound = (AudioClip)Resources.Load(name);
        audioSource.clip = sound;
        audioSource.Play();
    }
    private void playAudio2(string name)
    {
        sound = (AudioClip)Resources.Load(name);
        audioSource2.clip = sound;
        audioSource2.Play();
    }
    public void audioPause()
    {
        if (paused)
        {
            audioSource.UnPause();
            audioSource2.Pause();
            paused = false;
        }
        else
        {
            audioSource.Pause();
            audioSource2.Pause();

            paused = true;
        }
    }



}
