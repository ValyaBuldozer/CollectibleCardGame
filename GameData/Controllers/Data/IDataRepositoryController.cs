using System.Collections.Generic;

namespace GameData.Controllers.Data
{
    public interface IDataRepositoryController<T>
    {
        /// <summary>
        ///     Получить элемент по индексу
        /// </summary>
        /// <param name="id">Индекс</param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        ///     Получить список элементов по их индексам
        /// </summary>
        /// <param name="idCollection">Список индексов</param>
        /// <returns></returns>
        IEnumerable<T> GetById(IEnumerable<int> idCollection);

        /// <summary>
        ///     Добавить элемент с указанным индексом
        /// </summary>
        /// <param name="item">Элемент</param>
        void Add(T item);

        /// <summary>
        ///     Добавть элемент с присвоением индекса (переданнному элементу будет присвоен новый индекс)
        /// </summary>
        /// <param name="item">Элемент</param>
        void AddNewItem(T item);

        /// <summary>
        ///     Удалить элемент из репозитория
        /// </summary>
        /// <param name="element">Элемент</param>
        void Remove(T element);

        /// <summary>
        ///     Удалить элемент с указанным id из репозитория
        /// </summary>
        /// <param name="id">Индекс</param>
        void Remove(int id);

        /// <summary>
        ///     Переприсвоить элемент с указанным индексом в репозитории
        /// </summary>
        /// <param name="elemnt">Элемент</param>
        /// <param name="id">Индекс</param>
        void Edit(T elemnt, int id);

        /// <summary>
        ///     Полностью очищает репозиторий
        /// </summary>
        void ClearRepository();

        /// <summary>
        ///     Получить коллекцию элементов
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetCollection();
    }
}