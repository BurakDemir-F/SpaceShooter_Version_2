using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    [System.Serializable]
    public class SerializableDictionary<TKey,TValue> : Dictionary<TKey,TValue>,ISerializationCallbackReceiver
    {
        [SerializeField] private bool isModifiable;

        [SerializeField]
        private List<TKey> keys = new ();
     
        [SerializeField]
        private List<TValue> values = new ();
        public void OnBeforeSerialize()
        {
            if(isModifiable) return;
            
            keys.Clear();
            values.Clear();
            foreach(KeyValuePair<TKey, TValue> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }
     
        public void OnAfterDeserialize()
        {
            this.Clear();

            for(int i = 0; i < Mathf.Min(keys.Count,values.Count); i++)
                this.Add(keys[i], values[i]);
        }
    }
}