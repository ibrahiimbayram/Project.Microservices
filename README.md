# Microservices mimarisi ile Docker ve Kafka

Microservices mimarisi ile geliştirmiş olduğum projede Docker ve Kafka teknolojileri kullanılarak
Order ve Product servisler geliştirildi.
Proje servislerin kafka üzerinden haberleşmesi ve ürün adet miktarının güncel kalmasını sağlamayı amaçlamaktadır.


![image](https://user-images.githubusercontent.com/47785669/174316822-3232d67b-ae7c-4109-b20e-c12303ae8d41.png)



# Kurulum

1. `Docker Desktop`'ı yükleyin .
2. Docker Compose yapılandırmasını alın

```
git clone https://github.com/ibrahiimbayram/Project.Microservices
```


3. Kurulum Ortamı

* Windows

```
cd Project.Microservices
docker-compose up -d
```


* Linux & MacOS

```
cd Project.Microservices
docker-compose up -d
```

4. Job.Producer(data producer) loglarını kontrol edin.

```
docker-compose logs Job.Producer
```

5. Sql Server'a bağlanın.

```
User=SA
Password=secret123new!
Port=1433 (1433 portunuzun kullanılmadığından emin olun)
```

6. Order ve Product tablolarını kontrol edin.
```
select * from Product

select * from Order
```

# Proje Görünümü ve Tanımlar

![image](https://user-images.githubusercontent.com/47785669/174255939-e8dc200c-547b-499a-92f6-208fe9d1bbe1.png)

* Job.Producer = Product ve Order tablolarına veri akışını sağlar.

* Job.Consumer.Api = Db'de migration oluşturur.Kafkayı dinler.

* Services.Order = Sipariş servisi crud işlemlerini ve Kafkaya veri akışını sağlar.

* Services.Product = Ürün servisi crud işlemlerini sağlar.

# Geliştirilmesi gereken noktalar

* Memory Cache eklenebilir.

* Integration ve Unit Testleri eklenebilir.

