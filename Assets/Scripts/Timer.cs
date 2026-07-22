using UnityEngine;

public class Timer : MonoBehaviour
{
    public float _timer;
    void Update()
    {
        _timer += Time.fixedDeltaTime;
    }
}
