# NSFW
![icon](icon.png)

#### Examples | Примеры
[LAN Chat | Чат по локальной сети](https://github.com/Nezdanchick/NetworkChat)

### English
A simple wrapper over .net sockets.
Provides a User class that has server and client fields. When using the client box, you can connect to the server, and the server box starts listening for incoming connections.

Client:
> User.Client.Connect("192.168.0.####:port");  // connect to "local_address:port"

Server:
> User.Server.Start();       // start server with port of system preference   
  User.Server.Start(port);   // start server with specific port   
  User.Server.Listen();      // listen to one incoming connection   
  User.Server.ListenAsync(); // listen every connection in other thread   

The client and server have the following methods for exchanging data:
> void Send<T>(T? data);
   T? Receive<T>();
   void Send(byte[]? data);
   byte[]? receive();

### Русский
Простая обертка над .net сокетами.
Предоставляет класс User, имеющий поля сервер и клиент. При использовании поля клиент можно подключиться к серверу, а полем сервер начать прослушивать входящие подключения.

Клиент:
> User.Client.Connect("192.168.0.####:port"); // подключиться по локальной сети к "адрес:порт"

Сервер:
> User.Server.Start();       // запустить сервер с портом по предпочтению системы   
  User.Server.Start(port);   // запустить сервер с установленным портом   
  User.Server.Listen();      // слушать входящее подключение   
  User.Server.ListenAsync(); // слушать все подключения в другом потоке   

Клиент и сервер имеют следующие методы для обмена данными:
> void Send<T>(T? data);   
  T? Receive<T>();   
  void Send(byte[]? data);   
  byte[]? Receive();   
  
