#  Siber Suç Farkındalık Ölçeği (CAS) Dijital Platformu

##  Proje Hakkında
Siber Suç Farkındalık Ölçeği (CAS) Dijital Platformu, bireylerin siber güvenlik farkındalığını ölçen ve Üretken Yapay Zeka (GenAI) entegrasyonu ile kişiselleştirilmiş gelişim raporları sunan uçtan uca (full-stack) bir web uygulamasıdır. Geleneksel anket sistemlerinden farklı olarak bu proje, sadece bir veri toplama aracı değil; insan kaynaklı siber risk haritasını çıkaran yapay zeka destekli proaktif bir analiz ve eğitim platformudur.

##  Akademik Temel
* Sistem, Doç. Dr. İbrahim Arpacı ve Ersin Ateş (2022) tarafından geliştirilen ve geçerliliği kanıtlanmış 41 maddelik Siber Suç Farkındalık Ölçeği (CAS) üzerine inşa edilmiştir.
* Kullanıcıların dijital risk farkındalığı; **Bilişim Sistemleri Suçları**, **Kişisel Veri Suçları** ile **Gizlilik ve Güvenlik** olmak üzere üç ana boyutta analiz edilmektedir.

##  Sistem Mimarisi ve Kullanılan Teknolojiler
* **Mimari:** Proje, kodun sürdürülebilirliği ve performansı için "Ayrık" (Decoupled) ve Çok Katmanlı (N-Tier) mimari prensiplerine uygun olarak geliştirilmiştir.
* **Backend:** Yüksek performans ve güvenli iş mantığı (Business Logic) yönetimi için ASP.NET Core 8.0 Web API kullanılmıştır.
* **Veritabanı:** MS SQL Server üzerinde Entity Framework Core ile Code-First yaklaşımı kullanılarak modellenmiş ve 41 maddelik akademik veri "Data Seeding" yöntemiyle sisteme aktarılmıştır.
* **Frontend:** React.js ile kullanıcı dostu, dinamik bir arayüz tasarlanmış; 41 soruluk test kullanıcıyı yormamak adına 5'erli sayfalama (pagination) ve ilerleme çubuğu ile entegre edilmiştir. Test sonuçlarının görselleştirilmesi için Chart.js kullanılarak Radar ve Sütun grafikleri oluşturulmuştur.

##  Öne Çıkan Mühendislik Çözümleri
* **Hibrit Analiz Motoru:** Yapay zekanın matematiksel hesaplamalarda hata (halüsinasyon) yapmasını önlemek amacıyla "Ters Madde" (Reverse Scoring) optimizasyonu ($6 - \text{Ham Puan}$ formülü) tamamen C# Backend servis katmanında işlenmektedir.
* **Yapay Zeka (AI) Entegrasyonu:** Backend'de hesaplanan sayısal boyut skorları ve kullanıcının sorulara verdiği detaylı yanıtlar, Prompt Engineering teknikleri ile OpenAI (GPT-4o mini) API'sine iletilmektedir. Yapay zeka, sisteme enjekte edilen NIST ve ISO 27001 siber güvenlik standartları bağlamında kullanıcının zayıf yönlerini bulup kişiye özel bir "Uzman Analiz Raporu" üretmektedir.
* **Güvenli Kimlik Doğrulama:** Sisteme yetkisiz erişimleri engellemek ve verileri korumak için JWT (JSON Web Token) tabanlı yetkilendirme altyapısı kurulmuştur.
* **Veritabanı Optimizasyonu:** Tablolar arası ilişkisel modellemede karşılaşılan "Multiple Cascade Paths" kilitlenme hatası, Fluent API üzerinde `DeleteBehavior.Restrict` kuralları kurgulanarak bizzat çözülmüştür.
* **Admin Dashboard:** Yöneticiler ve araştırmacılar için sistem genelindeki analizlerin Excel (CSV) formatında dışa aktarılabildiği, risk dağılımlarının canlı takip edilebildiği yetki tabanlı bir yönetim paneli kodlanmıştır.

## ⚠️ Kurulum ve Güvenlik Notu
* Açık kaynak paylaşım kuralları gereği, projeye ait `appsettings.json` dosyasında yer alan gerçek OpenAI API anahtarı ve JWT güvenlik token'ı repoya yüklenmemiştir. 
* Projeyi yerelinizde çalıştırmak için repositöry'de bulunan `appsettings.example.json` dosyasını kullanabilir ve kendi API anahtarlarınızı tanımlayabilirsiniz.
