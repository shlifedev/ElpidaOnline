 

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Elpida.Map
{
    [RequireComponent(typeof(Grid))]
    public class MapObject : MonoBehaviour
    {
        /// <summary>
        /// 최상위 그리드
        /// </summary>
        private Grid grid;

        /// <summary>
        /// 타일 맵
        /// </summary>
        private List<Tilemap> tileMaps;


        private bool isInitialized = false;

        public Grid Grid
        {
            get => grid;
            set => grid = value;
        }

        public List<Tilemap> TileMaps
        {
            get => tileMaps;
            set => tileMaps = value;
        }

        public void InitializeComponents()
        {
            if (isInitialized == false)
            {
                this.grid = GetComponent<Grid>();
                this.tileMaps = GetComponentsInChildren<Tilemap>().ToList();
            }
        }

        private void Awake()
        {
            this.grid = GetComponent<Grid>();
            this.tileMaps = GetComponentsInChildren<Tilemap>().ToList();
        }
    }

}