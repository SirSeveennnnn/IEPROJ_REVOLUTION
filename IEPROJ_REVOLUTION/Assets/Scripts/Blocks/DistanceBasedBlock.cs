using UnityEngine;

public abstract class DistanceBasedBlock : MonoBehaviour
{
    [SerializeField] protected LevelSettings levelSettings;

    public void SetLevelSettings(LevelSettings levelSettings)
    {
        this.levelSettings = levelSettings;
    }
}
