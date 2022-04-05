Telefon Rehber Uygulaması 

Öncelikle [docker](https://docs.docker.com/engine/install/) yüklemelisiniz.
Daha sonra yönetici olarak çalıştırdığınız komut satırı üzerinde aşağıdaki komutları çalıştırınız.

-RabbitMQ için
docker run -d  --name phoneguiderabbitmq  -p 5672:5672 -p 15672:15672 rabbitmq:3-management

-Mongodb için
docker run --name phoneguidemongodb -d -p 27017:27017 mongo

-Postgres için
docker run --name phoneguidepostgres -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=123456 -p 5432:5432 -d postgres

Docker üzerinde yapılan kurulumlar tamamlandıktan sonra son adım olarak solution üzerinde gerekli ayarlar yapılmalıdır.

Bu komutları başarılı bir şekilde çalıştırdıktan sonra Visual Studio üzerinde açtığınız solution üzerinden başlangıç projelerini ayarlamanız gerekmektedir.
Bunun solution'a sağ tıklayıp "Set Startup Projects" seçeneği ile açılan ekranda "Multiple Startup Projects" seçiminin altındaki listeden "PhoneGuide.Shared" projesi hariç diğerlerini "Start" olarak belirtmeniz gerekmektedir. Bu adımdan sonra uygulama ayağa kalkmaya hazır hale gelmiştir.


- .NET 5.0
- Ocelot (API Gateway)
- POSTGRESQL (Contact Data)
- RABBITMQ (Create Report)
- MONGODB (Rapor Data)
- ASPNET CORE MVC
- ASPNET CORE WEB API
