using System.Collections;
using System.Collections.Generic;

namespace BiliLiveLotteryPlugin
{
    public class LimitedQueue<T>:Queue<T>
    {
        int limit = 5;
        public LimitedQueue(int Limited)
        {
            limit = Limited;
        }

        public LimitedQueue()
        {
            
        }

        public new void Enqueue(T item)
        {
            base.Enqueue(item);
            if (Count > limit)
            {
                Dequeue();
            }
        }
    }
}