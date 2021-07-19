using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolCircle : MonoBehaviour
{
    [SerializeField] private int activeSymbols;
    [SerializeField] private int amountOfSymbols;
    [SerializeField] private SpriteRenderer circle;
    [SerializeField] private Sprite[] sprites;


    public int ActiveSymbols { get => activeSymbols; set => activeSymbols = value; }
    public int AmountOfSymbols { get => amountOfSymbols; set => amountOfSymbols = value; }

    private void Awake()
    {
        circle = GetComponent<SpriteRenderer>();
        
    }
    private void FixedUpdate()
    {
        circle.sprite = sprites[activeSymbols];
    }
}
