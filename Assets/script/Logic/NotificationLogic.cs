using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace stealFight {

    public class NotificationLogic : BaseLogic {
        private static NotificationLogic _instance;

        public static NotificationLogic Instance {
            get { return _instance ?? (_instance = new NotificationLogic()); }
        }

        struct RegisterInfo {
            public NotificationEvent Event;
            public Action<NotificationEvent, System.Object[]> Callback;
            public WeakReference Caller;

            public RegisterInfo(NotificationEvent _event,
                Action<NotificationEvent, System.Object[]> callback,
                WeakReference caller) {
                Event = _event;
                Callback = callback;
                Caller = caller;
            }
        }

        private Dictionary<NotificationEvent, Dictionary<int, RegisterInfo>> _eventGroups =
            new Dictionary<NotificationEvent, Dictionary<int, RegisterInfo>>();

        public override void Init() {
        }

        public void Register(NotificationEvent @event, Action<NotificationEvent, System.Object[]> callback,
            System.Object caller) {
            Dictionary<int, RegisterInfo> eventGroup;
            if (!_eventGroups.ContainsKey(@event)) {
                eventGroup = new Dictionary<int, RegisterInfo>();
                _eventGroups[@event] = eventGroup;
            } else {
                eventGroup = _eventGroups[@event];
            }

            var code = GetEventHash(callback, caller);
            eventGroup[code] = new RegisterInfo(@event, callback, new WeakReference(caller));
        }

        public void Emit(NotificationEvent @event, params System.Object[] data) {
            if (!_eventGroups.ContainsKey(@event)) {
                return;
            }

            var eventGroup = _eventGroups[@event];

            foreach (var kv in eventGroup) {
                var callback = kv.Value.Callback;
                if (callback != null && kv.Value.Caller.IsAlive) {
                    callback.Invoke(kv.Value.Event, data);
                }
            }
        }

        private int GetEventHash(Action<NotificationEvent, System.Object[]> callback, System.Object caller) {
            return caller.GetHashCode();
        }
    }

    public enum NotificationEvent {
        Login,
        DirControllStart,
        DirControllEnd,
        DirControllInfo
    }

}