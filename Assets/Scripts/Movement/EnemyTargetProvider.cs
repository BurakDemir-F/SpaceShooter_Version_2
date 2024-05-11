using UnityEngine;

public class EnemyTargetProvider : TargetProvider
{
    public override Vector3 GetTarget(Vector3 currentPos) => new Vector3(Vector3.right.x * 100, currentPos.y,0f);
}