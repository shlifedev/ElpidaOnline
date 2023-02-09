using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LifeDev.Editor
{
    public class AnimationUtils
    {
        /// <summary>
        /// 오브젝트 참조 키프레임 데이터를 생성
        /// </summary>
        /// <param name="sprites">바인딩 할 스프라이트 배열</param>
        /// <param name="samplesPerSecond">샘플링 시간단위</param>
        /// <returns></returns>
        public static ObjectReferenceKeyframe[] CreateKeysForSprites(Sprite[] sprites, int samplesPerSecond)
        {
            List<ObjectReferenceKeyframe> keys = new List<ObjectReferenceKeyframe>();
            float timePerFrame = 1.0f / samplesPerSecond;
            float currentTime = 0.0f;
            foreach (Sprite sprite in sprites)
            {
                ObjectReferenceKeyframe keyframe = new ObjectReferenceKeyframe();
                keyframe.time = currentTime;
                keyframe.value = sprite;
                keys.Add(keyframe); 
                currentTime += timePerFrame;
            } 
            return keys.ToArray();
        }
 
    }
}