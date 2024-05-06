using UnityEngine;

namespace Movement
{
    public class SizeProvider : MonoBehaviour, ISizeProvider
    {
        [SerializeField] private Vector2 _size;
        public Vector2 Size => _size;
    }
}