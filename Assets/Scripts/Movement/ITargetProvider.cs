using UnityEngine;

public interface ITargetProvider
{
    Vector3 GetTarget(Vector3 currentPos);
}