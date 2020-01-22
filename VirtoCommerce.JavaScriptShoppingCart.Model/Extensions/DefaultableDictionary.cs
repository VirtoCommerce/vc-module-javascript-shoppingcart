namespace VirtoCommerce.JavaScriptShoppingCart.Core.Extensions
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represent dictionary returning a default value if the key does not exist.
    /// </summary>
    /// <typeparam name="TKey">Generic type parameter for key.</typeparam>
    /// <typeparam name="TValue">Generic type parameter for value.</typeparam>
    public class DefaultableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;
        private readonly TValue _defaultValue;

        public DefaultableDictionary(IDictionary<TKey, TValue> dictionary, TValue defaultValue)
        {
            _dictionary = dictionary;
            _defaultValue = defaultValue;
        }

        public int Count
        {
            get { return _dictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return _dictionary.IsReadOnly; }
        }

        public ICollection<TKey> Keys
        {
            get { return _dictionary.Keys; }
        }

        public ICollection<TValue> Values
        {
            get
            {
                var values = new List<TValue>(_dictionary.Values) { _defaultValue };
                return values;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                if (!_dictionary.TryGetValue(key, out var result))
                {
                    result = _defaultValue;
                }

                return result;
            }

            set
            {
                _dictionary[key] = value;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _dictionary.Add(item);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Remove(item);
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
        }

        public bool Remove(TKey key)
        {
            return _dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (!_dictionary.TryGetValue(key, out value))
            {
                value = _defaultValue;
            }

            return true;
        }
    }
}
