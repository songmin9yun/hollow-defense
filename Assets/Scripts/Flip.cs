using System;
using UnityEngine;

public class Flip : MonoBehaviour
{
    [SerializeField] private GameObject flipPrefab;
    public float dir;

    private void Update()
    {
        dir = flipPrefab.transform.localScale.x;
    }

    public void rightFliping()
    {
        flipPrefab.transform.localScale = new Vector3(1, 1, 1);
    }

    public void leftFliping()
    {
        flipPrefab.transform.localScale = new Vector3(-1, 1, 1);
    }
}
