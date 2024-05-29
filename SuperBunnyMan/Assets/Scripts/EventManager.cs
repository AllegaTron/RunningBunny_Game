using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDEvent
{
    public static class EventManager
    {
        public static readonly PlayerEvents Player = new PlayerEvents();
        public static readonly NetworkEvents Network = new NetworkEvents();

        public class PlayerEvents
        {
            public UnityAction<Component, int> OnHealthChanged;
        }
        public class NetworkEvents
        {
            public UnityAction onConnect;
            public UnityAction onDisconnect;
        }
    }
}
