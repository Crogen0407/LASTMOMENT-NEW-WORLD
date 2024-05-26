using UnityEngine;

[System.Serializable]
public struct TechniqueTrm
{
    public float duration;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public float delay;
}

[CreateAssetMenu(menuName = "SO/ACMData")]
public class ACMDataSO : ScriptableObject
{
    public ACMEnum enumType;
    public TechniqueTrm[] techniqueTransforms;
}
