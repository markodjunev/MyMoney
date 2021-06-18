namespace MyMoney.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class BankAndDepositIntegration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdditionOfAmounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionOfAmounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityForCredit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityForCredit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OverdraftPossibilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverdraftPossibilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfDeposits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfDeposits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfInterests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfPaymentOfInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfPaymentOfInterests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WhoIsDepositFor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhoIsDepositFor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveAnnualInterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    TermOfTheDeposit = table.Column<int>(type: "int", nullable: false),
                    BankId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TypeOfDepositId = table.Column<int>(type: "int", nullable: false),
                    TypeOfPaymentOfInterestId = table.Column<int>(type: "int", nullable: false),
                    WhoIsDepositForId = table.Column<int>(type: "int", nullable: false),
                    TypeOfInterestId = table.Column<int>(type: "int", nullable: false),
                    AdditionOfAmountsId = table.Column<int>(type: "int", nullable: false),
                    OverdraftPossibilityId = table.Column<int>(type: "int", nullable: false),
                    OpportunityForCreditId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deposits_AdditionOfAmounts_AdditionOfAmountsId",
                        column: x => x.AdditionOfAmountsId,
                        principalTable: "AdditionOfAmounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deposits_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deposits_OpportunityForCredit_OpportunityForCreditId",
                        column: x => x.OpportunityForCreditId,
                        principalTable: "OpportunityForCredit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deposits_OverdraftPossibilities_OverdraftPossibilityId",
                        column: x => x.OverdraftPossibilityId,
                        principalTable: "OverdraftPossibilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deposits_TypeOfDeposits_TypeOfDepositId",
                        column: x => x.TypeOfDepositId,
                        principalTable: "TypeOfDeposits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deposits_TypeOfInterests_TypeOfInterestId",
                        column: x => x.TypeOfInterestId,
                        principalTable: "TypeOfInterests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deposits_TypeOfPaymentOfInterests_TypeOfPaymentOfInterestId",
                        column: x => x.TypeOfPaymentOfInterestId,
                        principalTable: "TypeOfPaymentOfInterests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deposits_WhoIsDepositFor_WhoIsDepositForId",
                        column: x => x.WhoIsDepositForId,
                        principalTable: "WhoIsDepositFor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionOfAmounts_IsDeleted",
                table: "AdditionOfAmounts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_IsDeleted",
                table: "Banks",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_AdditionOfAmountsId",
                table: "Deposits",
                column: "AdditionOfAmountsId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_BankId",
                table: "Deposits",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_IsDeleted",
                table: "Deposits",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_OpportunityForCreditId",
                table: "Deposits",
                column: "OpportunityForCreditId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_OverdraftPossibilityId",
                table: "Deposits",
                column: "OverdraftPossibilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_TypeOfDepositId",
                table: "Deposits",
                column: "TypeOfDepositId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_TypeOfInterestId",
                table: "Deposits",
                column: "TypeOfInterestId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_TypeOfPaymentOfInterestId",
                table: "Deposits",
                column: "TypeOfPaymentOfInterestId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_WhoIsDepositForId",
                table: "Deposits",
                column: "WhoIsDepositForId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityForCredit_IsDeleted",
                table: "OpportunityForCredit",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OverdraftPossibilities_IsDeleted",
                table: "OverdraftPossibilities",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfDeposits_IsDeleted",
                table: "TypeOfDeposits",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfInterests_IsDeleted",
                table: "TypeOfInterests",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfPaymentOfInterests_IsDeleted",
                table: "TypeOfPaymentOfInterests",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WhoIsDepositFor_IsDeleted",
                table: "WhoIsDepositFor",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deposits");

            migrationBuilder.DropTable(
                name: "AdditionOfAmounts");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "OpportunityForCredit");

            migrationBuilder.DropTable(
                name: "OverdraftPossibilities");

            migrationBuilder.DropTable(
                name: "TypeOfDeposits");

            migrationBuilder.DropTable(
                name: "TypeOfInterests");

            migrationBuilder.DropTable(
                name: "TypeOfPaymentOfInterests");

            migrationBuilder.DropTable(
                name: "WhoIsDepositFor");
        }
    }
}
