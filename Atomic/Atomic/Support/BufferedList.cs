using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atomic
{

    /*
     * BufferedList is just a normal List that incorperates the ability to buffer changes
     * that should be made - then apply them at a later time. This is useful for when
     * an object wants wanting to modify a list that is currently being iterated through.
     */

    class BufferedList<T> : List<T>
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
