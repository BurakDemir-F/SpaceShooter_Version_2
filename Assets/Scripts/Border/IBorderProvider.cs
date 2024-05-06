using UnityEngine;

namespace Border
{
    public interface IBorderProvider
    {
        Vector3 TopBorder { get; }
        Vector3 DownBorder { get; }
        Vector3 LeftBorder { get; }
        Vector3 RightBorder { get; }

        bool IsInBorder(Vector3 position, Vector2 size);
        Vector3 GetNearestPositionInBorder(Vector3 position, Vector2 size);
        void Initialize();
        Vector3 GetMidPoint();
        Vector3 GetTopMidPoint();
        Vector3 GetDownMidPoint();
    }
}