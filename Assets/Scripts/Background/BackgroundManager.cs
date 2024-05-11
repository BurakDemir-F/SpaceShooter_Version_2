using System;
using System.Collections.Generic;
using UnityEngine;

namespace Background
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private List<BackGroundLayer> _layers;

        private void Start()
        {
            foreach (var backGroundLayer in _layers)
                backGroundLayer.Construct();
        }

        private void OnDestroy()
        {
            foreach (var backGroundLayer in _layers)
                backGroundLayer.Destruct();
        }
    }
}