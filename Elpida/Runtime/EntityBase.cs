using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Elpida
{
    public class EntityBase : NetworkBehaviour, IEntity
    {
        public int Index;

        protected virtual void Update()
        {
     
        }
    } 
}