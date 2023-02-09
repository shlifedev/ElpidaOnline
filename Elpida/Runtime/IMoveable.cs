using UnityEngine;

namespace Elpida
{
    interface IEntity
    {

    }

    interface IMoveableEntity : IEntity
    {
        void Move(Vector2 dir);

        /// <summary>
        /// Move() 함수를 이용해서 길을 찾아간다.
        /// </summary>
        void TeleportTo(Vector2 point);
    }
}