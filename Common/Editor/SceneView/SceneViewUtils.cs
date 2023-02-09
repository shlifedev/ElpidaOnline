using UnityEditor;
using UnityEngine;

namespace Elpida.Editor
{
    public static class SceneViewUtils
    {
        public static Vector3 GetMouseWorldPosition2D(Camera sceneCamera)
        {
            var point =  GetMouseWorldRay2D(sceneCamera).GetPoint(0);
            return point;
        }
        
        public static Ray GetMouseWorldRay2D(Camera sceneCamera)
        {
            var ev = Event.current;  
            var mousePosition = Event.current.mousePosition * EditorGUIUtility.pixelsPerPoint;
            mousePosition.y = sceneCamera.pixelHeight - mousePosition.y;
            Ray ray = sceneCamera.ScreenPointToRay(mousePosition);
            return ray;
        }
    }
}