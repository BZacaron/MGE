﻿using MGE.Models.Item;
using MGE.Models.Parametros;
using Microsoft.EntityFrameworkCore;

namespace MGE.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ParametrosEntity> Parametros { get; set; }

        public DbSet<ItemEntity> Itens { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
