using BitirmeProjesi.Models;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProjesi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Question> Questions { get; set; }
        public DbSet<UserResponse> UserResponses { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<TestResult> TestResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Question>().HasData(
                // A. Bilişim Sistemleri Suçları (Information Systems Crimes)
                new Question { Id = 1, Dimension = "A", IsReverse = false, QuestionText = "Siber alanda yasaklanmış bir cihaz veya program (şifre kırıcı, e-imza aracı vb.) kullanmanın suç olduğunun farkındayım." },
                new Question { Id = 2, Dimension = "A", IsReverse = false, QuestionText = "Bilişim sistemlerini kullanarak nitelikli hırsızlık yapmanın suç olduğunu biliyorum." },
                new Question { Id = 3, Dimension = "A", IsReverse = false, QuestionText = "Siber alanda nitelikli etkileşimli dolandırıcılık yapmanın suç olduğunu biliyorum." },
                new Question { Id = 4, Dimension = "A", IsReverse = false, QuestionText = "Başka birinin e-posta hesabını ele geçirmek (hacklemek) bir suçtur." },
                new Question { Id = 5, Dimension = "A", IsReverse = false, QuestionText = "İletişim verilerini (e-posta, adres, telefon vb.) kullanıcıların izni olmadan kullanmak bir suçtur." },
                new Question { Id = 6, Dimension = "A", IsReverse = false, QuestionText = "Siber alanda soruşturma gizliliğini ihlal etmenin yasal bir cezası vardır." },
                new Question { Id = 7, Dimension = "A", IsReverse = false, QuestionText = "Siber alanda uyuşturucu kullanımını özendirmek bir suçtur." },
                new Question { Id = 8, Dimension = "A", IsReverse = false, QuestionText = "Siber alanda bir devlete veya devlet kurumlarına hakaret etmenin suç olduğunu biliyorum." },
                new Question { Id = 9, Dimension = "A", IsReverse = false, QuestionText = "Terör içerikli paylaşımların suç teşkil ettiğinin farkındayım." },
                new Question { Id = 10, Dimension = "A", IsReverse = false, QuestionText = "Donanım ve yazılımı korumaya yönelik programları devre dışı bırakmak bir suçtur." },
                new Question { Id = 11, Dimension = "A", IsReverse = false, QuestionText = "Siber alanda asılsız siyasi ve askeri paylaşımlar yapmak bir suçtur." },
                new Question { Id = 12, Dimension = "A", IsReverse = false, QuestionText = "Siber alanda bir bilgisayar sisteminin çalışmasını engellemek veya durdurmak bir suçtur." },
                new Question { Id = 13, Dimension = "A", IsReverse = false, QuestionText = "Siber alanda yasa dışı (çalıntı, sahte vb.) ürünler satmak bir suçtur." },
                new Question { Id = 14, Dimension = "A", IsReverse = false, QuestionText = "Bağlantılar (linkler) aracılığıyla bireyleri yasa dışı sitelere yönlendirmek bir suçtur." },
                new Question { Id = 15, Dimension = "A", IsReverse = false, QuestionText = "Siber alanda siber suç işlemek için yer ve imkan sağlamak bir suçtur." },
                new Question { Id = 16, Dimension = "A", IsReverse = false, QuestionText = "Güvenilir olmayan borsalarda kripto paralara yatırım yaparken dolandırılabilirim." },

                // B. Kişisel Veri Suçları (Personal Data Offenses)
                new Question { Id = 17, Dimension = "B", IsReverse = false, QuestionText = "Açık bir bilişim sistemine yetkisiz erişim sağlanması yasal işlem gerektirir." },
                new Question { Id = 18, Dimension = "B", IsReverse = false, QuestionText = "Kişiler arasındaki haberleşmenin gizliliğini ihlal etmek bir suçtur." },
                new Question { Id = 19, Dimension = "B", IsReverse = false, QuestionText = "Kişisel bilgileri siber alanda üçüncü şahıslarla paylaşmak bir suçtur." },
                new Question { Id = 20, Dimension = "B", IsReverse = false, QuestionText = "Siber alanda yasa dışı bahis veya kumar oynamak bir suçtur." },
                new Question { Id = 21, Dimension = "B", IsReverse = false, QuestionText = "Siber alanda şantaj yapmak bir suçtur." },
                new Question { Id = 22, Dimension = "B", IsReverse = false, QuestionText = "Siber alanda hakaret içerikli ifadeler kullanmanın suç olduğunu biliyorum." },
                new Question { Id = 23, Dimension = "B", IsReverse = false, QuestionText = "İzin almadan ses veya video kaydı yapmak bir suçtur." },
                new Question { Id = 24, Dimension = "B", IsReverse = false, QuestionText = "Yasa dışı işlemler yoluyla siber alanda haksız kazanç elde etmek bir suçtur." },
                new Question { Id = 25, Dimension = "B", IsReverse = false, QuestionText = "Bilişim sistemlerinin ele geçirilmesi bir suçtur." },
                new Question { Id = 26, Dimension = "B", IsReverse = false, QuestionText = "Siber taciz ve zorbalığın suç teşkil ettiğini biliyorum." },
                new Question { Id = 27, Dimension = "B", IsReverse = false, QuestionText = "Başkasına ait e-imza verilerini kullanmanın suç olduğunu biliyorum." },
                new Question { Id = 28, Dimension = "B", IsReverse = false, QuestionText = "Cinsel içeriklerin siber alanda paylaşılmaması gerektiğinin farkındayım." },
                new Question { Id = 29, Dimension = "B", IsReverse = false, QuestionText = "Başkasının sosyal medya hesap şifresini ele geçirerek işlem yapmak bir suçtur." },
                new Question { Id = 30, Dimension = "B", IsReverse = false, QuestionText = "Siber alanda sabotaj içeren beyanlarda bulunmak bir suçtur." },
                new Question { Id = 31, Dimension = "B", IsReverse = false, QuestionText = "Bilişim sistemlerini kötü amaçlı yazılımlar (virüs, Truva atı vb.) yoluyla bozmak bir suçtur." },
                new Question { Id = 32, Dimension = "B", IsReverse = false, QuestionText = "Bir web sitesine izinsiz girmek bir suçtur." },
                new Question { Id = 33, Dimension = "B", IsReverse = false, QuestionText = "Siber alanda fidye yazılımı (siber şantaj) kullanarak para veya değerli eşya istemek bir suçtur." },

                // C. Gizlilik ve Güvenlik (Privacy and Security)
                new Question { Id = 34, Dimension = "C", IsReverse = false, QuestionText = "Telif haklarını ihlal etmemek için siber alanda yaptığım alıntılarda kaynak belirtmeliyim." },
                new Question { Id = 35, Dimension = "C", IsReverse = false, QuestionText = "Özel hayatın gizliliğini ihlal edenlere karşı suç duyurusunda bulunmalıyım." },
                new Question { Id = 36, Dimension = "C", IsReverse = false, QuestionText = "Kamu kurumları tarafından alınan web sitesine erişim engelleme kararlarına uyarım." },
                new Question { Id = 37, Dimension = "C", IsReverse = false, QuestionText = "Bir bilişim sistemindeki verileri yok etmek bir suçtur." },
                new Question { Id = 38, Dimension = "C", IsReverse = false, QuestionText = "Yanıltıcı (spam) e-postalar göndermenin suç teşkil ettiğinin farkındayım." },
                new Question { Id = 39, Dimension = "C", IsReverse = false, QuestionText = "Kripto paralar aracılığıyla yasa dışı işlemler yapmak bir suçtur." },
                new Question { Id = 40, Dimension = "C", IsReverse = false, QuestionText = "Lisans sözleşmesini ihlal eden yazılımları kullanmak bir suçtur." },
                new Question { Id = 41, Dimension = "C", IsReverse = false, QuestionText = "Siber alanda gizli görevlere ilişkin sırların ifşa edilmesi bir suçtur." }
            );

          


            modelBuilder.Entity<UserResponse>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.Responses)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserResponse>()
                .HasOne(ur => ur.Question)
                .WithMany(q => q.Responses)
                .HasForeignKey(ur => ur.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
