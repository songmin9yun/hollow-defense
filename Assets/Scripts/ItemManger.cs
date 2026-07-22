using UnityEngine;
public class ItemManager : MonoBehaviour
{
    [SerializeField] PlayerState player;
    
    public void PoisonFang()
    {
        player.poisonFang=true;
    }
    
    public void SteamGenerator()
    {
        player.steamGenerator=true;
    }
}