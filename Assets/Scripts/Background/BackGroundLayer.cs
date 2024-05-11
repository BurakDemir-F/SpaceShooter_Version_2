using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace Background
{
    public class BackGroundLayer : MonoBehaviour
    {
        [SerializeField] private Transform _screenStart;
        [SerializeField] private Transform _screenEnd;
        [SerializeField] private Transform _screenCenter;
        [SerializeField] private float _backgroundSpeed;

        [SerializeField] private List<BackgroundImage> _backgrounds;

        public void Construct()
        {
            foreach (var backgroundImage in _backgrounds)
            {
                backgroundImage.Construct();
                backgroundImage.OnPositionReached += OnImageReachedHandler;
            }

            SetInitialPositions();
            StartMoving();
        }

        public void Destruct()
        {
            foreach (var backgroundImage in _backgrounds)
            {
                backgroundImage.OnPositionReached -= OnImageReachedHandler;
                backgroundImage.Destruct();
            }
        }

        private void StartMoving()
        {
            foreach (var backgroundImage in _backgrounds)
            {
                backgroundImage.MoveToPosition(GetLeftEndPos(),_backgroundSpeed);
            }
        }
        
        private void OnImageReachedHandler(BackgroundImage image)
        {
            image.StopMovement();
            var rightEnd = GetRightEndPos();
            image.SetPosition(rightEnd.SetX(rightEnd.x + GetImageLength()/2f));
            image.MoveToPosition(GetLeftEndPos(),_backgroundSpeed);
        }
        
        private void SetInitialPositions()
        {
            var leftEnd = GetLeftEndPos();
            var imageLength = GetImageLength();

            for (var i = 0; i < _backgrounds.Count; i++)
            {
                var backgroundImage = _backgrounds[i];
                backgroundImage.SetPosition(leftEnd.SetX(leftEnd.x + i * imageLength));
            }
        }

        private Vector3 GetLeftEndPos()
        {
            var imageLength = GetImageLength();
            var halfImageLength = imageLength / 2f;
            var screenStartPos = _screenStart.position;
            return screenStartPos.SetX(screenStartPos.x - halfImageLength);
        }

        private Vector3 GetRightEndPos()
        {
            var image = _backgrounds.Aggregate((image1, image2) =>
                image1.CenterPosition.x > image2.CenterPosition.x ? image1 : image2);

            return image.EndPosition;
        }

        private float GetImageLength()
        {
            var firstImage = _backgrounds[0];
            return firstImage.Length;
        }
    }
}