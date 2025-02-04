using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basic_Appointment_Management.Migrations
{
    /// <inheritdoc />
    public partial class addedAppointmentTables1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientContactInformation",
                table: "Appointments",
                newName: "PatientContact");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientContact",
                table: "Appointments",
                newName: "PatientContactInformation");
        }
    }
}
