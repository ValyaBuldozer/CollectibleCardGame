using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Controllers.Data
{
    public interface IDataRepositoryController<T>
    {
        /// <summary>
        /// Получить элемент по индексу
        /// </summary>
        /// <param name="id">Индекс</param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Добавить элемент с указанным индексом
        /// </summary>
        /// <param name="item">Элемент</param>
        void Add(T item);

        /// <summary>
        /// Добавть элемент с присвоением индекса (переданнному элементу будет присвоен новый индекс)
        /// </summary>
        /// <param name="item">Элемент</param>
        void AddNewItem(T item);

        /// <summary>
        /// Удалить элемент из репозитория
        /// </summary>
        /// <param name="element">Элемент</param>
        void Remove(T element);

        /// <summary>
        /// Удалить элемент с указанным id из репозитория
        /// </summary>
        /// <param name="id">Индекс</param>
        void Remove(int id);

        /// <summary>
        /// Переприсвоить элемент с указанным индексом в репозитории
        /// </summary>
        /// <param name="elemnt">Элемент</param>
        /// <param name="id">Индекс</param>
        void Edit(T elemnt, int id);
    }
}
