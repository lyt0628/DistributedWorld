




using QS.Api.Control.Domain;
using QS.Api.Control.Error;
using QS.Control.Domain;
using QS.GameLib.Rx.Relay;
using QS.GameLib.Util;
using System;
using System.Collections.Generic;

namespace QS.Control.Service
{
    class CharaTranslationProxyDataSource : ICharaTranslationProxyDataSource_tag
    {
        readonly Dictionary<string, CharaTranslationProxy> points = new();
        readonly Dictionary<string, Relay<ICharaTranslationProxy>> relays = new();

        public ICharaTranslationSnapshot Create()
        {
            return new CharaTranslationSnapshot();
        }

        public string New(Relay<ICharaTranslationSnapshot> data)
        {
            var uuid = MathUtil.UUID();
            var p = new CharaTranslationProxy(uuid);
            points[uuid] = p;
            var r = data.Map<ICharaTranslationProxy>((d) =>
            {
                var point = points[uuid];
                point.UpdateSnapshot(d);
                return point;
            });
            relays[uuid] = r;

            return uuid;
        }

        public void Remove(string uuid)
        {
            points.Remove(uuid);
            relays.Remove(uuid);
        }

        public Relay<ICharaTranslationProxy> Get(string uuid) 
        {
            try
            {
                return relays[uuid];
            }
            catch (Exception)
            {
                throw new ProxyNotFoundException(uuid, typeof(ICharaTranslationSnapshot).FullName);
            }
        }
    }
}