using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror; 
/// <summary>
/// 게임 입장시 호출하면 된다.
/// </summary>
public struct EnterGameMessage : NetworkMessage
{
} 
[Serializable]
public class ElpidaNetworkManager : NetworkManager 
{
    public static ElpidaNetworkManager Singleton { get; private set; } 
    public ElpidaNetworkMapManager NetworkMapManager = new ElpidaNetworkMapManager(); 
    private bool isHotMapsLoaded = false; 
    #region 서버 실행시 
    /// <summary>
    /// 서버가 실행된경우 (성공적으로 소켓이 열린경우)
    /// 서버용 씬을 로드하고 다양한 초기화 작업을 한다.
    /// </summary>
    public override void OnStartServer()
    {
        base.OnStartServer();
        // 콜백을 등록한다. 
        NetworkServer.RegisterHandler<EnterGameMessage>(HandleGameEnterMessage);
        // 서버가 실행되면
        InitializeWhenServerStartUpAsync().Forget();
    }

    [Server]
    async void HandleGameEnterMessage(NetworkConnectionToClient conn, EnterGameMessage gameMessage)
    {
        Debug.Log("HandleGameEnterMessage");
        if (!isHotMapsLoaded)
            Debug.Log(
                "[서버] 플레이어 입장 요청 메세지를 받았으나 아직 서버에서 맵을 로드하고있어 실제 플레이어 생성이 늦어지고 있습니다. 보통 이 메세지는 호스트 플레이어에게만 보여집니다.");
        await UniTask.WaitWhile(() => { return isHotMapsLoaded == false; });
 
        conn.Send(new SceneMessage()
        {
            sceneName = "map1",
            sceneOperation = SceneOperation.LoadAdditive,
        }); 
        
        // 플레이어 프리팹 생성
        GameObject playerObj = Instantiate(playerPrefab);
                   playerObj.name = $"${conn.connectionId}";
                   
        // 로컬플레이어 설정을 위해 커넥션 연결 이후 씬으로 이동한다.
        NetworkServer.AddPlayerForConnection(conn, playerObj); 
        SceneManager.MoveGameObjectToScene(playerObj, NetworkMapManager.GetScene(20000001));  
    }

    [Server]
    public async UniTaskVoid InitializeWhenServerStartUpAsync()
    {
        UGS.UnityGoogleSheet.LoadAllData();
        // 열려있던 모든 씬 언로드 (서버)
        await CleanUpBeforeGameStartAsync(); 
        // 등록된 맵들을 생성한다.
        await LoadServerSceneWhenStartUpAsync();
    }

    /// <summary>
    /// 서버 시작시 preInstanceScene 들을 서버에 미리 올려둔다.
    /// </summary>
    [Server]
    private async UniTask LoadServerSceneWhenStartUpAsync()
    { 
        foreach (var data in Map.Data.DataList)
            await NetworkMapManager.LoadMap(data.mapId);

        isHotMapsLoaded = true;
    }

    [Server]
    private async UniTask CleanUpBeforeGameStartAsync()
    {
        Debug.Log("게임을 실행하기 전 미리 오픈되어있던 장면들을 정리하고 있습니다.");
        for (int i = 0; i < SceneManager.loadedSceneCount - 1; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name == "Server") continue;

            var data = Map.Data.DataList.Find(x => x.sceneName == scene.name);
            if (data != null)
                await SceneManager.UnloadSceneAsync(scene);
        }
    }


    /// <summary>
    /// 여기서 다양한 초기화작업을 한다.
    /// 1. 서버에 연결한다
    /// 2. 게임에 필요한 기본 씬, 캐릭터들을 불러온다.
    /// 3. 서버에 연결한다. 
    /// </summary>
    public override void OnClientConnect()
    { 
        // 클라이언트에서 서버로 게임 접속을 요청,
        NetworkClient.Send(new EnterGameMessage()
        {
        });
    }
 

    //
    // public async UniTaskVoid OnServerAddPlayerDelayed(NetworkConnectionToClient conn)
    // {
    //     Debug.Log("[OnServerAddPlayerDelayed] 플레이어를 생성 하기전 서버가 초기화되기를 기다리고 있습니다.");
    //     await UniTask.WaitWhile(() => { return isHotMapsLoaded == false;});
    //     Debug.Log("[OnServerAddPlayerDelayed] 플레이어 생성중..");
    //     // 플레이어 생성
    //     
    //     // Send Scene message to client to additively load the game scene
    //     conn.Send(new SceneMessage { sceneName = "town", sceneOperation = SceneOperation.LoadAdditive }); 
    //     base.OnServerAddPlayer(conn);  
    //     SceneManager.MoveGameObjectToScene(conn.identity.gameObject, NetworkMapManager.GetSceneFirst(Map.Data.DataList[0].mapId));
    // }
    // public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    // {
    //     OnServerAddPlayerDelayed(conn).Forget();
    // } 
    /// <summary>
    /// Runs on both Server and Client
    /// Networking is NOT initialized when this fires
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        Singleton = this;
    }

    #endregion
}