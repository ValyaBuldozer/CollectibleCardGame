using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Controllers.Data
{
    public interface IDataRepositoryController<T>
    {
        T GetById(int id);
        void Add(T element);
        void AddNewItem(ref T element);
        void Remove(T element);
        void Remove(int id);
        void Edit(T elemnt, int id);
    }
}
