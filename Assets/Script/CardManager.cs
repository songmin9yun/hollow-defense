using UnityEngine;

public class CardManager : MonoBehaviour
{
    
    [SerializeField] private GameObject cardObjects;
    
    void Start()
    {
        cardObjects.SetActive(false);
    }


    public void cardGet()
    {
        cardObjects.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnClickCard()
    {
        cardObjects.SetActive(false);
        Time.timeScale = 1;
    }
}
