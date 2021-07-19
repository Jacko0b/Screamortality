using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolsGroup : MonoBehaviour
{
    [SerializeField] private List<Symbol> symbolList;
    [SerializeField] private SymbolCircle circle;
    [SerializeField] private int counter=0;

    private void Start()
    {
        counter = 0;
        for (int i = 0; i < 24; i++)
        {
            if (PlayerStats.ActivatedSymbols[i] == true)
            {
                symbolList[i].activate();
            }
        }
        circle.AmountOfSymbols = symbolList.Count;
    }
    private void FixedUpdate()
    {
        if(Time.frameCount % 25 == 0)
        {
            saveSymbols();
            CountActiveSymbols();
        }
    }
    private int CountActiveSymbols()
    {
        counter = 0;
        for(int i = 0; i < 24; i++)
        {
            if (PlayerStats.ActivatedSymbols[i] == true)
            {
                counter++;
            }            
        }
        circle.ActiveSymbols = counter;

        return counter;
    }
    private void OnDestroy()
    {
        saveSymbols();
    }
    private void saveSymbols()
    {
        foreach (Symbol symbol in symbolList)
        {
            if (symbol.Activated)
            {
                PlayerStats.ActivatedSymbols[symbol.Id] = true ;

            }
        }
    }
}
