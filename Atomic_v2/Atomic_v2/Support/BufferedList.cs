using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atomic
{
    public class BufferedList<T> : List<T>
    {
        public List<T> AddBuffer = new List<T>();
        public List<T> RemoveBuffer = new List<T>();

        public void ApplyBuffers()
        {
            AddRange(AddBuffer);
            RemoveAll(InRemoveBuffer);

            AddBuffer.Clear();
            RemoveBuffer.Clear();
        }

        //Predicate match for RemoveAll
        private bool InRemoveBuffer(T obj)
        {
            return RemoveBuffer.Contains(obj);
        }
    }
}
