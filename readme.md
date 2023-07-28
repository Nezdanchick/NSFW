# NSFW
![icon](icon.png)

### English
A simple wrapper over .net sockets.
Provides a User class that has server and client fields. When using the client box, you can connect to the server, and the server box starts listening for incoming connections.

Client:
> User.Current.Client.Connect("192.168.0.####:port");  // connect to "local_address:port"

Server:
> User.Current.Server.Start();       // start server with port of system preference   
  User.Current.Server.Start(port);   // start server with specific port   
  User.Current.Server.Listen();      // listen to one incoming connection   
  User.Current.Server.ListenAsync(); // listen every connection in other thread   

Also, each of these fields has an OnConnect event.
On the client, it happens after connection
On the server, it handles every connection

### Русский
Простая обертка над .net сокетами.
Предоставляет класс User, имеющий поля сервер и клиент. При использовании поля клиент можно подключиться к серверу, а полем сервер начать прослушивать входящие подключения.

Клиент:
> User.Current.Client.Connect("192.168.0.####:port"); // подключиться по локальной сети к "адрес:порт"

Сервер:
> User.Current.Server.Start();       // запустить сервер с портом по предпочтению системы   
  User.Current.Server.Start(port);   // запустить сервер с установленным портом   
  User.Current.Server.Listen();      // слушать входящее подключение   
  User.Current.Server.ListenAsync(); // слушать все подключения в другом потоке   

Также каждое из этих полей имеет событие OnConnect.
На клиенте оно происходит после подключения
На сервере оно отрабатывает каждое подключение
