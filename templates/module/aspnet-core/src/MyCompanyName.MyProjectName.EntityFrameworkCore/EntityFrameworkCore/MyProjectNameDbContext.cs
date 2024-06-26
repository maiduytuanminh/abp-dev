﻿using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.EntityFrameworkCore;

[ConnectionStringName(MyProjectNameDbProperties.ConnectionStringName)]
public class MyProjectNameDbContext : SmartSoftwareDbContext<MyProjectNameDbContext>, IMyProjectNameDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public MyProjectNameDbContext(DbContextOptions<MyProjectNameDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureMyProjectName();
    }
}
