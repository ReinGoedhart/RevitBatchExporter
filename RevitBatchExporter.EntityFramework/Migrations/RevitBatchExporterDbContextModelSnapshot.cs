﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RevitBatchExporter.EntityFramework;

#nullable disable

namespace RevitBatchExporter.EntityFramework.Migrations
{
    [DbContext(typeof(RevitBatchExporterDbContext))]
    partial class RevitBatchExporterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("ConfigurationProject", b =>
                {
                    b.Property<int>("ConfigurationsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ConfigurationsId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("ConfigurationProject");
                });

            modelBuilder.Entity("RevitBatchExporter.Domain.Models.Configuration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConfigurationName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProjectDtoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RevitVersion")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjectDtoId");

                    b.ToTable("Configuration");
                });

            modelBuilder.Entity("RevitBatchExporter.Domain.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ConfigurationDtoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConfigurationPath")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LocalModelPath")
                        .HasColumnType("TEXT");

                    b.Property<int?>("LogFileDtoId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("ModelGuid")
                        .HasColumnType("TEXT");

                    b.Property<string>("OutputName")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ProjectGuid")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Region")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RevitExportType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RevitVersion")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("SaveAfterExport")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ViewName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ConfigurationDtoId");

                    b.HasIndex("LogFileDtoId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("RevitBatchExporter.EntityFramework.Dtos.ConfigurationDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConfigurationName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RevitVersion")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("RevitBatchExporter.EntityFramework.Dtos.LogFileDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConfigurationsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ErrorsOccured")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LogFilePath")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ConfigurationsId");

                    b.ToTable("LogFiles");
                });

            modelBuilder.Entity("RevitBatchExporter.EntityFramework.Dtos.ProjectDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConfigurationPath")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LocalModelPath")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ModelGuid")
                        .HasColumnType("TEXT");

                    b.Property<string>("OutputName")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ProjectGuid")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Region")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RevitExportType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RevitVersion")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("SaveAfterExport")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ViewName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ConfigurationProject", b =>
                {
                    b.HasOne("RevitBatchExporter.Domain.Models.Configuration", null)
                        .WithMany()
                        .HasForeignKey("ConfigurationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RevitBatchExporter.Domain.Models.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RevitBatchExporter.Domain.Models.Configuration", b =>
                {
                    b.HasOne("RevitBatchExporter.EntityFramework.Dtos.ProjectDto", null)
                        .WithMany("Configurations")
                        .HasForeignKey("ProjectDtoId");
                });

            modelBuilder.Entity("RevitBatchExporter.Domain.Models.Project", b =>
                {
                    b.HasOne("RevitBatchExporter.EntityFramework.Dtos.ConfigurationDto", null)
                        .WithMany("Projects")
                        .HasForeignKey("ConfigurationDtoId");

                    b.HasOne("RevitBatchExporter.EntityFramework.Dtos.LogFileDto", null)
                        .WithMany("Projects")
                        .HasForeignKey("LogFileDtoId");
                });

            modelBuilder.Entity("RevitBatchExporter.EntityFramework.Dtos.LogFileDto", b =>
                {
                    b.HasOne("RevitBatchExporter.Domain.Models.Configuration", "Configurations")
                        .WithMany()
                        .HasForeignKey("ConfigurationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("RevitBatchExporter.EntityFramework.Dtos.ConfigurationDto", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("RevitBatchExporter.EntityFramework.Dtos.LogFileDto", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("RevitBatchExporter.EntityFramework.Dtos.ProjectDto", b =>
                {
                    b.Navigation("Configurations");
                });
#pragma warning restore 612, 618
        }
    }
}