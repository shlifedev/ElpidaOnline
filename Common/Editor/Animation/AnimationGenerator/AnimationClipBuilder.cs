 
using UnityEditor;
using UnityEngine;

namespace LifeDev.Editor
{ 
    /// <summary>
    /// 클립 생성기
    /// </summary>
    public class AnimationClipBuilder 
    { 
        private  AnimationClip m_clip; 
         
        public AnimationClipBuilder(AnimationClip clip, string clipName, int frameRate = 24)
        {
            this.m_clip = clip;
        }
        public AnimationClipBuilder(string clipName, int frameRate = 24)
        {
            this.m_clip = new AnimationClip();
        }

        public static AnimationClipBuilder Create(AnimationClip clip, string clipName, int frameRate)
        {
            return new(clip, clipName, frameRate);
        } 
        
        public static AnimationClipBuilder Create(string clipName, int frameRate)
        {
            return new(clipName, frameRate);
        } 

        
        /// <summary>
        /// 클립을 에셋 데이터베이스 (로컬)에 저장합니다.
        /// </summary> 
        public void Save(string directory)
        { 
            AssetDatabase.CreateAsset(m_clip, directory);
            AssetDatabase.Refresh(); 
        }
        
        /// <summary>
        /// 애니메이션 클립에 키프레임들을 적용합니다.
        /// </summary>
        /// <param name="keyframes">키프레임 정보</param> 
        /// <param name="inPath">클립의 transform간 패스정보입니다. 기본적으로 공백이며 만약 A 오브젝트안에있는 B 프로퍼티를 수정한다면 B</param> 
        public void SetKeyFrames(ObjectReferenceKeyframe[] keyframes, string inPath ="") 
        {
            var editorCurveBinding =
                EditorCurveBinding.PPtrCurve(inPath, typeof(SpriteRenderer), "m_Sprite");
            AnimationUtility.SetObjectReferenceCurve(m_clip, editorCurveBinding, keyframes);
        } 
    }
}