using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

using org.Core.Collections;
using org.Core.Utilities;

namespace org.Core.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class RegistryEntryCollection<TEntry> : RegistryEntry, IListCollection<TEntry>, IReadOnlyListCollection<TEntry>, IEnumerable, IList, ICollection
        where TEntry : RegistryEntry
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public RegistryEntryCollection()
            : base()
        {
            //Synchronizing += new NotifySynchronizationEventHandler(_OnSynchronizing);

            foreach (var _childNodePath in GetChildNodePaths())
            {
                _innerRepository.Add(RegistryEntry.Initializer.Initialize(typeof(TEntry), _childNodePath));
            }
        }
        #endregion

        #region Events        
        //static event NotifySynchronizationEventHandler      Synchronizing       = null;

        //public event PropertyChangedEventHandler            PropertyChanged     = null;
        //public event NotifyCollectionChangedEventHandler    CollectionChanged   = null;
        #endregion

        #region Fields
        static object                                       _syncRoot           = new object();
        ListCollection<TEntry>                              _innerRepository    = new ListCollection<TEntry>(true);
        #endregion

        #region Indexers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TEntry this[int index]
        {
            get
            {
                return _innerRepository[index];
            }

            set
            {
                _innerRepository[index] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        object IList.this[int index]
        {
            get
            {
                return (_innerRepository as IList)[index];
            }

            set
            {
                (_innerRepository as IList)[index] = value;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                return _innerRepository.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return (0 == _innerRepository.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return _innerRepository.IsReadOnly;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFixedSize
        {
            get
            {
                return _innerRepository.IsFixedSize;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSynchronized
        {
            get
            {
                return _innerRepository.IsSynchronized;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object SyncRoot
        {
            get
            {
                return _innerRepository.SyncRoot;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TEntry> GetEnumerator()
        {
            return _innerRepository.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_innerRepository as IEnumerable).GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(TEntry item)
        {
            return _innerRepository.IndexOf(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>;
        public void Insert(int index, TEntry item)
        {
            _innerRepository.Insert(index, item);

            //RaisePropertyChanged("Count");
            //RaisePropertyChanged("Item[]");

            //RaiseCollectionChanged(NotifyCollectionChangedAction.Add, item, index);

            SetDirty();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            var _item = this[index];

            _innerRepository.RemoveAt(index);

            //RaisePropertyChanged("Count");
            //RaisePropertyChanged("Item[]");

            //RaiseCollectionChanged(NotifyCollectionChangedAction.Remove, _item, index);

            SetDirty();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(TEntry item)
        {
            _innerRepository.Add(item);

            //RaisePropertyChanged("Count");
            //RaisePropertyChanged("Item[]");

            //RaiseCollectionChanged(NotifyCollectionChangedAction.Add, item, _innerRepository.Count);

            SetDirty();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public void AddRange(IEnumerable<TEntry> collection)
        {
            foreach (var _item in collection)
            {
                _innerRepository.Add(_item);

                //RaisePropertyChanged("Count");
                //RaisePropertyChanged("Item[]");

                //RaiseCollectionChanged(NotifyCollectionChangedAction.Add, _item, _innerRepository.Count);
            }

            SetDirty();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _innerRepository.Clear();

            //RaisePropertyChanged("Count");
            //RaisePropertyChanged("Item[]");

            //RaiseCollectionChanged(NotifyCollectionChangedAction.Reset);

            SetDeleted();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(TEntry item)
        {
            return _innerRepository.Contains(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(TEntry[] array, int arrayIndex)
        {
            _innerRepository.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(TEntry item)
        {
            //var _index = IndexOf(item);

            if (_innerRepository.Remove(item))
            {
                item.Delete();

                //RaisePropertyChanged("Count");
                //RaisePropertyChanged("Item[]");

                //RaiseCollectionChanged(NotifyCollectionChangedAction.Remove, item, _index);

                SetDirty();

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(object value)
        {
            var _index = _innerRepository.Add(value);

            //RaisePropertyChanged("Count");
            //RaisePropertyChanged("Item[]");

            //RaiseCollectionChanged(NotifyCollectionChangedAction.Add, value, _index);

            SetDirty();

            return _index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(object value)
        {
            return _innerRepository.Contains(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(object value)
        {
            return _innerRepository.IndexOf(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(Array array, int index)
        {
            _innerRepository.CopyTo(array, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert(int index, object value)
        {
            _innerRepository.Insert(index, value);

            //RaisePropertyChanged("Count");
            //RaisePropertyChanged("Item[]");

            //RaiseCollectionChanged(NotifyCollectionChangedAction.Add, value, index);

            SetDirty();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Remove(object value)
        {
            //var _index = IndexOf(value);

            _innerRepository.Remove(value);

            ((TEntry)value).Delete();

            //RaisePropertyChanged("Count");
            //RaisePropertyChanged("Item[]");

            //RaiseCollectionChanged(NotifyCollectionChangedAction.Remove, value, _index);

            SetDirty();
        }

        /// <summary>
        /// 
        /// </summary>
        public sealed override void Delete()
        {
            _innerRepository.Clear();

            //RaisePropertyChanged("Count");
            //RaisePropertyChanged("Item[]");

            //RaiseCollectionChanged(NotifyCollectionChangedAction.Reset);

            base.Delete();
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal sealed override void SetDeleted()
        {
            base.SetDeleted();

            //RaiseSynchronizingEvent(new NotifySynchronizationEntry(_innerRepository, false, true));
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal sealed override void SetDirty()
        {
            base.SetDirty();

            //RaiseSynchronizingEvent(new NotifySynchronizationEntry(_innerRepository, true, false));
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="propertyName"></param>
        //void RaisePropertyChanged(string propertyName)
        //{
        //    RaisePropertyChanged(new PropertyChangedEventArgs(propertyName));
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="e"></param>
        //void RaisePropertyChanged(PropertyChangedEventArgs e)
        //{
        //    if (null != PropertyChanged)
        //    {
        //        PropertyChanged(this, e);
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="action"></param>
        //void RaiseCollectionChanged(NotifyCollectionChangedAction action)
        //{
        //    RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(action));
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="action"></param>
        ///// <param name="item"></param>
        ///// <param name="index"></param>
        //void RaiseCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
        //{
        //    RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="action"></param>
        ///// <param name="item"></param>
        ///// <param name="index"></param>
        ///// <param name="oldIndex"></param>
        //void RaiseCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
        //{
        //    RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="action"></param>
        ///// <param name="oldItem"></param>
        ///// <param name="newItem"></param>
        ///// <param name="index"></param>
        //void RaiseCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index)
        //{
        //    RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="e"></param>
        //void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
        //{
        //    if (null != CollectionChanged)
        //    {
        //        CollectionChanged(this, e);
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //void RaiseSynchronizingEvent(NotifySynchronizationEntry synchronizationEntry)
        //{
        //    if (null != Synchronizing)
        //    {
        //        var _arguments = new NotifySynchronizationEventArgs(synchronizationEntry);

        //        Synchronizing(this, _arguments);
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _OnSynchronizing(object sender, NotifySynchronizationEventArgs e)
        {
            if (this != sender)
            {
                if (e.SynchronizationEntry.IsDirty)
                {
                    var _enumerable = e.SynchronizationEntry.Item as IEnumerable<TEntry>;

                    if (null != _enumerable)
                    {
                        lock (_syncRoot)
                        { 
                            Clear();

                            AddRange(_enumerable);
                        }
                    }
                }
                else if (e.SynchronizationEntry.IsDeleted)
                {
                    lock (_syncRoot)
                    {
                        Clear();
                    }
                }
            }
        }
        #endregion
    }
}
