using UnityEngine;
using UnityEngine.Tilemaps;

namespace Elpida.Map.Tile
{
    public class ElpidaTile : TileBase
    {
        /// <summary>
        /// 씬이 실행될때 호출된다고 한다.
        /// </summary> 
        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            return base.StartUp(position, tilemap, go);
        }
 
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        { 
            base.GetTileData(position, tilemap, ref tileData);
        } 
    }
}
