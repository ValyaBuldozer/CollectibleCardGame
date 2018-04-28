﻿using System;
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
        void Delete(T element);
        void Delete(int id);
        void Edit(T elemnt, int id);
    }
}