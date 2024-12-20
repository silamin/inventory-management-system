using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfcRepositories.Migrations
{
    public partial class UpdateOrderStatusToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE \"Orders\" " +
                "SET \"OrderStatus\" = " +
                "CASE " +
                "   WHEN \"OrderStatus\" = 'IN_PROGRESS' THEN 0 " +
                "   WHEN \"OrderStatus\" = 'COMPLETED' THEN 1 " +
                "   ELSE 0 " +  // Default to 0 (IN_PROGRESS) if no match is found
                "END;"
            );

            migrationBuilder.Sql(
                "ALTER TABLE \"Orders\" " +
                "ALTER COLUMN \"OrderStatus\" TYPE integer USING \"OrderStatus\"::integer;"
            );

            // (Optional) If you want to reapply a default value for OrderStatus, do it here
            // migrationBuilder.Sql(
            //     "ALTER TABLE \"Orders\" ALTER COLUMN \"OrderStatus\" SET DEFAULT 0;"
            // );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE \"Orders\" " +
                "SET \"OrderStatus\" = " +
                "CASE " +
                "   WHEN \"OrderStatus\" = 0 THEN 'IN_PROGRESS' " +
                "   WHEN \"OrderStatus\" = 1 THEN 'COMPLETED' " +
                "   ELSE 'IN_PROGRESS' " +  // Default to IN_PROGRESS if no match is found
                "END;"
            );

            migrationBuilder.Sql(
                "ALTER TABLE \"Orders\" " +
                "ALTER COLUMN \"OrderStatus\" TYPE text USING \"OrderStatus\"::text;"
            );

            // (Optional) If the default value was set, you may also want to revert it.
            // migrationBuilder.Sql(
            //     "ALTER TABLE \"Orders\" ALTER COLUMN \"OrderStatus\" SET DEFAULT 'IN_PROGRESS';"
            // );
        }
    }
}
