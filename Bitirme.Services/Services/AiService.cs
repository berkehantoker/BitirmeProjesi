using OpenAI.Chat;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitirmeProjesi.Services
{
    
    public class AiService
    {
        private readonly ChatClient _client;

        public AiService(IConfiguration configuration)
        {
            
            var apiKey = configuration["OpenAI:ApiKey"];
            var model = configuration["OpenAI:Model"] ?? "gpt-4o-mini";

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new System.Exception("OpenAI API Key bulunamadı! Lütfen appsettings.json dosyasını kontrol edin.");
            }

            _client = new ChatClient(model, apiKey);
        }

        /// <summary>
        /// Kullanıcının genel skorlarını ve 41 soruya verdiği detaylı yanıtları analiz ederek rapor oluşturur.
        /// </summary>
        /// <param name="scoreA">Bilişim Sistemleri Suçları Yüzdesi</param>
        /// <param name="scoreB">Kişisel Veri Suçları Yüzdesi</param>
        /// <param name="scoreC">Gizlilik ve Güvenlik Yüzdesi</param>
        /// <param name="detailedResponses">Her bir sorunun metni ve kullanıcının puanını içeren liste</param>
        public async Task<string> GenerateDetailedReportAsync(double scoreA, double scoreB, double scoreC, List<string> detailedResponses)
        {
            
            string systemPrompt = "Sen profesyonel bir Siber Güvenlik Farkındalık Uzmanısın. " +
                                  "Kullanıcının Siber Güvenlik Farkındalık Ölçeği (CAS) sonuçlarını analiz edip, " +
                                  "akademik bir dille kişiselleştirilmiş bir gelişim raporu hazırlayacaksın.";

            
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
                return $"Rapor oluşturulurken bir hata oluştu: {ex.Message}";
            }
        }
    }
}