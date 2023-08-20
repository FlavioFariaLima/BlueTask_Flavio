using UnityEngine;

public class Gameplay_SoundLibrary : MonoBehaviour
{
    public static Gameplay_SoundLibrary instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    public Sound[] sounds;

    private AudioSource audioSource;

    private void Awake()
    {
        // Implementing Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(string soundName)
    {
        Sound soundToPlay = System.Array.Find(sounds, sound => sound.name == soundName);
        if (soundToPlay != null)
        {
            audioSource.PlayOneShot(soundToPlay.clip);
        }
        else
        {
            Debug.LogWarning("Sound named " + soundName + " not found!");
        }
    }
}
