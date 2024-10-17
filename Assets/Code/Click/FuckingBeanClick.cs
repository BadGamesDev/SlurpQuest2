using UnityEngine;
using UnityEngine.EventSystems;

public class FuckingBeanClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<GameState>().globalPaused = false;
        FindObjectOfType<OverworldUI>().AddMessage("Congratulations, you have accomplished the difficult task of clicking on a bean!");
        Destroy(gameObject);
    }
}
