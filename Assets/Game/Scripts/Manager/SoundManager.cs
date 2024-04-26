using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource1;
    [SerializeField] private AudioSource m_AudioSource2;
    [SerializeField] private AudioClip flip, match, mitchMatch;


    public void FlipCard()
    {
        m_AudioSource1.clip = flip;
        m_AudioSource1.Play();
    }

    public void MatchCard()
    {
        m_AudioSource2.clip = match;
        m_AudioSource2.Play();
    }
    public void MitchMatch()
    {
        m_AudioSource2.clip = mitchMatch;
        m_AudioSource2.Play();
    }





}
