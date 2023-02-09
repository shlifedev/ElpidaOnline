using Elpida.Map.Editor;
using Elpida.Map; 
using UnityEditor;
using UnityEngine; 

namespace Elpida.Map.Editor
{
    [CustomEditor(typeof(MapObject))]
    public class MapEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            // var sceneCameras = UnityEditor.SceneView.GetAllSceneCameras();
            // if (sceneCameras.Length == 0)
            //     return;
            // var workspaceCamera = sceneCameras[0]; 
            // var map = target as MapObject;
            // map.InitializeComponents(); 
            // var point = SceneViewUtils.GetMouseWorldPosition2D(workspaceCamera); 
            // Handles.DrawWireCube(Vector3Int.FloorToInt(point), Vector3.one);
        }
    }
}