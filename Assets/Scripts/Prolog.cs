using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Prolog : MonoBehaviour
{
    [SerializeField] private SceneCollector sceneManager;
    [SerializeField] private AudioSource prologAudio;

    [SerializeField] private bool turningVolumeDown = false;
    private void Update()
    {
        TurningVolumeDown();
    }
    private void TurningVolumeDown()
    {
        if (turningVolumeDown)
        {
            prologAudio.volume -= 0.5f * Time.deltaTime;
        }
    }
    public void OnSkip(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            sceneManager.NextLevel();
            turningVolumeDown = true;
        }
    }

}
