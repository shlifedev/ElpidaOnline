using System;
using System.IO;
using UnityEngine;
using UnityEngine.WSA;

namespace Game.Common.Editor
{
    public static class EditorFileInfoExtension
    { 
        public static string ToUnityRelativePath(this FileInfo fi)
        {
            if (fi == null) throw new NullReferenceException(fi.FullName);
            return ToUnityRelativePath(fi.FullName);
        }
        public static string ToUnityRelativePath(this string abstractPath)
        {
            var dataPath = UnityEngine.Application.dataPath;
            
            // file path인경우 url style로 변경해주어야함.
            var uri = new System.Uri(abstractPath); 
            abstractPath = uri.AbsolutePath; 
            if (abstractPath.StartsWith(dataPath))
            {
                var unityRelativePath = "Assets" + abstractPath.Substring(dataPath.Length);
                return unityRelativePath;
            } 
            return abstractPath;
        }
    }
}