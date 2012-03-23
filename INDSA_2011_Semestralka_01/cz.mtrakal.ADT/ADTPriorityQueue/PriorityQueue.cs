/*
 * This code implements priority queue which uses min-heap as underlying storage
 * http://www.codeproject.com/Articles/126751/Priority-queue-in-C-with-the-help-of-heap-data-str
 * Copyright (C) 2010 Alexey Kurakin
 * www.avk.name
 * alexey[ at ]kurakin.me
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace cz.mtrakal.ADT.ADTPriorityQueue {
    /// <summary>
    /// Priority queue based on binary heap,
    /// Elements with minimum priority dequeued first
    /// </summary>
    /// <typeparam name="TPriority">Type of priorities</typeparam>
    /// <typeparam name="TValue">Type of values</typeparam>
    public class PriorityQueue<TPriority, TValue, TKey> where TPriority : IComparable<TPriority> {
        class PQData : IEqualityComparer<PQData> {
            public TKey Key { get; set; }
            public TValue Value { get; set; }

            public bool Equals(PQData x, PQData y) {
                if (ReferenceEquals(x, y)) {
                    return true;
                }

                if ((x as PQData).Key.Equals((y as PQData).Key)) {
                    return true;
                }

                return false;
            }

            public int GetHashCode(PQData obj) {
                throw new NotImplementedException();
            }
        }
        private List<KeyValuePair<TPriority, PQData>> _baseHeap;
        private IComparer<TPriority> _comparer;

        Dictionary<TKey, KeyValuePair<TPriority, PQData>> dict;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of priority queue with default initial capacity and default priority comparer
        /// </summary>
        public PriorityQueue()
            : this(Comparer<TPriority>.Default) {
            dict = new Dictionary<TKey, KeyValuePair<TPriority, PQData>>();
        }

        /// <summary>
        /// Initializes a new instance of priority queue with specified initial capacity and default priority comparer
        /// </summary>
        /// <param name="capacity">initial capacity</param>
        public PriorityQueue(int capacity)
            : this(capacity, Comparer<TPriority>.Default) {
            dict = new Dictionary<TKey, KeyValuePair<TPriority, PQData>>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of priority queue with specified initial capacity and specified priority comparer
        /// </summary>
        /// <param name="capacity">initial capacity</param>
        /// <param name="comparer">priority comparer</param>
        public PriorityQueue(int capacity, IComparer<TPriority> comparer) {
            if (comparer == null)
                throw new ArgumentNullException();

            _baseHeap = new List<KeyValuePair<TPriority, PQData>>(capacity);
            dict = new Dictionary<TKey, KeyValuePair<TPriority, PQData>>(capacity);
            _comparer = comparer;
        }

        /// <summary>
        /// Initializes a new instance of priority queue with default initial capacity and specified priority comparer
        /// </summary>
        /// <param name="comparer">priority comparer</param>
        public PriorityQueue(IComparer<TPriority> comparer) {
            if (comparer == null)
                throw new ArgumentNullException();

            _baseHeap = new List<KeyValuePair<TPriority, PQData>>();
            _comparer = comparer;
        }

        #endregion

        #region Merging

        /// <summary>
        /// Merges two priority queues
        /// </summary>
        /// <param name="pq1">first priority queue</param>
        /// <param name="pq2">second priority queue</param>
        /// <returns>resultant priority queue</returns>
        /// <remarks>
        /// source priority queues must have equal comparers,
        /// otherwise <see cref="InvalidOperationException"/> will be thrown
        /// </remarks>
        public static PriorityQueue<TPriority, TValue, TKey> MergeQueues(PriorityQueue<TPriority, TValue, TKey> pq1, PriorityQueue<TPriority, TValue, TKey> pq2) {
            if (pq1 == null || pq2 == null)
                throw new ArgumentNullException();
            if (pq1._comparer != pq2._comparer)
                throw new InvalidOperationException("Priority queues to be merged must have equal comparers");
            return MergeQueues(pq1, pq2, pq1._comparer);
        }

        /// <summary>
        /// Merges two priority queues and sets specified comparer for resultant priority queue
        /// </summary>
        /// <param name="pq1">first priority queue</param>
        /// <param name="pq2">second priority queue</param>
        /// <param name="comparer">comparer for resultant priority queue</param>
        /// <returns>resultant priority queue</returns>
        public static PriorityQueue<TPriority, TValue, TKey> MergeQueues(PriorityQueue<TPriority, TValue, TKey> pq1, PriorityQueue<TPriority, TValue, TKey> pq2, IComparer<TPriority> comparer) {
            if (pq1 == null || pq2 == null || comparer == null)
                throw new ArgumentNullException();
            // merge data
            PriorityQueue<TPriority, TValue, TKey> result = new PriorityQueue<TPriority, TValue, TKey>(pq1.Count + pq2.Count, pq1._comparer);
            result._baseHeap.AddRange(pq1._baseHeap);
            result._baseHeap.AddRange(pq2._baseHeap);
            // heapify data
            for (int pos = result._baseHeap.Count / 2 - 1; pos >= 0; pos--)
                result.HeapifyFromBeginningToEnd(pos);

            return result;
        }

        #endregion

        #region Priority queue operations

        /// <summary>
        /// Enqueues element into priority queue
        /// </summary>
        /// <param name="priority">element priority</param>
        /// <param name="value">element value</param>
        public void Enqueue(TPriority priority, TValue value, TKey key) {
            Insert(priority, value, key);
        }

        /// <summary>
        /// Dequeues element with minimum priority and return its priority and value as <see cref="KeyValuePair{TPriority,TValue}"/> 
        /// </summary>
        /// <returns>priority and value of the dequeued element</returns>
        /// <remarks>
        /// Method throws <see cref="InvalidOperationException"/> if priority queue is empty
        /// </remarks>
        public KeyValuePair<TPriority, TValue> Dequeue() {
            if (!IsEmpty) {
                KeyValuePair<TPriority, TValue> result = new KeyValuePair<TPriority, TValue>(_baseHeap[0].Key, _baseHeap[0].Value.Value);
                DeleteRoot();
                return result;
            } else
                throw new InvalidOperationException("Priority queue is empty");
        }

        /// <summary>
        /// Dequeues element with minimum priority and return its value
        /// </summary>
        /// <returns>value of the dequeued element</returns>
        /// <remarks>
        /// Method throws <see cref="InvalidOperationException"/> if priority queue is empty
        /// </remarks>
        public TValue DequeueValue() {
            return Dequeue().Value;
        }

        /// <summary>
        /// Gets whether priority queue is empty
        /// </summary>
        public bool IsEmpty {
            get { return _baseHeap.Count == 0; }
        }

        #endregion

        #region Heap operations

        private void ExchangeElements(int pos1, int pos2) {
            KeyValuePair<TPriority, PQData> val = _baseHeap[pos1];
            _baseHeap[pos1] = _baseHeap[pos2];
            _baseHeap[pos2] = val;
        }

        private void Insert(TPriority priority, TValue value, TKey key) {
            KeyValuePair<TPriority, PQData> val = new KeyValuePair<TPriority, PQData>(priority, new PQData() { Key = key, Value = value });
            if (changePriority(key, priority, value)) {
                _baseHeap.Add(val);
                // heap[i] have children heap[2*i + 1] and heap[2*i + 2] and parent heap[(i-1)/ 2];
                // heapify after insert, from end to beginning
                HeapifyFromEndToBeginning(_baseHeap.Count - 1);
            }
        }


        private int HeapifyFromEndToBeginning(int pos) {
            if (pos >= _baseHeap.Count) return -1;

            while (pos > 0) {
                int parentPos = (pos - 1) / 2;
                if (_comparer.Compare(_baseHeap[parentPos].Key, _baseHeap[pos].Key) > 0) {
                    ExchangeElements(parentPos, pos);
                    pos = parentPos;
                } else break;
            }
            return pos;
        }


        private void DeleteRoot() {
            if (_baseHeap.Count <= 1) {
                _baseHeap.Clear();
                return;
            }

            _baseHeap[0] = _baseHeap[_baseHeap.Count - 1];
            _baseHeap.RemoveAt(_baseHeap.Count - 1);

            // heapify
            HeapifyFromBeginningToEnd(0);
        }

        private void HeapifyFromBeginningToEnd(int pos) {
            if (pos >= _baseHeap.Count) return;

            // heap[i] have children heap[2*i + 1] and heap[2*i + 2] and parent heap[(i-1)/ 2];

            while (true) {
                // on each iteration exchange element with its smallest child
                int smallest = pos;
                int left = 2 * pos + 1;
                int right = 2 * pos + 2;
                if (left < _baseHeap.Count && _comparer.Compare(_baseHeap[smallest].Key, _baseHeap[left].Key) > 0)
                    smallest = left;
                if (right < _baseHeap.Count && _comparer.Compare(_baseHeap[smallest].Key, _baseHeap[right].Key) > 0)
                    smallest = right;

                if (smallest != pos) {
                    ExchangeElements(smallest, pos);
                    pos = smallest;
                } else break;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TPriority, TValue>> implementation

        /// <summary>
        /// Enqueus element into priority queue
        /// </summary>
        /// <param name="item">element to add</param>
        public void Add(KeyValuePair<TPriority, TValue> item, TKey key) {
            Enqueue(item.Key, item.Value, key);
        }

        /// <summary>
        /// Clears the collection
        /// </summary>
        public void Clear() {
            _baseHeap.Clear();
        }

        /// <summary>
        /// Gets number of elements in the priority queue
        /// </summary>
        public int Count {
            get { return _baseHeap.Count; }
        }

        /// <summary>
        /// Copies the elements of the priority queue to an Array, starting at a particular Array index. 
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from the priority queue. The Array must have zero-based indexing. </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <remarks>
        /// It is not guaranteed that items will be copied in the sorted order.
        /// </remarks>
        //public void CopyTo(KeyValuePair<TPriority, PQData>[] array, int arrayIndex) {
        //    _baseHeap.CopyTo(array, arrayIndex);
        //}

        /// <summary>
        /// Gets a value indicating whether the collection is read-only. 
        /// </summary>
        /// <remarks>
        /// For priority queue this property returns <c>false</c>.
        /// </remarks>
        public bool IsReadOnly {
            get { return false; }
        }

        public bool Remove(TKey key) {
            return Remove(dict[key]);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the priority queue. 
        /// </summary>
        /// <param name="item">The object to remove from the ICollection <(Of <(T >)>). </param>
        /// <returns><c>true</c> if item was successfully removed from the priority queue.
        /// This method returns false if item is not found in the collection. </returns>
        private bool Remove(KeyValuePair<TPriority, PQData> item) {
            // find element in the collection and remove it
            int elementIdx = _baseHeap.IndexOf(item); // TODO: vyřešit a bude to asi OK! :)
            if (elementIdx < 0) return false;

            //remove element
            _baseHeap[elementIdx] = _baseHeap[_baseHeap.Count - 1];
            _baseHeap.RemoveAt(_baseHeap.Count - 1);
            dict.Remove(item.Value.Key);

            // heapify
            int newPos = HeapifyFromEndToBeginning(elementIdx);
            if (newPos == elementIdx)
                HeapifyFromBeginningToEnd(elementIdx);

            return true;
        }
        #endregion

        private bool changePriority(TKey key, TPriority priority, TValue value) {
            if (dict.ContainsKey(key)) {
                if ((dict[key].Key.CompareTo(priority)) == 1) { // TODO: zkontrolovat podmínku
                    dict.Remove(key);
                } else {
                    return false;
                }
            }
            dict.Add(key, new KeyValuePair<TPriority, PQData>(priority, new PQData() { Key = key, Value = value }));
            return true;
        }
    }
}
