using System.Collections.Generic;
using EntytiModel;

namespace DAL
{
    public class ManagerRepository : IModelRepository<Manager>
    {
        public void Add(Manager item)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Manager item)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Manager item)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Manager> Items { get; private set; }
        public void SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}