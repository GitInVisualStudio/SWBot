using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummonersWarBot
{
    public class Bot
    {
        private Queue<Func<Bitmap, bool>> listener;
        private bool finished;

        public Bot()
        {
            listener = new Queue<Func<Bitmap, bool>>();
            finished = false;
        }

        public void AddListener(Func<Bitmap, bool> action)
        {
            listener.Enqueue(action);
        }

        public virtual void OnTick(Bitmap screen)
        {
            if (listener.Peek()(screen))
                listener.Dequeue();
            if(listener.Count <= 0)
            {
                finished = true;
            }
        }
    }
}
