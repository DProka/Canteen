
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObject/General/SoundData")]
public class SoundData : ScriptableObject
{
    [Header("Game")]

    public AudioClip money;

    public AudioClip burner1;
    public AudioClip burner2;
    public AudioClip drink;
}
