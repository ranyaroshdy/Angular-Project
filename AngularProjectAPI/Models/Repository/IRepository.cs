using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models.Repository
{
    public interface IRepository<T, Tkey,TkeySec>
    {
        IEnumerable <T> GetAll();
        T GetById(Tkey id);
        T GetByName(TkeySec name);
        void Add(T Object);
        void Update(T Object);
        void Delete(T id);
    }
}
