
namespace FlighStatusApi.Domain.SeedWork
{
    public abstract class Entity 
    {
        int _id;
        public virtual int ID
        {
            get
            {
                return _id;
            }
            protected set
            {
                _id = value;
            }
        }
    }
}
