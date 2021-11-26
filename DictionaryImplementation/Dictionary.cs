using System;
using System.Collections;
using System.Collections.Generic;


namespace DictionaryImplementation
{
    class _Dictionary<TKey, TValue>
    {
        private struct Entry
        {
            public int hashCode;
            public int next;
            public TKey key;
            public TValue value;
        }
        private int[] _buckets;
        private Entry[] _entries;
        private int _version;
        private int _count;
        private int _freeList;
        private int _freeCount;
        private IEqualityComparer<TKey> _comparer;
        public _Dictionary() : this(0, null) { }

        public _Dictionary(int capacity) : this(capacity, null) { }
        public _Dictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            if (capacity < 0) throw new Exception();
            if (capacity > 0) Initialize(capacity);
            _comparer = comparer ?? EqualityComparer<TKey>.Default;

        }

        private void Initialize(int capacity)
        {
            int size = HashHelpers.GetPrime(capacity);
            _buckets = new int[size];
            for (int i = 0; i < _buckets.Length; i++) _buckets[i] = -1;
            _entries = new Entry[size];
            _freeList = -1;
        }
        public TValue this[TKey key]
        {
            get
            {
                int i = FindEntry(key);
                if (i >= 0)
                {
                    return _entries[i].value;
                }
                throw new Exception();
                return default(TValue);
            }
            set
            {
                Insert(key, value, false);
            }
        }
        
        private int FindEntry(TKey key)
        {
            if (key == null)
            {
                throw new Exception();
            }

            if (_buckets != null)
            {
                int hashCode = _comparer.GetHashCode(key) & 0x7FFFFFFF;
                for (int i = _buckets[hashCode % _buckets.Length]; i >= 0; i = _entries[i].next)
                {
                    if (_entries[i].hashCode == hashCode && _comparer.Equals(_entries[i].key, key)) return i;
                }
            }
            return -1;
        }
        private void Insert(TKey key, TValue value, bool add)
        {

            if (key == null)
            {
                throw new Exception();
            }

            if (_buckets == null) Initialize(0);
            int hashCode = _comparer.GetHashCode(key) & 0x7FFFFFFF;
            int targetBucket = hashCode % _buckets.Length;

            int collisionCount = 0;

            for (int i = _buckets[targetBucket]; i >= 0; i = _entries[i].next)
            {
                if (_entries[i].hashCode == hashCode && _comparer.Equals(_entries[i].key, key))
                {
                    if (add)
                    {
                        throw new Exception();
                    }
                    _entries[i].value = value;
                    _version++;
                    return;
                }
            }

            collisionCount++;

            int index;
            if (_freeCount > 0)
            {
                index = _freeList;
                _freeList = _entries[index].next;
                _freeCount--;
            }
            else
            {
                if (_count == _entries.Length)
                {
                    Resize();
                    targetBucket = hashCode % _buckets.Length;
                }
                index = _count;
                _count++;
            }

            _entries[index].hashCode = hashCode;
            _entries[index].next = _buckets[targetBucket];
            _entries[index].key = key;
            _entries[index].value = value;
            _buckets[targetBucket] = index;
            _version++;

        }
        private void Resize()
        {
            Resize(HashHelpers.ExpandPrime(_count), false);
        }
        private void Resize(int newSize, bool forceNewHashCodes)
        {
            int[] newBuckets = new int[newSize];
            for (int i = 0; i < newBuckets.Length; i++) newBuckets[i] = -1;
            Entry[] newEntries = new Entry[newSize];
            Array.Copy(_entries, 0, newEntries, 0, _count);
            if (forceNewHashCodes)
            {
                for (int i = 0; i < _count; i++)
                {
                    if (newEntries[i].hashCode != -1)
                    {
                        newEntries[i].hashCode = (_comparer.GetHashCode(newEntries[i].key) & 0x7FFFFFFF);
                    }
                }
            }
            for (int i = 0; i < _count; i++)
            {
                if (newEntries[i].hashCode >= 0)
                {
                    int bucket = newEntries[i].hashCode % newSize;
                    newEntries[i].next = newBuckets[bucket];
                    newBuckets[bucket] = i;
                }
            }
            _buckets = newBuckets;
            _entries = newEntries;
        }
        public void Add(TKey key, TValue value)
        {
            Insert(key, value, true);
        }
      

    }
}
