﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Astrum.TrackerProject.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Astrum.TrackerProject.Persistance.Migrations
{
    [DbContext(typeof(TrackerProjectDbContext))]
    [Migration("20230428082915_ExternalUsers")]
    partial class ExternalUsers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("TrackerProject")
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Astrum.SharedLib.Persistence.Models.Audit.AuditHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Changed")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Kind")
                        .HasColumnType("integer");

                    b.Property<string>("RowId")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Username")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.ToTable("AuditHistory", "TrackerProject");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.Article", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AuthorId")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsNew")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("ParentArticleId")
                        .HasColumnType("text");

                    b.Property<string>("ParentId")
                        .HasColumnType("text");

                    b.Property<string>("ProjectId")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Articles", "TrackerProject");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.ArticleComment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ArticleId")
                        .HasColumnType("text");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("ArticleComments", "TrackerProject");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.Attachment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ArticleCommentId")
                        .HasColumnType("text");

                    b.Property<string>("ArticleId")
                        .HasColumnType("text");

                    b.Property<string>("Base64Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Charset")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("IssueCommentId")
                        .HasColumnType("text");

                    b.Property<string>("IssueId")
                        .HasColumnType("text");

                    b.Property<string>("MetaData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("Size")
                        .HasColumnType("bigint");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ArticleCommentId");

                    b.HasIndex("ArticleId");

                    b.HasIndex("IssueCommentId");

                    b.HasIndex("IssueId");

                    b.ToTable("Attachments", "TrackerProject");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.ExternalUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ExternalUsers", "TrackerProject");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.Issue", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int?>("CommentsCount")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("DraftOwnerId")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsDraft")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("ParentId")
                        .HasColumnType("text");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ReporterId")
                        .HasColumnType("text");

                    b.Property<string>("Summary")
                        .HasColumnType("text");

                    b.Property<string>("UpdaterId")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Issues", "TrackerProject");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.IssueComment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("IssueId")
                        .HasColumnType("text");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("IssueComments", "TrackerProject");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.IssueTag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("IssueId")
                        .HasColumnType("text");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("IssueTags", "TrackerProject");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.Project", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("IconUrl")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LeaderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TeamId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TeamId1")
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TeamId1");

                    b.ToTable("Projects", "TrackerProject");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.Team", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<List<string>>("Members")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Teams", "TrackerProject");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.Article", b =>
                {
                    b.HasOne("Astrum.TrackerProject.Domain.Aggregates.Article", "Parent")
                        .WithMany("ChildArticles")
                        .HasForeignKey("ParentId");

                    b.HasOne("Astrum.TrackerProject.Domain.Aggregates.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.Navigation("Parent");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.ArticleComment", b =>
                {
                    b.HasOne("Astrum.TrackerProject.Domain.Aggregates.Article", null)
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.Attachment", b =>
                {
                    b.HasOne("Astrum.TrackerProject.Domain.Aggregates.ArticleComment", null)
                        .WithMany("Attachments")
                        .HasForeignKey("ArticleCommentId");

                    b.HasOne("Astrum.TrackerProject.Domain.Aggregates.Article", null)
                        .WithMany("Attachments")
                        .HasForeignKey("ArticleId");

                    b.HasOne("Astrum.TrackerProject.Domain.Aggregates.IssueComment", null)
                        .WithMany("Attachments")
                        .HasForeignKey("IssueCommentId");

                    b.HasOne("Astrum.TrackerProject.Domain.Aggregates.Issue", null)
                        .WithMany("Attachments")
                        .HasForeignKey("IssueId");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.Issue", b =>
                {
                    b.HasOne("Astrum.TrackerProject.Domain.Aggregates.Issue", "Parent")
                        .WithMany("Subtasks")
                        .HasForeignKey("ParentId");

                    b.HasOne("Astrum.TrackerProject.Domain.Aggregates.Project", "Project")
                        .WithMany("Issues")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.IssueComment", b =>
                {
                    b.HasOne("Astrum.TrackerProject.Domain.Aggregates.Issue", null)
                        .WithMany("Comments")
                        .HasForeignKey("IssueId");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.IssueTag", b =>
                {
                    b.HasOne("Astrum.TrackerProject.Domain.Aggregates.Issue", null)
                        .WithMany("Tags")
                        .HasForeignKey("IssueId");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.Project", b =>
                {
                    b.HasOne("Astrum.TrackerProject.Domain.Aggregates.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId1");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.Article", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("ChildArticles");

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.ArticleComment", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.Issue", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Comments");

                    b.Navigation("Subtasks");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.IssueComment", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("Astrum.TrackerProject.Domain.Aggregates.Project", b =>
                {
                    b.Navigation("Issues");
                });
#pragma warning restore 612, 618
        }
    }
}
