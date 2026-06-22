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
            string systemPrompt = @"Sen profesyonel bir Siber Güvenlik Farkındalık Uzmanısın ve Akademisyensin. 
Kullanıcının çözdüğü test, literatürde yeri olan 'Siber Suç Farkındalık Ölçeği (Cybercrime Awareness Scale - CAS)' adlı bilimsel bir araçtır. 
Bu ölçek 3 temel boyuttan oluşur:
1. Bilişim Sistemleri Suçları: Sisteme yetkisiz erişim, hackleme ve zararlı yazılımlar.
2. Kişisel Veri Suçları: Kimlik hırsızlığı, şifre güvenliği ve özel hayatın gizliliği.
3. Gizlilik ve Güvenlik: Sosyal mühendislik, oltalama (phishing) ve dijital ayak izi koruması.

Görev: Kullanıcının bu 3 boyuttaki CAS ölçeği sonuçlarını ve soru bazlı cevaplarını analiz ederek, akademik ve yol gösterici kişiselleştirilmiş bir gelişim raporu hazırlamak. Yorumlarını yaparken CAS ölçeğinin bu bilimsel temellerine atıfta bulun.";

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