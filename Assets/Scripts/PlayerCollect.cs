using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    public int PlayerCollectNumber = 0;

    public GameObject WinUIManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lightbulb"))
        {
            PlayerCollectNumber += 1;
            Debug.Log("collect #" + PlayerCollectNumber);
        }

        if (PlayerCollectNumber == 5)
        {
            if (other.CompareTag("WinCollider"))
            {
                WinUIManager.SetActive(true);
            }
        }
    }
}
