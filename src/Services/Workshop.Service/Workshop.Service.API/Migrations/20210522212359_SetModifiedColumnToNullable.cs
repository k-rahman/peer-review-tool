﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Workshop.Service.API.Migrations
{
        public partial class SetModifiedColumnToNullable : Migration
        {
                protected override void Up(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.AlterColumn<DateTimeOffset>(
                            name: "modified",
                            table: "tasks",
                            type: "timestamp with time zone",
                            nullable: true,
                            oldClrType: typeof(DateTimeOffset),
                            oldType: "timestamp with time zone");
                }

                protected override void Down(MigrationBuilder migrationBuilder)
                {
                        migrationBuilder.AlterColumn<DateTimeOffset>(
                            name: "modified",
                            table: "tasks",
                            type: "timestamp with time zone",
                            nullable: false,
                            defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            oldClrType: typeof(DateTimeOffset),
                            oldType: "timestamp with time zone",
                            oldNullable: true);
                }
        }
}
