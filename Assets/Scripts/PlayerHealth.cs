using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    public float currentHealth = 10f;
    public float damageScaler = 1.0f;

    private bool isSafe = false;

    void Start()
    {

    }

    void FixedUpdate()
    {

        //Debug.Log("health: " + currentHealth);

        if (currentHealth >= 0)
        {
            if (isSafe == false)
            {
                currentHealth -= Time.deltaTime * damageScaler;
            }
        }
        else
        {
            Die();
        }
 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SafeZone"))
        {
            //Debug.Log("safe!");
            isSafe = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SafeZone"))
        {
            //Debug.Log("not safe!");
            isSafe = false;
        }
    }

    private void Die()
    {
        Debug.Log("player dead bro");
        Destroy(gameObject);
    }



    
}
