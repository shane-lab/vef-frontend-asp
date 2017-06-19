using MvcWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWeb.Hubs
{
    public class SignalRClientDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, HashSet<TValue>> _clientValues = new Dictionary<TKey, HashSet<TValue>>();

        public int Count
        {
            get
            {
                return _clientValues.Count;
            }
        }

        public IEnumerable<TKey> Clients
        {
            get
            {
                return _clientValues.Keys;
            }
        }

        public void UnsetClient(TKey key)
        {
            if (Clients.Where(x => key.Equals(x)).FirstOrDefault() != null)
            {
                _clientValues.Remove(key);
            }
        }

        public IEnumerable<TValue> GetValues(TKey key)
        {
            HashSet<TValue> values;
            if (_clientValues.TryGetValue(key, out values))
            {
                return values;
            }

            return Enumerable.Empty<TValue>();
        }

        public void Add(TKey key, TValue value)
        {
            lock (_clientValues)
            {
                HashSet<TValue> values;
                if (!_clientValues.TryGetValue(key, out values))
                {
                    values = new HashSet<TValue>();
                    _clientValues.Add(key, values);
                }

                lock (values)
                {
                    values.Add(value);
                }
            }
        }
        
        public void Remove(TKey key, TValue value)
        {
            lock (_clientValues)
            {
                HashSet<TValue> values;
                if (!_clientValues.TryGetValue(key, out values))
                {
                    return;
                }

                lock (values)
                {
                    values.Remove(value);

                    if (values.Count == 0)
                    {
                        _clientValues.Remove(key);
                    }
                }
            }
        }
    }
}