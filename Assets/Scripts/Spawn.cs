using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] bool occupied = false;
    [SerializeField] int spawnID ;


    public bool Occupied { get => occupied; set => occupied = value; }
    public int SpawnID { get => spawnID; set => spawnID = value; }
}
