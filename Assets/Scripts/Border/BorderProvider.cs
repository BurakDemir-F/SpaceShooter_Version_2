using UnityEngine;

namespace Border
{
    public class BorderProvider : MonoBehaviour,IBorderProvider
    {
        [SerializeField] private Transform _top;
        [SerializeField] private Transform _down;
        [SerializeField] private Transform _right;
        [SerializeField] private Transform _left;
        public Vector3 TopBorder => _top.position;
        public Vector3 DownBorder => _down.position;
        public Vector3 LeftBorder => _left.position;

        public Vector3 RightBorder => _right.position;

        private Boundaries _boundaries;
        
        public void Initialize()
        {
            _boundaries = new Boundaries();
        }

        public bool IsInBorder(Vector3 position, Vector2 size)
        {
            _boundaries.Calculate(position,size);
            var left = _boundaries.left;
            var right = _boundaries.right;
            var top = _boundaries.top;
            var down = _boundaries.down;

            return TopBorder.y >= top && DownBorder.y <= down && RightBorder.x >= right && LeftBorder.x <= left;
        }

        public Vector3 GetNearestPositionInBorder(Vector3 position, Vector2 size)
        {
            if (IsInBorder(position, size))
                return position;
            
            _boundaries.Calculate(position,size);
            var left = _boundaries.left;
            var right = _boundaries.right;
            var top = _boundaries.top;
            var down = _boundaries.down;

            var safePosition = position;

            if (LeftBorder.x > left)
                safePosition.x = LeftBorder.x + size.x/2f;

            if (RightBorder.x < right)
                safePosition.x = RightBorder.x - size.x/2f;

            if (TopBorder.y < top)
                safePosition.y = TopBorder.y  - size.y /2f;

            if (DownBorder.y > down)
                safePosition.y = DownBorder.y + size.y /2f;

            return safePosition;
        }

        public Vector3 GetMidPoint()
        {
            return new Vector3((RightBorder.x + LeftBorder.x) / 2f, (TopBorder.y + DownBorder.y) / 2f, 0f);
        }

        public Vector3 GetTopMidPoint()
        {
            return new Vector3((RightBorder.x + LeftBorder.x) / 2f, (TopBorder.y + GetMidPoint().y) / 2f, 0f);
        }

        public Vector3 GetDownMidPoint()
        {
            return new Vector3((RightBorder.x + LeftBorder.x) / 2f, (DownBorder.y + GetMidPoint().y) / 2f, 0f);
        }

        private struct Boundaries
        {
            public float left ;
            public float right;
            public float top ;
            public float down ;
            
            public void Calculate(Vector3 position, Vector2 size)
            {
                left = position.x - size.x / 2f;
                right = position.x + size.x / 2f;
                top = position.y + size.y / 2f;
                down = position.y - size.y / 2f;
            }
        }
    }
}