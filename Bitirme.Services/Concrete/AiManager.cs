using Bitirme.Services.Abstract;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bitirme.Services.Concrete
{
    public class AiManager : IAiService
    {
        private readonly ChatClient _client;

        public AiManager(IConfiguration configuration)
        {
            var apiKey = configuration["OpenAI:ApiKey"];
            var model = configuration["OpenAI:Model"] ?? "gpt-4o-mini";

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new System.Exception("OpenAI API Key bulunamadı! Lütfen appsettings.json dosyasını kontrol edin.");
            }

            _client = new ChatClient(model, apiKey);
        }

        public async Task<string> GenerateDetailedReportAsync(double scoreA, double scoreB, double scoreC, List<string> detailedResponses)
        {
            string systemPrompt = @"Sen üst düzey bir Siber Güvenlik Denetçisi ve Uyumluluk Uzmanısın (Cybersecurity Compliance Auditor). 
Kullanıcının Siber Güvenlik Farkındalık Ölçeği (CAS) test sonuçlarını inceleyerek ona kişiselleştirilmiş bir rapor sunacaksın.

DİKKAT - DEĞERLENDİRME OMURGASI (DEFAULT ASSESSMENT BACKBONE):
Raporunu hazırlarken tavsiyelerini ve risk analizlerini KESİNLİKLE aşağıdaki standartlara dayandırarak ve bu standartların ismini geçirerek (atıfta bulunarak) yapmalısın:
1. Bireysel (Individual) Boyut İçin: 'NIST CSF (Basitleştirilmiş)', 'CIS Basic Hygiene (Temel Hijyen)' ve temel 'Gizlilik (Privacy) Prensipleri'.
2. Kurumsal/KOBİ (SME/Enterprise) Etkileri İçin (Eğer kullanıcının hatası çalıştığı kurumu etkileyebilecekse): 'CIS Controls', 'NIST CSF 800-53' ve 'ISO 27001-27002' mantığı.

(Örneğin: 'NIST CSF Kimlik Doğrulama standartlarına göre...' veya 'CIS Temel Hijyen kuralları kapsamında...' şeklinde profesyonel atıflar yap.)

LÜTFEN RAPORUNU AŞAĞIDAKİ MARKDOWN BAŞLIKLARINI KESİNLİKLE KULLANARAK OLUŞTUR:
### Genel Değerlendirme ve NIST CSF Uyumluluğu
### Güçlü Yönler
### Kritik Risk Analizi ve ISO 27001 İhlalleri
### CIS Basic Hygiene ve Gelişim Önerileri

Cevapların net, akademik, profesyonel ve doğrudan kullanıcıya hitap edecek şekilde olmalıdır.";

            string userContent = $@"
                AŞAĞIDAKİ VERİLERİ ANALİZ ET:
                
                GENEL FARKINDALIK SKORLARI:
                - Bilişim Sistemleri Suçları: %{scoreA}
                - Kişisel Veri Suçları: %{scoreB}
                - Gizlilik ve Güvenlik: %{scoreC}

                DETAYLI SORU CEVAPLARI (1: En Düşük, 5: En Yüksek Farkındalık):
                {string.Join("\n", detailedResponses)}

                RAPOR FORMATI ŞÖYLE OLSUN:
                1. Genel Değerlendirme: Kullanıcının genel siber güvenlik duruşu.
                2. Kritik Risk Analizi: Düşük puan verilen (1, 2 veya 3) sorular üzerinden o sorulara ait soru soru nokta atışı uyarılar.
                3. Somut Öneriler: Kullanıcının güvenliğini artırması için yapması gereken 5 temel adım.
                4. Kapanış: Motivasyonel ve akademik bir bitiş cümlesi.

                Lütfen raporu Markdown formatında ve profesyonel bir Türkçe ile hazırla.";

            List<ChatMessage> messages = new List<ChatMessage>
            {
                new SystemChatMessage(systemPrompt),
                new UserChatMessage(userContent)
            };

            try
            {
                ChatCompletion completion = await _client.CompleteChatAsync(messages);
                return completion.Content[0].Text;
            }
            catch (System.Exception ex)
            {
                return $"Rapor oluşturulurken bir hata oluştu: {ex.ToString()}";
            }
        }
    }
}