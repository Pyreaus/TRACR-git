using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bristows.TRACR.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DataTransferUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "SKILLS",
            columns: new[] { "SKILL_ID", "SKILL_NAME", "SKILL_DESCRIPTION", "SHOW", "COLOUR" },
            values: new object[,]
            {
                { 1, "Programming", "Programming languages and development", "true", "#0000FF" },   
                { 2, "Database Management", "Database design and management", "true", "#008000" },  
                { 3, "Web Development", "Frontend and backend web development", "true", "#FF0000" },
                { 4, "Data Analysis", "Analyzing and interpreting data", "true", "#FFFF00" },        
                { 5, "Graphic Design", "Creating visual content and graphics", "true", "#800080" },   
                { 6, "Project Management", "Managing and leading projects", "true", "#FFA500" },      
                { 7, "Machine Learning", "Implementing machine learning algorithms", "true", "#FFC0CB" },
                { 8, "Cybersecurity", "Securing computer systems and networks", "true", "#808080" },
                { 9, "Mobile App Development", "Developing applications for mobile devices", "true", "#00FFFF" },
                { 10, "UI/UX Design", "User interface and user experience design", "true", "#A52A2A" },       
                { 11, "Network Administration", "Managing computer networks", "true", "#008080" },   
                { 12, "Cloud Computing", "Working with cloud-based services", "true", "#00FF00" },  
                { 13, "Artificial Intelligence", "Implementing AI solutions", "true", "#4B0082" },    
                { 14, "Content Writing", "Creating written content", "true", "#FF00FF" },            
                { 15, "Digital Marketing", "Marketing products/services online", "true", "#808000" }
            }, schema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
