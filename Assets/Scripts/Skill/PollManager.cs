using UnityEngine;
using System.Collections.Generic;

public class PollManager : MonoBehaviour
{

    public List<GameObject> skillPools;
    public GameObject skillPrefab;
    [SerializeField] int count;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(skillPrefab);
            skillPools.Add(obj);
            obj.SetActive(false);
        }
    }

    public void SkillOn(Vector2 pos)
    {
        for (int i = 0; i < skillPools.Count; i++)
        {
            if (!skillPools[i].activeSelf)
            {
                skillPools[i].transform.position = pos;
                skillPools[i].SetActive(true);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
