using System;
using UnityEngine;

public class SpawnBomb : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private Transform bt;
    
    public void spawnBomb()
    {
        Instantiate(bomb, bt.position, bt.rotation);
    }
}
