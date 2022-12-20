﻿// <auto-generated />
using System;
using Cheaper_Effort.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cheaper_Effort.Data.Migrations
{
    [DbContext(typeof(ProjectDbContext))]
    partial class ProjectDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("Cheaper_Effort.Models.Account", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("BLOB");

                    b.Property<int>("Points")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Cheaper_Effort.Models.Discount", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Claimer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateClaimed")
                        .HasColumnType("TEXT");

                    b.Property<int>("DiscountsType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Cheaper_Effort.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("IngredientName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Cheaper_Effort.Models.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("CategoryType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Difficult_steps")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("BLOB");

                    b.Property<int>("Points")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Time")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Cheaper_Effort.Models.Recipe_Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IngredientId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("RecipeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Recipe_Ingredients");
                });

            modelBuilder.Entity("Cheaper_Effort.Models.Recipe_Ingredient", b =>
                {
                    b.HasOne("Cheaper_Effort.Models.Ingredient", "Ingredient")
                        .WithMany("Recipe_Ingredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cheaper_Effort.Models.Recipe", "Recipe")
                        .WithMany("Recipe_Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Cheaper_Effort.Models.Ingredient", b =>
                {
                    b.Navigation("Recipe_Ingredients");
                });

            modelBuilder.Entity("Cheaper_Effort.Models.Recipe", b =>
                {
                    b.Navigation("Recipe_Ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}
