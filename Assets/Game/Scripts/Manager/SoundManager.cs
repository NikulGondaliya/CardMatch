using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource m_AudioSource1;
    public AudioSource m_AudioSource2;
    [SerializeField] 
    private AudioClip flip, match, mitchMatch;


    private void Start()
    {
        m_AudioSource1 = GetComponent<AudioSource>();

    }


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
