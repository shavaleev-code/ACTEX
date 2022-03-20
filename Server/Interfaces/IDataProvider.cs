using Server.Model;

namespace Server.Interfaces
{
    interface IDataProvider
    {
        /// <summary>
        /// Обращается к источнику данных, запрашивает список пользователей
        /// </summary>
        /// <returns>Возвращает всех пользователей</returns>
        List<User> GetUsers();
        /// <summary>
        /// Обращается к источнику данных, запрашивает список пользователей, подходящих по фильтру
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Возвращает всех пользователей, у которых в имя содержит name</returns>
        List<User> GetUsersWithName(string name);
        /// <summary>
        /// Обращается к источнику данных, запрашивает список пользователей, подходящих по фильтру
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Возвращает всех пользователей с типом type</returns>
        List<User> GetUsersWithType(int type);
        /// <summary>
        /// Обращается к источнику данных, запрашивает список пользователей, подходящих по фильтру
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>Возвращает всех пользователей, у которых дата последнего визита находится между from и to</returns>
        List<User> GetUsersWithDate(DateTime from, DateTime to);
        /// <summary>
        /// Обращается к источнику данных, запрашивает список типов пользователей
        /// </summary>
        /// <returns>Возвращает все типы пользователей</returns>
        List<UserType> GetUserTypes();
        /// <summary>
        /// Обращается к источнику данных, запрашивает данные пользователя с логином и паролем 
        /// </summary>
        /// <returns>Возвращает пользователя</returns>
        User GetUser(string login, string password);
        void CreateUser(string login, string password, string name);
        void EditUser(int id, string login, string password, string name);
        void DeleteUser(int id);
    }
}
