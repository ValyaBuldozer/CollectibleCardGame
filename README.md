# CollectibleCardGame
Simple server-client application

 <h4>Требования к системе:</h4>

 <li>SQL Server 2017
 
 <li>.NET Framework 4.5.2

 <li>Visual Studio 2017

  Перед запуском соберите проект в Visual Studio 2017.
  <hr>
  Чтобы работала база данных(сохранялись данные пользователя) в серверной части приложения, требуется "обновить" базу данных. Для этого в Visual Studio перейдите в  Средства-Диспетчер пакетов NuGet-Консоль диспетчера пакетов, в открывшейся консоли пропишите update-database, нажмите Enter, готово. 
  <hr>

   Для начала игры требуется запустить Server.exe (для корректной работы требуется SQL Server 2017, т.к. без него программа будет работать без сохранения зарегистрированных пользователей и их данных при перезапуске сервера).
После того как сервер произведет свой запуск и настройку, будет выдано сообщение ("Enter command") о непосредственной готовности к запуску
Далее следует ввести "start", теперь сервер готов принимать клиентов.
<hr>

   Чтобы уже непосредственно поиграть в игру, требуется запустить CollectibleCardGame.
Для входа в игру необходимо зарегистрироваться в системе, указав логин и пароль.
При попадании в главное меню, нажмите на вкладу "Играть" в левом списке, далее выберите игровую фракцию и кликните по кнопке "Начать сражение".
<hr>

  ***Good Luck Have Fun!***
