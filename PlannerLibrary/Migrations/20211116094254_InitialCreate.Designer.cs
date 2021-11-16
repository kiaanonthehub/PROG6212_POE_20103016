﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlannerLibrary.DbModels;

namespace PlannerLibrary.Migrations
{
    [DbContext(typeof(PlannerContext))]
    [Migration("20211116094254_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PlannerLibrary.DbModels.TblModule", b =>
                {
                    b.Property<string>("ModuleId")
                        .HasMaxLength(8)
                        .IsUnicode(false)
                        .HasColumnType("varchar(8)")
                        .HasColumnName("module_id");

                    b.Property<int>("ModuleClassHours")
                        .HasColumnType("int")
                        .HasColumnName("module_class_hours");

                    b.Property<int>("ModuleCredits")
                        .HasColumnType("int")
                        .HasColumnName("module_credits");

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("module_name");

                    b.HasKey("ModuleId")
                        .HasName("PK__tblModul__1A2D06535FD6E50A");

                    b.ToTable("tblModule");
                });

            modelBuilder.Entity("PlannerLibrary.DbModels.TblStudent", b =>
                {
                    b.Property<int>("StudentNumber")
                        .HasColumnType("int")
                        .HasColumnName("student_number");

                    b.Property<int?>("NumberOfWeeks")
                        .HasColumnType("int")
                        .HasColumnName("number_of_weeks");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime")
                        .HasColumnName("start_date");

                    b.Property<string>("StudentEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("student_email");

                    b.Property<string>("StudentHashPassword")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("student_hash_password");

                    b.Property<string>("StudentName")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("student_name");

                    b.Property<string>("StudentSurname")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("student_surname");

                    b.HasKey("StudentNumber")
                        .HasName("PK__tblStude__0E749A78C500C332");

                    b.ToTable("tblStudent");
                });

            modelBuilder.Entity("PlannerLibrary.DbModels.TblStudentModule", b =>
                {
                    b.Property<int>("StudentModuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("student_module_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ModuleId")
                        .HasMaxLength(8)
                        .IsUnicode(false)
                        .HasColumnType("varchar(8)")
                        .HasColumnName("module_id");

                    b.Property<decimal?>("ModuleSelfStudyHour")
                        .HasColumnType("decimal(5,2)")
                        .HasColumnName("module_self_study_hour");

                    b.Property<int?>("StudentNumber")
                        .HasColumnType("int")
                        .HasColumnName("student_number");

                    b.Property<decimal?>("StudyHoursRemains")
                        .HasColumnType("decimal(5,2)")
                        .HasColumnName("study_hours_remains");

                    b.Property<string>("StudyReminderDay")
                        .HasMaxLength(25)
                        .IsUnicode(false)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("study_reminder_day");

                    b.HasKey("StudentModuleId")
                        .HasName("PK__tblStude__EC62D1BAE16B185E");

                    b.HasIndex("ModuleId");

                    b.HasIndex("StudentNumber");

                    b.ToTable("tblStudentModule");
                });

            modelBuilder.Entity("PlannerLibrary.DbModels.TblTrackStudy", b =>
                {
                    b.Property<int>("TrackStudiesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("track_studies_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateWorked")
                        .HasColumnType("date")
                        .HasColumnName("date_worked");

                    b.Property<decimal?>("HoursWorked")
                        .HasColumnType("decimal(5,2)")
                        .HasColumnName("hours_worked");

                    b.Property<string>("ModuleId")
                        .HasMaxLength(8)
                        .IsUnicode(false)
                        .HasColumnType("varchar(8)")
                        .HasColumnName("module_id");

                    b.Property<int?>("StudentNumber")
                        .HasColumnType("int")
                        .HasColumnName("student_number");

                    b.Property<int?>("WeekNumber")
                        .HasColumnType("int")
                        .HasColumnName("week_number");

                    b.HasKey("TrackStudiesId")
                        .HasName("PK__tblTrack__92A794307520D04F");

                    b.HasIndex("ModuleId");

                    b.HasIndex("StudentNumber");

                    b.ToTable("tblTrackStudies");
                });

            modelBuilder.Entity("PlannerLibrary.DbModels.TblStudentModule", b =>
                {
                    b.HasOne("PlannerLibrary.DbModels.TblModule", "Module")
                        .WithMany("TblStudentModules")
                        .HasForeignKey("ModuleId")
                        .HasConstraintName("FK__tblStuden__modul__29572725");

                    b.HasOne("PlannerLibrary.DbModels.TblStudent", "StudentNumberNavigation")
                        .WithMany("TblStudentModules")
                        .HasForeignKey("StudentNumber")
                        .HasConstraintName("FK__tblStuden__stude__286302EC");

                    b.Navigation("Module");

                    b.Navigation("StudentNumberNavigation");
                });

            modelBuilder.Entity("PlannerLibrary.DbModels.TblTrackStudy", b =>
                {
                    b.HasOne("PlannerLibrary.DbModels.TblModule", "Module")
                        .WithMany("TblTrackStudies")
                        .HasForeignKey("ModuleId")
                        .HasConstraintName("FK__tblTrackS__modul__2C3393D0");

                    b.HasOne("PlannerLibrary.DbModels.TblStudent", "StudentNumberNavigation")
                        .WithMany("TblTrackStudies")
                        .HasForeignKey("StudentNumber")
                        .HasConstraintName("FK__tblTrackS__stude__2D27B809");

                    b.Navigation("Module");

                    b.Navigation("StudentNumberNavigation");
                });

            modelBuilder.Entity("PlannerLibrary.DbModels.TblModule", b =>
                {
                    b.Navigation("TblStudentModules");

                    b.Navigation("TblTrackStudies");
                });

            modelBuilder.Entity("PlannerLibrary.DbModels.TblStudent", b =>
                {
                    b.Navigation("TblStudentModules");

                    b.Navigation("TblTrackStudies");
                });
#pragma warning restore 612, 618
        }
    }
}
