
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerModel", menuName = "ScriptableObjects/PlayerModel")]
public class PlayerModel: ScriptableObject
{
    public int Points = 0;

    public void Reset()
    {
        Points = 0;
    }
}
