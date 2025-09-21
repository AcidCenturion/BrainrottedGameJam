using UnityEngine;

public class Lightbulb : MonoBehaviour
{
    public bool CollectedLightbulb1 = false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectedLightbulb1 = true;
            Destroy(this.gameObject);
        }
    }
}
