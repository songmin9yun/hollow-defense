using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutManager : MonoBehaviour
{
    private PlayableDirector _playableDirector;
    [SerializeField] TimelineAsset[] _timeLine;
    void Start()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        Debug.Log(_timeLine);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CutScene")
        {
            other.gameObject.SetActive(false);
            _playableDirector.Play(_timeLine[0]);
        }
    }
}
