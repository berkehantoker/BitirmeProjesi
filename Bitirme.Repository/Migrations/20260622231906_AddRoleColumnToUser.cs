using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bitirme.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleColumnToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dimension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsReverse = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ScoreA = table.Column<double>(type: "float", nullable: false),
                    ScoreB = table.Column<double>(type: "float", nullable: false),
                    ScoreC = table.Column<double>(type: "float", nullable: false),
                    AiReport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResults_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerValue = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserResponses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserResponses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Dimension", "IsReverse", "QuestionText" },
                values: new object[,]
                {
                    { 1, "A", false, "Siber alanda yasaklanmış bir cihaz veya program (şifre kırıcı, e-imza aracı vb.) kullanmanın suç olduğunun farkındayım." },
                    { 2, "A", false, "Bilişim sistemlerini kullanarak nitelikli hırsızlık yapmanın suç olduğunu biliyorum." },
                    { 3, "A", false, "Siber alanda nitelikli etkileşimli dolandırıcılık yapmanın suç olduğunu biliyorum." },
                    { 4, "A", false, "Başka birinin e-posta hesabını ele geçirmek (hacklemek) bir suçtur." },
                    { 5, "A", false, "İletişim verilerini (e-posta, adres, telefon vb.) kullanıcıların izni olmadan kullanmak bir suçtur." },
                    { 6, "A", false, "Siber alanda soruşturma gizliliğini ihlal etmenin yasal bir cezası vardır." },
                    { 7, "A", false, "Siber alanda uyuşturucu kullanımını özendirmek bir suçtur." },
                    { 8, "A", false, "Siber alanda bir devlete veya devlet kurumlarına hakaret etmenin suç olduğunu biliyorum." },
                    { 9, "A", false, "Terör içerikli paylaşımların suç teşkil ettiğinin farkındayım." },
                    { 10, "A", false, "Donanım ve yazılımı korumaya yönelik programları devre dışı bırakmak bir suçtur." },
                    { 11, "A", false, "Siber alanda asılsız siyasi ve askeri paylaşımlar yapmak bir suçtur." },
                    { 12, "A", false, "Siber alanda bir bilgisayar sisteminin çalışmasını engellemek veya durdurmak bir suçtur." },
                    { 13, "A", false, "Siber alanda yasa dışı (çalıntı, sahte vb.) ürünler satmak bir suçtur." },
                    { 14, "A", false, "Bağlantılar (linkler) aracılığıyla bireyleri yasa dışı sitelere yönlendirmek bir suçtur." },
                    { 15, "A", false, "Siber alanda siber suç işlemek için yer ve imkan sağlamak bir suçtur." },
                    { 16, "A", false, "Güvenilir olmayan borsalarda kripto paralara yatırım yaparken dolandırılabilirim." },
                    { 17, "B", false, "Açık bir bilişim sistemine yetkisiz erişim sağlanması yasal işlem gerektirir." },
                    { 18, "B", false, "Kişiler arasındaki haberleşmenin gizliliğini ihlal etmek bir suçtur." },
                    { 19, "B", false, "Kişisel bilgileri siber alanda üçüncü şahıslarla paylaşmak bir suçtur." },
                    { 20, "B", false, "Siber alanda yasa dışı bahis veya kumar oynamak bir suçtur." },
                    { 21, "B", false, "Siber alanda şantaj yapmak bir suçtur." },
                    { 22, "B", false, "Siber alanda hakaret içerikli ifadeler kullanmanın suç olduğunu biliyorum." },
                    { 23, "B", false, "İzin almadan ses veya video kaydı yapmak bir suçtur." },
                    { 24, "B", false, "Yasa dışı işlemler yoluyla siber alanda haksız kazanç elde etmek bir suçtur." },
                    { 25, "B", false, "Bilişim sistemlerinin ele geçirilmesi bir suçtur." },
                    { 26, "B", false, "Siber taciz ve zorbalığın suç teşkil ettiğini biliyorum." },
                    { 27, "B", false, "Başkasına ait e-imza verilerini kullanmanın suç olduğunu biliyorum." },
                    { 28, "B", false, "Cinsel içeriklerin siber alanda paylaşılmaması gerektiğinin farkındayım." },
                    { 29, "B", false, "Başkasının sosyal medya hesap şifresini ele geçirerek işlem yapmak bir suçtur." },
                    { 30, "B", false, "Siber alanda sabotaj içeren beyanlarda bulunmak bir suçtur." },
                    { 31, "B", false, "Bilişim sistemlerini kötü amaçlı yazılımlar (virüs, Truva atı vb.) yoluyla bozmak bir suçtur." },
                    { 32, "B", false, "Bir web sitesine izinsiz girmek bir suçtur." },
                    { 33, "B", false, "Siber alanda fidye yazılımı (siber şantaj) kullanarak para veya değerli eşya istemek bir suçtur." },
                    { 34, "C", false, "Telif haklarını ihlal etmemek için siber alanda yaptığım alıntılarda kaynak belirtmeliyim." },
                    { 35, "C", false, "Özel hayatın gizliliğini ihlal edenlere karşı suç duyurusunda bulunmalıyım." },
                    { 36, "C", false, "Kamu kurumları tarafından alınan web sitesine erişim engelleme kararlarına uyarım." },
                    { 37, "C", false, "Bir bilişim sistemindeki verileri yok etmek bir suçtur." },
                    { 38, "C", false, "Yanıltıcı (spam) e-postalar göndermenin suç teşkil ettiğinin farkındayım." },
                    { 39, "C", false, "Kripto paralar aracılığıyla yasa dışı işlemler yapmak bir suçtur." },
                    { 40, "C", false, "Lisans sözleşmesini ihlal eden yazılımları kullanmak bir suçtur." },
                    { 41, "C", false, "Siber alanda gizli görevlere ilişkin sırların ifşa edilmesi bir suçtur." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_UserId",
                table: "TestResults",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserResponses_QuestionId",
                table: "UserResponses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserResponses_UserId",
                table: "UserResponses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestResults");

            migrationBuilder.DropTable(
                name: "UserResponses");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
