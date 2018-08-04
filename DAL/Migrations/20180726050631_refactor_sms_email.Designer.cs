﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(context))]
    [Migration("20180726050631_refactor_sms_email")]
    partial class refactor_sms_email
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.adminMenu", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<string>("area");

                    b.Property<string>("controller");

                    b.Property<bool>("isDeleted");

                    b.Property<int>("orderIndex");

                    b.Property<bool>("showInMenu");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("siteId");

                    b.ToTable("adminMenus");
                });

            modelBuilder.Entity("Entities.attribute", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<int>("categoryId");

                    b.Property<bool>("isDeleted");

                    b.Property<string>("name");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.Property<string>("value");

                    b.HasKey("id");

                    b.HasIndex("categoryId");

                    b.HasIndex("siteId");

                    b.ToTable("attributes");
                });

            modelBuilder.Entity("Entities.box", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<int>("boxCategoryId");

                    b.Property<string>("content");

                    b.Property<string>("cssClass");

                    b.Property<bool>("isDeleted");

                    b.Property<string>("link");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("boxCategoryId");

                    b.HasIndex("siteId");

                    b.ToTable("boxes");
                });

            modelBuilder.Entity("Entities.boxCategory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<string>("cssClass");

                    b.Property<int?>("fatherId");

                    b.Property<bool>("isDeleted");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("fatherId");

                    b.HasIndex("siteId");

                    b.ToTable("boxCategories");
                });

            modelBuilder.Entity("Entities.category", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<bool>("isDeleted");

                    b.Property<string>("name");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<int>("termId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("siteId");

                    b.HasIndex("termId");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("Entities.dataType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<int?>("fatherId");

                    b.Property<bool>("isDeleted");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("fatherId");

                    b.HasIndex("siteId");

                    b.ToTable("dataTypes");
                });

            modelBuilder.Entity("Entities.dataValue", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<int>("dataTypeId");

                    b.Property<int?>("fatherId");

                    b.Property<bool>("isDeleted");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("dataTypeId");

                    b.HasIndex("siteId");

                    b.ToTable("dataValues");
                });

            modelBuilder.Entity("Entities.dataValue_category", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("categoryId");

                    b.Property<int>("dataValueId");

                    b.Property<bool>("isDeleted");

                    b.Property<int>("siteId");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("categoryId");

                    b.HasIndex("dataValueId");

                    b.HasIndex("siteId");

                    b.ToTable("dataValue_category");
                });

            modelBuilder.Entity("Entities.field", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<string>("afterCssClass");

                    b.Property<string>("beforeCssClass");

                    b.Property<int?>("dataTypeid");

                    b.Property<string>("elementCssClass");

                    b.Property<int?>("fatherid");

                    b.Property<string>("fieldCssClass");

                    b.Property<int>("fieldSetId");

                    b.Property<bool>("isDeleted");

                    b.Property<bool>("isHideLabel");

                    b.Property<bool>("isRequire");

                    b.Property<string>("labelCssClass");

                    b.Property<string>("name");

                    b.Property<int>("orderIndex");

                    b.Property<string>("placeHolder");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<string>("type");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.Property<string>("value");

                    b.HasKey("id");

                    b.HasIndex("dataTypeid");

                    b.HasIndex("fatherid");

                    b.HasIndex("fieldSetId");

                    b.HasIndex("siteId");

                    b.ToTable("fields");
                });

            modelBuilder.Entity("Entities.fieldSet", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<string>("cssClass");

                    b.Property<bool>("isDeleted");

                    b.Property<string>("name");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<int>("stepId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("siteId");

                    b.HasIndex("stepId");

                    b.ToTable("fieldSets");
                });

            modelBuilder.Entity("Entities.hook", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<bool>("isDeleted");

                    b.Property<string>("name");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("siteId");

                    b.ToTable("hooks");
                });

            modelBuilder.Entity("Entities.insurance", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<string>("cssClass");

                    b.Property<string>("formula");

                    b.Property<bool>("isDeleted");

                    b.Property<string>("name");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<int>("stepCount");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("siteId");

                    b.ToTable("insurances");
                });

            modelBuilder.Entity("Entities.menu", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<int?>("fatherId");

                    b.Property<bool>("isDeleted");

                    b.Property<string>("link");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("fatherId");

                    b.HasIndex("siteId");

                    b.ToTable("menus");
                });

            modelBuilder.Entity("Entities.order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<string>("bankReference");

                    b.Property<DateTime>("dateTime");

                    b.Property<int>("insuranceId");

                    b.Property<bool>("isDeleted");

                    b.Property<string>("log");

                    b.Property<int>("orderIndex");

                    b.Property<int>("orderStatusId");

                    b.Property<int?>("paymentTypeId");

                    b.Property<int>("price");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.Property<int>("userId");

                    b.HasKey("id");

                    b.HasIndex("insuranceId");

                    b.HasIndex("orderStatusId");

                    b.HasIndex("paymentTypeId");

                    b.HasIndex("siteId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("Entities.orderField", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<bool>("isDeleted");

                    b.Property<string>("name");

                    b.Property<int>("orderId");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<string>("type");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.Property<string>("value");

                    b.HasKey("id");

                    b.HasIndex("orderId");

                    b.HasIndex("siteId");

                    b.ToTable("orderFields");
                });

            modelBuilder.Entity("Entities.orderStatus", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<string>("color");

                    b.Property<bool>("isDeleted");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("siteId");

                    b.ToTable("orderStatuses");
                });

            modelBuilder.Entity("Entities.paymentType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<bool>("isDeleted");

                    b.Property<string>("name");

                    b.Property<int>("orderIndex");

                    b.Property<bool>("showForAll");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("siteId");

                    b.ToTable("paymentTypes");
                });

            modelBuilder.Entity("Entities.post", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<string>("brief");

                    b.Property<string>("content");

                    b.Property<string>("cssClass");

                    b.Property<bool>("isDeleted");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<string>("title2");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("siteId");

                    b.ToTable("posts");
                });

            modelBuilder.Entity("Entities.post_postCategory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("isDeleted");

                    b.Property<int?>("postCategoryid");

                    b.Property<int?>("postid");

                    b.Property<int>("siteId");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("postCategoryid");

                    b.HasIndex("postid");

                    b.HasIndex("siteId");

                    b.ToTable("post_postCategory");
                });

            modelBuilder.Entity("Entities.postCategory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<string>("cssClass");

                    b.Property<int?>("fatherId");

                    b.Property<string>("image");

                    b.Property<bool>("isDeleted");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("fatherId");

                    b.HasIndex("siteId");

                    b.ToTable("postCategories");
                });

            modelBuilder.Entity("Entities.price", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("value");

                    b.HasKey("id");

                    b.ToTable("price");
                });

            modelBuilder.Entity("Entities.role_adminMenu", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("adminMenuid");

                    b.Property<bool>("isDeleted");

                    b.Property<string>("roleName");

                    b.Property<int>("siteId");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("adminMenuid");

                    b.HasIndex("siteId");

                    b.ToTable("role_adminMenu");
                });

            modelBuilder.Entity("Entities.setting", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("isDeleted");

                    b.Property<string>("key");

                    b.Property<int>("siteId");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.Property<string>("value");

                    b.HasKey("id");

                    b.HasIndex("siteId");

                    b.ToTable("settings");
                });

            modelBuilder.Entity("Entities.site", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("host");

                    b.Property<string>("name");

                    b.HasKey("id");

                    b.ToTable("sites");
                });

            modelBuilder.Entity("Entities.sms_email", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<int?>("hookid");

                    b.Property<bool>("isDeleted");

                    b.Property<string>("mobile");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<string>("text");

                    b.Property<string>("title");

                    b.Property<string>("type");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("hookid");

                    b.HasIndex("siteId");

                    b.ToTable("Sms_emails");
                });

            modelBuilder.Entity("Entities.step", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<string>("beforStepButtonsCssClass");

                    b.Property<int>("insuranceId");

                    b.Property<bool>("isDeleted");

                    b.Property<string>("name");

                    b.Property<string>("navigationCssClass");

                    b.Property<string>("nextStepCssClass");

                    b.Property<string>("nextStepText");

                    b.Property<int>("number");

                    b.Property<int>("orderIndex");

                    b.Property<string>("previousStepCssClass");

                    b.Property<string>("previousStepText");

                    b.Property<string>("priceCssClass");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("insuranceId");

                    b.HasIndex("siteId");

                    b.ToTable("steps");
                });

            modelBuilder.Entity("Entities.term", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("active");

                    b.Property<bool>("isDeleted");

                    b.Property<string>("name");

                    b.Property<int>("orderIndex");

                    b.Property<int>("siteId");

                    b.Property<string>("title");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.HasKey("id");

                    b.HasIndex("siteId");

                    b.ToTable("terms");
                });

            modelBuilder.Entity("Entities.user_paymentType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("isDeleted");

                    b.Property<int>("paymentTypeId");

                    b.Property<int>("siteId");

                    b.Property<DateTime>("updateDateTime");

                    b.Property<int>("updateUserId");

                    b.Property<int>("userId");

                    b.HasKey("id");

                    b.HasIndex("paymentTypeId");

                    b.HasIndex("siteId");

                    b.ToTable("user_paymentType");
                });

            modelBuilder.Entity("Entities.adminMenu", b =>
                {
                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.attribute", b =>
                {
                    b.HasOne("Entities.category", "category")
                        .WithMany("attributes")
                        .HasForeignKey("categoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.box", b =>
                {
                    b.HasOne("Entities.boxCategory", "boxCategory")
                        .WithMany("boxes")
                        .HasForeignKey("boxCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.boxCategory", b =>
                {
                    b.HasOne("Entities.boxCategory", "father")
                        .WithMany("childs")
                        .HasForeignKey("fatherId");

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.category", b =>
                {
                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.term", "term")
                        .WithMany("categories")
                        .HasForeignKey("termId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.dataType", b =>
                {
                    b.HasOne("Entities.dataType", "father")
                        .WithMany()
                        .HasForeignKey("fatherId");

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.dataValue", b =>
                {
                    b.HasOne("Entities.dataType", "dataType")
                        .WithMany("dataValues")
                        .HasForeignKey("dataTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.dataValue_category", b =>
                {
                    b.HasOne("Entities.category", "category")
                        .WithMany()
                        .HasForeignKey("categoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.dataValue", "dataValue")
                        .WithMany("categories")
                        .HasForeignKey("dataValueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.field", b =>
                {
                    b.HasOne("Entities.dataType", "dataType")
                        .WithMany()
                        .HasForeignKey("dataTypeid");

                    b.HasOne("Entities.field", "father")
                        .WithMany()
                        .HasForeignKey("fatherid");

                    b.HasOne("Entities.fieldSet", "fieldSet")
                        .WithMany("fields")
                        .HasForeignKey("fieldSetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.fieldSet", b =>
                {
                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.step", "step")
                        .WithMany("fieldSets")
                        .HasForeignKey("stepId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.hook", b =>
                {
                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.insurance", b =>
                {
                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.menu", b =>
                {
                    b.HasOne("Entities.menu", "father")
                        .WithMany("childs")
                        .HasForeignKey("fatherId");

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.order", b =>
                {
                    b.HasOne("Entities.insurance", "insurance")
                        .WithMany("orders")
                        .HasForeignKey("insuranceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.orderStatus", "orderStatus")
                        .WithMany()
                        .HasForeignKey("orderStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.paymentType", "paymentType")
                        .WithMany()
                        .HasForeignKey("paymentTypeId");

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.orderField", b =>
                {
                    b.HasOne("Entities.order", "order")
                        .WithMany("fields")
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.orderStatus", b =>
                {
                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.paymentType", b =>
                {
                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.post", b =>
                {
                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.post_postCategory", b =>
                {
                    b.HasOne("Entities.postCategory", "postCategory")
                        .WithMany("posts")
                        .HasForeignKey("postCategoryid");

                    b.HasOne("Entities.post", "post")
                        .WithMany("categories")
                        .HasForeignKey("postid");

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.postCategory", b =>
                {
                    b.HasOne("Entities.postCategory", "father")
                        .WithMany("childs")
                        .HasForeignKey("fatherId");

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.role_adminMenu", b =>
                {
                    b.HasOne("Entities.adminMenu", "adminMenu")
                        .WithMany()
                        .HasForeignKey("adminMenuid");

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.setting", b =>
                {
                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.sms_email", b =>
                {
                    b.HasOne("Entities.hook", "hook")
                        .WithMany()
                        .HasForeignKey("hookid");

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.step", b =>
                {
                    b.HasOne("Entities.insurance")
                        .WithMany("steps")
                        .HasForeignKey("insuranceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.term", b =>
                {
                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entities.user_paymentType", b =>
                {
                    b.HasOne("Entities.paymentType", "paymentType")
                        .WithMany("user_paymentType")
                        .HasForeignKey("paymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entities.site", "site")
                        .WithMany()
                        .HasForeignKey("siteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
