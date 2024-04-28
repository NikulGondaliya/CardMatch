using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource1;
    [SerializeField] private AudioSource m_AudioSource2;
    [SerializeField] private AudioClip flip;
    [SerializeField] private AudioClip match;
    [SerializeField] private AudioClip mitchMatch;
    [SerializeField] private AudioClip Click;

    public void ClickButton() => m_AudioSource1.PlayOneShot(Click);

    public void FlipCard() => m_AudioSource1.PlayOneShot(flip);

    public void MatchCard() => m_AudioSource2.PlayOneShot(match);

    public void MitchMatch() => m_AudioSource2.PlayOneShot(mitchMatch);
}
