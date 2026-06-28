
using System.Collections;
using UnityEngine;

public enum Sound
{
    Money,

    Burner1,
    Burner2,

    Drink
}

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

    [Header("Start Settings")]

    [SerializeField] float musicVolume = 0.8f;
    [SerializeField] float effectsVolume = 1f;

    [Header("Sources")]

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource effectsSource;
    [SerializeField] AudioSource effectsSource2;

    private SoundData soundData;

    private bool musicIsOn;
    private bool effectsIsOn;

    private bool soundIsPlaying;

    public void Init()
    {
        Instance = this;
        soundData = GameController.settings.soundData;

        soundIsPlaying = false;
        effectsIsOn = true;
    }

    public void SetSoundData(SoundData data)
    {
        soundData = data;
    }

    #region Switches

    public void SwitchMusic(bool isOn)
    {
        musicIsOn = isOn;
        musicSource.volume = isOn ? musicVolume : 0f;
    }

    public void SwitchEffects(bool isOn)
    {
        effectsIsOn = isOn;
        effectsSource.volume = isOn ? effectsVolume : 0f;
    }

    #endregion

    #region Sounds

    public void SwitchMainMenuMusic(bool isOn)
    {
        if (isOn && !musicSource.isPlaying)
            musicSource.Play();

        if (!isOn && musicSource.isPlaying)
            musicSource.Stop();
    }

    public void PlaySound(Sound sound)
    {
        if (effectsIsOn)
        {
            effectsSource.volume = effectsVolume;

            switch (sound)
            {
                case Sound.Money: StartSoundPlaying(soundData.money); break;
                case Sound.Burner1: StartSoundPlaying(soundData.burner1); break;
                case Sound.Burner2: StartSoundPlaying(soundData.burner2); break;
                case Sound.Drink: StartSoundPlaying(soundData.drink); break;
            }

            Debug.Log("Sound Is Played " + sound);
        } 
    }

    private void StartSoundPlaying(AudioClip track)
    {
        if (!soundIsPlaying)
        {
            soundIsPlaying = true;
            //effectsSource.volume = effectsIsOn ? 1f : 0f;
            effectsSource.PlayOneShot(track);
            StartCoroutine(SwitchOfSoundIsPlaying());

            Debug.Log("Sound Is Played");
        }
    }

    private IEnumerator SwitchOfSoundIsPlaying()
    {
        yield return new WaitForSeconds(0.2f);

        soundIsPlaying = false;
    }

    #endregion
}
