using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Schedulizer_WebApp.Models;

namespace Schedulizer_WebApp.Migrations
{
    [DbContext(typeof(CourseContext))]
    [Migration("20160424051253_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Schedulizer_WebApp.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("College");

                    b.Property<int>("CourseCode");

                    b.Property<string>("Department");

                    b.Property<int?>("ScheduleId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Schedulizer_WebApp.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name");

                    b.Property<string>("UserName");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Schedulizer_WebApp.Models.Course", b =>
                {
                    b.HasOne("Schedulizer_WebApp.Models.Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId");
                });
        }
    }
}
