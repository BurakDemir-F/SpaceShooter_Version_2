using UnityEngine;

public abstract class TargetProvider : MonoBehaviour, ITargetProvider
{
    public abstract Vector3 GetTarget(Vector3 currentPos);
}