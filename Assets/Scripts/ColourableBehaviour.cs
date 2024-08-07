using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStuff
{
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(Collider))]
    public class ColourableBehaviour : MonoBehaviour
    {
        public Color MainColor = Color.red;

        public float DeactivationThreshold = 20f;

        Collider _collider;
        Renderer _renderer;

        private void OnValidate()
        {
            var renderers = GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                renderer.material.color = MainColor;
            }
        }

        void Start()
        {
            if (_collider == null)
                _collider = GetComponent<Collider>();

            if (_renderer == null)
                _renderer = GetComponent<Renderer>();
        }

        void Update()
        {
        }

        public void ProcessColouring(Color lensColor)
        {
            // If the colour difference is less than the threshold, do the colour behaviour
            if (true)
            {
                DoColourBehaviour();
            }
            else
            {
                ResetColourBehaviour();
            }
        }

        public bool IsColourWithinThreshold(Color lensColor)
        {
            return true;
        }

        public void DoColourBehaviour()
        {
            Debug.Log("Matching Colour Detected");
            // disable the collider
            _collider.enabled = false;
            _renderer.forceRenderingOff = true;
        }

        public void ResetColourBehaviour()
        {
            Debug.Log("Deactivating");
            _collider.enabled = true;

            _renderer.forceRenderingOff = false;
        }
    }
}
