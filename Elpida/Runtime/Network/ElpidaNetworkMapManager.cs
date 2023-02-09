using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Eldpia.Network.Messages;
using Map; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElpidaNetworkMapManager
{ 
    /// <summary>
    /// 한 맵이 여러개 존재할 수 있는 상황도 있다. 
    /// 예를들면 인스턴스 던전 
    /// </summary>
    private Dictionary<int, List<Scene>> instanceMap = new Dictionary<int, List<Scene>>(); 
    public Scene GetScene(int index)
    { 
        return  instanceMap[index].FirstOrDefault(); 
    }
    /// <summary>
    /// 유저에게 직접 맵 접속 요청이 온 경우
    /// </summary>
    public void OnUserMapEnter(MapEnterMessage message)
    {
        Debug.Log("");
    }
    
    /// <summary>
    /// 씬을 불러온 후 데이터에 가지고 있어야   한다.
    /// </summary>
    /// <param name="index"></param>
    public async UniTask LoadMap(int index)
    {
        var map = Data.DataMap.FirstOrDefault(x => x.Key == index).Value;
        if (map != null)
        {
            var mapData = Map.Data.DataMap.FirstOrDefault(x => x.Key == index);

            // 씬을 불러온다. 
            await SceneManager.LoadSceneAsync(mapData.Value.sceneName,
                new LoadSceneParameters
                    { loadSceneMode = LoadSceneMode.Additive, localPhysicsMode = LocalPhysicsMode.Physics2D }); 
            var scene = SceneManager.GetSceneAt(SceneManager.sceneCount-1);
            if(instanceMap.ContainsKey(index) == false)
                instanceMap.Add(index, new List<Scene>());  
            instanceMap[index].Add(scene);
            
            Debug.Log($"[Load Map] Complete, {index}, and {scene.name}(${instanceMap[index].Count-1})");
        } 
    }
}