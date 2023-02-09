namespace Eldpia.Network.Messages
{
    public struct MapEnterMessage
    {
        public int mapId;
    }

    /// <summary>
    /// 맵 이동 요청
    /// </summary>
    public struct MoveMap
    {
        public int mapId;
        public int instanceId;
    }
}