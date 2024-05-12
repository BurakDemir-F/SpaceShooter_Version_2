using UnityEngine;

public class InScreenTargetProvider : TargetProvider
{
    public override Vector3 GetTarget(Vector3 currentPos) => new Vector3(Vector3.right.x * 10, currentPos.y,0f);
}