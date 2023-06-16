using System.Collections.Generic;

namespace ViewLab10.Model
{
    public interface IObservable
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();        
    }
}
