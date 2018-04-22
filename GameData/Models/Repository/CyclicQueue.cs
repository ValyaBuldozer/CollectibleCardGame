using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Models.Repository
{
    public class CyclicQueue<T>
    {
        private readonly Queue<T> _queue;

        public CyclicQueue(IEnumerable<T> data)
        {
            _queue = new Queue<T>(data);
        }

        public T Dequeue()
        {
            var element = _queue.Dequeue();
            _queue.Enqueue(element);
            return element;
        }

        public void Enqueue(T element)
        {
            _queue.Enqueue(element);
        }
    }
}
