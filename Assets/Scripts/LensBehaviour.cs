using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace GameStuff
{
    public class LensBehaviour : MonoBehaviour
    {

        public Color LensColor = Color.red;

        List<GameObject> currentlyProcessedObjects = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            List<GameObject> ObjectsOnScreen = GetAllObjectsOnScreen();
            ObjectsOnScreen.ForEach(go =>
            {
                // Uneccessary check but we can have it just for validation?
                var colourable = go.GetComponent<ColourableBehaviour>();
                if (colourable != null)
                {
                    // Check if if the object has not been processed yet
                    if (!currentlyProcessedObjects.Contains(go))
                    {
                        colourable.ProcessColouring(LensColor);
                        currentlyProcessedObjects.Add(go);
                    }
                }
            });

            // Remove the objects that are not on screen but were managed
            currentlyProcessedObjects.Where(go =>
            {
                var renderer = go.GetComponent<Renderer>();
                if (renderer == null)
                {
                    return false;
                }

                return !IsObjectOnCamera(go);
            }).ToList().ForEach(go =>
            {
                var colourable = go.GetComponent<ColourableBehaviour>();
                if (colourable != null)
                {
                    colourable.ResetColourBehaviour();
                }
                currentlyProcessedObjects.Remove(go);
            });
        }

        List<GameObject> GetAllObjectsOnScreen()
        {
            // Get all the objects that are visible to the main camera and has the tag "Colourable"
            return GameObject.FindGameObjectsWithTag("Colourable").Where(go =>
            {
                var renderer = go.GetComponent<Renderer>();
                if (renderer == null)
                {
                    return false;
                }
                return IsObjectOnCamera(go);
            }).ToList();
        }

        bool IsObjectOnCamera(GameObject go)
        {
            // Check if the point is between 0 and 1 and z is positive
            var screenPoint = Camera.main.WorldToViewportPoint(go.transform.position);
            return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1 && screenPoint.z > 0;

        }
    }
}
