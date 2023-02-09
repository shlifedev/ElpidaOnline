using System;
using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Elpida
{
    public class   Idea : MonoBehaviour
    {
        public int projectileCount = 8;


        private void OnGUI()
        {
            if (GUILayout.Button("Do!"))
            {
                StartCoroutine(StartSkill());
            }
        }

        public IEnumerator StartSkill()
        {
            GameObject[] projectiles = new GameObject[projectileCount];
            Vector2[] points = new Vector2[projectileCount];
            Vector2 target = new Vector2(10, 10);
            int xRange = 4;
            int yRange = 2;
            for (int i = 0; i < projectileCount; i++)
            {
                var rX = Random.Range((float)-xRange, xRange);
                var rY = Random.Range((float)-yRange, yRange);
                points[i] = new Vector2(rX, rY);
                projectiles[i] = new GameObject();
                
                //디버깅용
                var visualDebuggingObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                visualDebuggingObject.transform.localScale = Vector3.one * 0.1f;
                visualDebuggingObject.name = "debug";
                visualDebuggingObject.transform.SetParent(projectiles[i].transform);
            } 
            yield return WaitProjectileMovePosition(projectiles, points);
            yield return WaitHit(projectiles, target);
        }

        public IEnumerator WaitProjectileMovePosition(GameObject[] projectiles, Vector2[] points)
        {
            float t = 0; 
            while (t < 1)
            {
                t += Time.deltaTime;
                if (t > 1) 
                    t = 1;
                for (int i = 0; i < projectiles.Length; i++) 
                    projectiles[i].transform.position = Vector3.Lerp(projectiles[i].transform.position, points[i], t); 
                yield return null;
            } 
        } 
        public IEnumerator WaitHit(GameObject[] projectiles, Vector2 target)
        {
            // 0.5초 대기
            yield return new WaitForSeconds(0.5f);
            float t = 0; 
            while (t < 1)
            {
                t += Time.deltaTime;
                if (t > 1) 
                    t = 1;
                for (int i = 0; i < projectiles.Length; i++) 
                    projectiles[i].transform.position = Vector3.Lerp(projectiles[i].transform.position, target, t); 
                yield return null;
            } 
        } 
    }
}