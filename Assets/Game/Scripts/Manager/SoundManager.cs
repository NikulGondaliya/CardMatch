using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource m_AudioSource;
    [SerializeField] 
    private AudioClip flip, match, mitchMatch;


    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }


    public void FlipCard()
    {
        m_AudioSource.clip = flip;
        m_AudioSource.Play();
    }

    public void MatchCard()
    {
        m_AudioSource.clip = match;
        m_AudioSource.Play();
    }
    public void MitchMatch()
    {
        m_AudioSource.clip = mitchMatch;
        m_AudioSource.Play();
    }





}
