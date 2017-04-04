#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.SessionState;
using System.Web;
using System.Reflection;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Framework.SessionControl
{
    public class CustomOutOfProcSessionStateStore : SessionStateStoreProviderBase
    {
        private SessionStateStoreProviderBase _sessionStateStore = null;

        public CustomOutOfProcSessionStateStore()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(HttpSessionState));
            _sessionStateStore = (SessionStateStoreProviderBase)assembly.CreateInstance("System.Web.SessionState.OutOfProcSessionStateStore");
        }
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            try
            {
                _sessionStateStore.Initialize(name, config);
                Type storeType = _sessionStateStore.GetType();
                string baseUriName = "s_uribase";
                string olfBaseUri = (string)storeType.InvokeMember(baseUriName,
                    BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Static,
                    null, null, new object[] { });
                string appId = "/LM/W3SVC/888/ROOT";
                string hashKey = (string)typeof(System.Web.Configuration.MachineKeySection).InvokeMember("HashAndBase64EncodeString",
                    BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Static,
                    null, null, new object[] { appId });
                string newBaseUri = appId + "(" + hashKey + ")/";
                storeType.InvokeMember(baseUriName,
                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                    null, null, new object[] { newBaseUri });
                base.Initialize(name, config);
            }
            catch (Exception exp)
            {
                LogHelper.WriteExceptionLog(exp);
            }
        }
        public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
        {
            return _sessionStateStore.CreateNewStoreData(context, timeout);
        }
        public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
        {
            _sessionStateStore.CreateUninitializedItem(context, id, timeout);
        }
        public override void Dispose()
        {
            _sessionStateStore.Dispose();
        }
        public override void EndRequest(HttpContext context)
        {
            _sessionStateStore.EndRequest(context);
        }
        public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {
            return _sessionStateStore.GetItem(context, id, out locked, out lockAge, out lockId, out actions);
        }
        public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
        {
            return _sessionStateStore.GetItemExclusive(context, id, out locked, out lockAge, out lockId, out actions);
        }
        public override void InitializeRequest(HttpContext context)
        {
            _sessionStateStore.InitializeRequest(context);
        }
        public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
        {
            _sessionStateStore.ReleaseItemExclusive(context, id, lockId);
        }
        public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
        {
            _sessionStateStore.RemoveItem(context, id, lockId, item);
        }
        public override void ResetItemTimeout(HttpContext context, string id)
        {
            _sessionStateStore.ResetItemTimeout(context, id);
        }
        public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
        {
            _sessionStateStore.SetAndReleaseItemExclusive(context, id, item, lockId, newItem);
        }
        public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
        {
            return _sessionStateStore.SetItemExpireCallback(expireCallback);
        }

    }
}
