using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Symbol : MonoBehaviour
{
    [SerializeField] private Light2D light2d;
    [SerializeField] private SpriteRenderer spriteVisible;
    [SerializeField] private SpriteRenderer spriteActivated;
    [SerializeField] private float timeToActivate;
    [SerializeField] private float timeRemaining;
    [SerializeField] private bool activated;
    [SerializeField] private int id;

    private float alfa;

    public int Id { get => id; set => id = value; }
    public bool Activated { get => activated; set => activated = value; }

    private void Awake()
    {
        
        timeRemaining = timeToActivate * 50;
    }
    public void activate()
    {
            activated = true;
            light2d.enabled = true;

            spriteActivated.color = Color.white;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flashlight") && !activated)
        {
            spriteVisible.enabled = true;
            light2d.enabled = true;

        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //50x / sekunde
        if (collision.CompareTag("Flashlight") && !activated)
        {
            
            if (timeRemaining > 0)
            {
                timeRemaining -= 1;
                if (timeRemaining % 50 == 0)
                {
                    alfa += 1 / timeToActivate;
                    spriteActivated.color = new Color(1,1,1,alfa);
                }
            }
            else
            {
                spriteActivated.color = Color.white;
                activated = true;
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Flashlight") && !activated)
        {
            spriteVisible.enabled = false;
            spriteActivated.color = Color.clear;
            timeRemaining = timeToActivate * 50;
            light2d.enabled = false;
            alfa = 0;

        }

    }
}
