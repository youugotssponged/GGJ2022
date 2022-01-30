using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LEVELTHEMEPLAYER : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip Theme;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = Theme;
        AudioSource.loop = true;
        AudioSource.Play();
    }
}
