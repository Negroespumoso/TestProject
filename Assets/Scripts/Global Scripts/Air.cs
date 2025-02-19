using UnityEngine;

public class Air : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            player.SwapState(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            player.SwapState(true);
        }
    }
}
