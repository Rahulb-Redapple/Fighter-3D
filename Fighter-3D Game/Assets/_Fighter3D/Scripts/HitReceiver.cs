using UnityEngine;

namespace Fighting3D
{
    public class HitReceiver : MonoBehaviour
    {
        private HitReceiverHandler _handler;
        private int _actorId;

        internal void Init(HitReceiverHandler hitReceiverHandler, int actorId)
        {
            _handler = hitReceiverHandler;
            _actorId = actorId;
        }

        internal HitReceiverHandler GetHandler() { return _handler; }
        internal int GetActorId() { return _actorId;}
    }
}
