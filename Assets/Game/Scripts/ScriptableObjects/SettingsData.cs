
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsData", menuName = "ScriptableObject/General/SettingsData")]
public class SettingsData : ScriptableObject
{
    public SoundData soundData;
    public PlayerStartParams playerStartParams;
}
