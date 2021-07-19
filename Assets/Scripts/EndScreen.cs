using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private SceneCollector sceneManager;
    [SerializeField] private GameObject buttons;

    private void Awake()
    {
        StartCoroutine(ShowButtons());
    }
    IEnumerator ShowButtons()
    {
        yield return new WaitForSeconds(3.5f);
        buttons.SetActive(true);
    }
    public void OnRetry(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            sceneManager.RetryGame();
        }
    }
    public void OnExitToMainMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            sceneManager.LoadMainMenu();
        }
    }
}
