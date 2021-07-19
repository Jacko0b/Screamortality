using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //#################AUDIO SOURCES#############################
    [SerializeField] private AudioSource batteryPickup;
    //###########################################################

    [SerializeField] private bool actionPerformed;
    [SerializeField] private GameObject battery;
    [SerializeField] private SceneCollector sceneManager;


    [SerializeField] private GameObject flashlight; 
    [SerializeField] private GameObject fakeFlashlight; 
    [SerializeField] private Flashlight flashlightScript;
    [SerializeField] private bool batteryInRange;


    public bool ActionPerformed { get => actionPerformed; set => actionPerformed = value; }

    private void Awake()
    {
        
    }
    private void Start()
    {
        if (PlayerStats.FirstFlashlightPickup != true)
        {
            fakeFlashlight.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        PerformAction();
    }
    private void PerformAction()
    {
        if (actionPerformed)
        {
            if (batteryInRange)
            {
                if (PlayerStats.FirstFlashlightPickup)
                {
                    batteryPickup.Play();
                    battery.SetActive(false);
                    flashlightScript.RefillBattery();
                    fakeFlashlight.SetActive(true);
                    PlayerStats.FirstFlashlightPickup = false;
                }
                else
                {
                    batteryPickup.Play();
                    battery.SetActive(false);
                    flashlightScript.RefillBattery();
                }

            }
        }
        actionPerformed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Battery"))
        {
            batteryInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Battery"))
        {
            batteryInRange = false;
        }

    }

}
