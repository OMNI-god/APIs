using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: true),
                    DOJ = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Bankingreturn = table.Column<double>(name: "Banking_return", type: "float", nullable: true),
                    Stockreturn = table.Column<double>(name: "Stock_return", type: "float", nullable: true),
                    SIPreturn = table.Column<double>(name: "SIP_return", type: "float", nullable: true),
                    Miscellaneousreturn = table.Column<double>(name: "Miscellaneous_return", type: "float", nullable: true),
                    Totalsavings = table.Column<double>(name: "Total_savings", type: "float", nullable: true),
                    Lastupdatedate = table.Column<DateTime>(name: "Last_update_date", type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "investments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(name: "Bank_Name", type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ROI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvestmentStartDate = table.Column<DateTime>(name: "Investment_Start_Date", type: "datetime2", nullable: false),
                    MaturityDate = table.Column<DateTime>(name: "Maturity_Date", type: "datetime2", nullable: false),
                    InvestmentAmount = table.Column<double>(name: "Investment_Amount", type: "float", nullable: false),
                    MaturityAmount = table.Column<double>(name: "Maturity_Amount", type: "float", nullable: false),
                    TimeLeftToMature = table.Column<string>(name: "Time_Left_To_Mature", type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    lastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_investments", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_investments_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_investments_users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_investments_UserId",
                table: "investments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_investments_UserId1",
                table: "investments",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "investments");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
