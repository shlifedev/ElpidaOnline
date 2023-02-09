using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Game.Common.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace LifeDev.Editor
{ 
    public class ElpidaAnimationBuildProcess : IAnimationBuildProcess
    {
        class AnimationTexture
        {
            /// <summary>
            /// spread sheet root file info 
            /// </summary>
            public readonly FileInfo FileInfo;
            /// <summary>
            /// items
            /// </summary>
            public readonly Sprite[] Sprites; 

            public AnimationTexture(FileInfo fileInfo)
            {
                this.FileInfo = fileInfo;  
                this.Sprites = AssetDatabase.LoadAllAssetsAtPath(fileInfo.FullName.ToUnityRelativePath())
                    .OfType<Sprite>().ToArray(); 
            }
        }
        public enum Mode
        {
            All,
            Characters,
            Monster
        }

        private const string SavePath = "Assets/Game/Resource/PreBuilding/";
        private readonly string rootPath;

        /// <summary>
        /// Ex ) Unit/Characters/unit_character_1
        /// Ex2) Unit/Animals/animal_abcd_1
        /// </summary>
        /// <param name="entityFolder">유닛의 데이터들이 집합된 폴더</param>
        /// <returns></returns>
        private IEnumerable<AnimationTexture> LoadAllSprite(DirectoryInfo entityFolder)
        {  
            var textureDirectory = entityFolder?.GetDirectories("*", SearchOption.AllDirectories)
                ?.FirstOrDefault(x => x.Name == "Textures");
            var textures = textureDirectory.GetFiles("*.png");
            var loadedSprite = textures.Select(fileInfo => new AnimationTexture(fileInfo));  
            return loadedSprite;
        }


        public ElpidaAnimationBuildProcess(string rootPath)
        {
            this.rootPath = rootPath;
        }

        public void Build()
        {
            System.IO.DirectoryInfo di = new(rootPath);
            if (!di.Exists)
                throw new DirectoryNotFoundException(rootPath);
 
            var resources = LoadAllSprite(di); 
            
            foreach (var resource in resources)
            {
                var builder = AnimationClipBuilder.Create("test", 24);
                var ork = LifeDev.Editor.AnimationUtils.CreateKeysForSprites(resource.Sprites.ToArray(), 24); 
                builder.SetKeyFrames(ork, "Renderer");
                builder.Save($"{rootPath}/Test/{Path.GetFileName(resource.FileInfo.Name)}.anim");
            }
        }
    }
}