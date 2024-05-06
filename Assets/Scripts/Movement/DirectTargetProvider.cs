using UnityEngine;

public class DirectTargetProvider : TargetProvider
{
    public override Vector3 GetTarget(Vector3 currentPos) => new Vector3(Vector3.left.x * 100, currentPos.y,0f);
}