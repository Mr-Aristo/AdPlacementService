# 📢 AdPlacements Service

Bu proje, belirli bir **lokasyon** için uygun **reklam platformlarını** döndüren bir **.NET 8 Web API** servisidir.  
Veriler **dosyadan yüklenir**, **in-memory (Redis cache destekli)** saklanır ve çok hızlı sorgulama yapılması hedeflenir.  

Ayrıca sistem, **ELK Stack (Elasticsearch, Kibana, Logstash)** ile gözlemlenebilirlik ve logging desteğiyle birlikte gelir.  
Mimari **Clean Architecture + CQRS (MediatR)** üzerine inşa edilmiştir.

---

## 🚀 Özellikler
- 📂 **Upload API**: Reklam platformlarını dosyadan yükler (`/api/platforms/upload`).
- 🔍 **Query API**: Lokasyona göre uygun platformları listeler (`/api/platforms?location=/ru/msk`).
- 🧠 **In-memory index** (Redis destekli, hızlı lookup).
- 📊 **Health Checks**: `/health/live` ve `/health/ready`.
- 📝 **Serilog** entegrasyonu, Elasticsearch/Kibana üzerinden log inceleme.
- 🧪 **xUnit testleri** ile birim testi desteği.

---

## 🛠 Teknolojiler
- [.NET 8](https://dotnet.microsoft.com/)
- [Redis](https://redis.io/)
- [Elasticsearch](https://www.elastic.co/)
- [Kibana](https://www.elastic.co/kibana/)
- [Logstash](https://www.elastic.co/logstash/)
- [Serilog](https://serilog.net/)
- [CQRS + MediatR](https://github.com/jbogard/MediatR)

---

## 📦 Kurulum

### 1. Gereksinimler
- [Docker](https://www.docker.com/) (20.x+)
- [Docker Compose](https://docs.docker.com/compose/) (v2.x)
- İsteğe bağlı: [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

---

### 2. Docker ile Çalıştırma
Projeyi klonladıktan sonra:

```bash
git clone https://github.com/<your-username>/AdPlacements.git
cd AdPlacements
docker-compose up --build
