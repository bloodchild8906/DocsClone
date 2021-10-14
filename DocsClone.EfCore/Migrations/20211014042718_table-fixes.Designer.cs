﻿// <auto-generated />
using System;
using DocsClone.EfCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DocsClone.EfCore.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20211014042718_table-fixes")]
    partial class tablefixes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DocsClone.Domain.Entities.Detail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedWithTimezone")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModifiedWithTimezone")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryContactNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Details");
                });

            modelBuilder.Entity("DocsClone.Domain.Entities.Document", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessLevel")
                        .HasColumnType("int");

                    b.Property<string>("CurrentVersion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("DocsClone.Domain.Entities.Revision", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedWithTimezone")
                        .HasColumnType("int");

                    b.Property<byte[]>("DocumentData")
                        .HasColumnType("varbinary(max)");

                    b.Property<long?>("DocumentId")
                        .HasColumnType("bigint");

                    b.Property<long?>("DocumentOwnerId")
                        .HasColumnType("bigint");

                    b.Property<string>("DocumentVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Modifications")
                        .HasColumnType("varbinary(max)");

                    b.Property<long?>("ModifiedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("ModifiedWithTimezone")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("DocumentId");

                    b.HasIndex("DocumentOwnerId");

                    b.HasIndex("ModifiedById");

                    b.ToTable("Revisions");
                });

            modelBuilder.Entity("DocsClone.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("DocumentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DocsClone.Domain.Entities.Detail", b =>
                {
                    b.HasOne("DocsClone.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DocsClone.Domain.Entities.Revision", b =>
                {
                    b.HasOne("DocsClone.Domain.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("DocsClone.Domain.Entities.Document", "Document")
                        .WithMany("Revisions")
                        .HasForeignKey("DocumentId");

                    b.HasOne("DocsClone.Domain.Entities.User", "DocumentOwner")
                        .WithMany()
                        .HasForeignKey("DocumentOwnerId");

                    b.HasOne("DocsClone.Domain.Entities.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("Document");

                    b.Navigation("DocumentOwner");

                    b.Navigation("ModifiedBy");
                });

            modelBuilder.Entity("DocsClone.Domain.Entities.User", b =>
                {
                    b.HasOne("DocsClone.Domain.Entities.Document", null)
                        .WithMany("User")
                        .HasForeignKey("DocumentId");
                });

            modelBuilder.Entity("DocsClone.Domain.Entities.Document", b =>
                {
                    b.Navigation("Revisions");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
