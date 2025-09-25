using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField] private AudioClip lightbulbSoundClip;

    public int PlayerCollectNumber = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lightbulb"))
        {
            PlayerCollectNumber += 1;
            Debug.Log("collect #" + PlayerCollectNumber);
            SoundFXManager.instance.PlaySoundFXClip(lightbulbSoundClip, transform, 2f);
        }
    }
}