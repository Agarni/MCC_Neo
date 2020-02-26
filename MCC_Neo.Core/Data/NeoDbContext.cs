using MCC_Neo.Core.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCC_Neo.Core.Data
{
    public class NeoDbContext : DbContext
    {
        public NeoDbContext(DbContextOptions options) : base(options)
        {
            //try
            //{
            //    var dir = CaminhoDatabase();

            //    if (!Directory.Exists(dir))
            //    {
            //        Directory.CreateDirectory(dir);
            //    }

            //    //Database.Migrate();
            //}
            //catch (Exception ex)
            //{
            //    //System.Windows.MessageBox.Show("Erro ao abrir BD!" + Environment.NewLine + Environment.NewLine +
            //    //    ex.Message, "Erro de conexão", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            //    //App.Current.Shutdown();
            //}
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string dirDB = CaminhoDatabase();

            //string connectionStringBuilder = new SqliteConnectionStringBuilder()
            //{
            //    DataSource = Path.Combine(dirDB, @"MCC_Neo.db")
            //}.ToString();

            //var connection = new SqliteConnection(connectionStringBuilder);
            //optionsBuilder
            //    .UseSqlite(connection);

            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cursilho>()
                .HasKey(x => x.CursilhoId);

            #region Cidades Cursilho
            modelBuilder.Entity<CidadeCursilho>()
                .HasKey(cc => new { cc.CursilhoId, cc.CidadeId });
            modelBuilder.Entity<CidadeCursilho>()
                .HasOne(cc => cc.Cursilho)
                .WithMany(c => c.CidadesCursilho)
                .HasForeignKey(cc => cc.CursilhoId);
            modelBuilder.Entity<CidadeCursilho>()
                .HasOne(cc => cc.Cidade)
                .WithMany(c => c.CidadesCursilho)
                .HasForeignKey(cc => cc.CidadeId);
            #endregion Cidades Cursilho

            #region Candidato
            modelBuilder.Entity<Candidato>()
                .HasIndex(c => new { c.CursilhoId, c.CandidatoCidadeId });

            modelBuilder.Entity<Candidato>()
                .HasOne(c => c.CandidatoCidade)
                .WithMany(c => c.CidadeCandidatos)
                .HasForeignKey(c => c.CandidatoCidadeId);

            modelBuilder.Entity<Candidato>()
                .HasIndex(p => p.Nome);

            modelBuilder.Entity<Candidato>()
                .HasOne(g => g.Grupo)
                .WithMany(g => g.Candidatos)
                .HasForeignKey(g => g.GrupoId);
            #endregion Candidato

            modelBuilder.Entity<Responsavel>()
                .HasIndex(p => p.Nome);

            modelBuilder.Entity<Funcao>()
                .HasIndex(f => new { f.FuncaoAtiva, f.Descricao });

            #region Mensagem
            modelBuilder.Entity<Mensagem>()
                .HasIndex(m => new { m.MensagemAtiva, m.NomeMensagem });
            #endregion Mensagem

            modelBuilder.Entity<Grupo>()
                .HasIndex(g => g.Nome);

            modelBuilder.Entity<MensagemCursilho>()
                .HasKey(mc => new { mc.CursilhoId, mc.MensagemId });
        }

        private static string CaminhoDatabase()
        {
            var dirDB = Utileco.Util.Settings.Get("DirDatabase");

            if (string.IsNullOrWhiteSpace(dirDB))
            {
                dirDB = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
                Utileco.Util.Settings.Update("DirDatabase", dirDB);
            }

            return dirDB;
        }
    }
}
