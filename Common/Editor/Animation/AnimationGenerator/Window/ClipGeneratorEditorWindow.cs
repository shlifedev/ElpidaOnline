using System; 
using System.Collections.Generic;
using System.IO; 
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

/// <summary>
/// 1. 리소스가 가 어느 경로에 있고 
/// 2. 리소스를 어떻게 불러와야 하고 (로더)
/// 3. 이걸로 클립을 '어떤 전략으로' 만드는지 (빌더)
/// </summary>
namespace LifeDev.Editor
{
   public class ClipGenerator : EditorWindow
   {
      private UnityEngine.Object m_targetDirectory;
      private string m_targetDirectoryPath;
      public List<IAnimationBuildProcess> process = new List<IAnimationBuildProcess>(); 

      [MenuItem("LifeDev/AnimationClipGenerator")]
      static void Init()
      {
         ClipGenerator window = (ClipGenerator)EditorWindow.GetWindow(typeof(ClipGenerator));
         window.Show();
      }


      private void CreateGUI()
      { 
      }

      private void OnGUI()
      {
         m_targetDirectory = EditorGUILayout.ObjectField(m_targetDirectory, typeof(Object));
         m_targetDirectoryPath = AssetDatabase.GetAssetPath(m_targetDirectory);
         if (GUILayout.Button("Build"))
         {
            this.process.Add(new ElpidaAnimationBuildProcess(m_targetDirectoryPath));
            DoBuild();
            this.process.Clear();
         }
         // EditorGUILayout.Popup("Label", selected, options); 
      }

      public void DoBuild()
      {
         DirectoryInfo dir = new(m_targetDirectoryPath);
         foreach (var build in process)
            build.Build();
      }
   }

}