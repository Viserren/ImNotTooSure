using UnityEngine;

namespace Utilities
{
    public static class CameraUtils
    {
        public static Vector3 ConvertToCameraSpace(Vector3 playerInput)
        {
            Camera camera = Camera.main;
            // Get the current y value of the vector
           // float currentYValue = playerInput.y;

            // Get the camera's forward and right vectors
            Vector3 cameraForward = camera.transform.forward;
            Vector3 cameraRight = camera.transform.right;

            // set the y values to 0 to ignore the y axis
            cameraForward.y = 0;
            cameraRight.y = 0;

            // Normalize the vectors
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 cameraForwardZProduct = playerInput.z * cameraForward;
            Vector3 cameraRightXProduct = playerInput.x * cameraRight;

            Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightXProduct;
            //vectorRotatedToCameraSpace.y = currentYValue;
            return vectorRotatedToCameraSpace;
        }
    }
}