using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int playerHealth = 100;

    void Start()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth -= 1;
            Debug.Log("Health: " + playerHealth);
        }
    }
}
