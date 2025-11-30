using Conexus.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Conexus.Api.Infra;

public partial class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aluno> Alunos { get; set; }

    public virtual DbSet<AlunoTurma> AlunoTurmas { get; set; }

    public virtual DbSet<Avaliacao> Avaliacaos { get; set; }

    public virtual DbSet<CalendarioAula> CalendarioAulas { get; set; }

    public virtual DbSet<Cartao> Cartaos { get; set; }

    public virtual DbSet<Contato> Contatos { get; set; }

    public virtual DbSet<DocumentoAluno> DocumentoAlunos { get; set; }

    public virtual DbSet<EmprestimoMaterial> EmprestimoMaterials { get; set; }

    public virtual DbSet<FormaPagamento> FormaPagamentos { get; set; }

    public virtual DbSet<Funcionario> Funcionarios { get; set; }

    public virtual DbSet<Idioma> Idiomas { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Nivel> Nivels { get; set; }

    public virtual DbSet<Pagamento> Pagamentos { get; set; }

    public virtual DbSet<Presenca> Presencas { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<TipoMaterial> TipoMaterials { get; set; }

    public virtual DbSet<Turma> Turmas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=escola_idiomas;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.Idaluno).HasName("PRIMARY");

            entity.ToTable("aluno");

            entity.HasIndex(e => e.Idusuario, "idusuario");

            entity.Property(e => e.Idaluno)
                .HasColumnType("int(11)")
                .HasColumnName("idaluno");
            entity.Property(e => e.Bairro)
                .HasMaxLength(100)
                .HasColumnName("bairro");
            entity.Property(e => e.Cep)
                .HasMaxLength(8)
                .HasColumnName("cep");
            entity.Property(e => e.Complemento)
                .HasMaxLength(100)
                .HasColumnName("complemento");
            entity.Property(e => e.Idusuario)
                .HasColumnType("int(11)")
                .HasColumnName("idusuario");
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .HasColumnName("numero");
            entity.Property(e => e.Responsavel)
                .HasMaxLength(200)
                .HasColumnName("responsavel");
            entity.Property(e => e.Rua)
                .HasMaxLength(255)
                .HasColumnName("rua");
            entity.Property(e => e.Situacao)
                .HasDefaultValueSql("'ativo'")
                .HasColumnType("enum('ativo','trancado','cancelado')")
                .HasColumnName("situacao");
            entity.Property(e => e.TelResponsavel)
                .HasMaxLength(11)
                .HasColumnName("tel_responsavel");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Alunos)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aluno_ibfk_1");
        });

        modelBuilder.Entity<AlunoTurma>(entity =>
        {
            entity.HasKey(e => e.IdalunoTurma).HasName("PRIMARY");

            entity.ToTable("aluno_turma");

            entity.HasIndex(e => new { e.Idaluno, e.Idturma }, "idaluno").IsUnique();

            entity.HasIndex(e => e.Idturma, "idturma");

            entity.Property(e => e.IdalunoTurma)
                .HasColumnType("int(11)")
                .HasColumnName("idaluno_turma");
            entity.Property(e => e.DataMatricula)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnName("data_matricula");
            entity.Property(e => e.Idaluno)
                .HasColumnType("int(11)")
                .HasColumnName("idaluno");
            entity.Property(e => e.Idturma)
                .HasColumnType("int(11)")
                .HasColumnName("idturma");

            entity.HasOne(d => d.IdalunoNavigation).WithMany(p => p.AlunoTurmas)
                .HasForeignKey(d => d.Idaluno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aluno_turma_ibfk_1");

            entity.HasOne(d => d.IdturmaNavigation).WithMany(p => p.AlunoTurmas)
                .HasForeignKey(d => d.Idturma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("aluno_turma_ibfk_2");
        });

        modelBuilder.Entity<Avaliacao>(entity =>
        {
            entity.HasKey(e => e.Idavaliacao).HasName("PRIMARY");

            entity.ToTable("avaliacao");

            entity.HasIndex(e => e.IdalunoTurma, "idaluno_turma");

            entity.HasIndex(e => e.Idfuncionario, "idfuncionario");

            entity.HasIndex(e => e.Idturma, "idturma");

            entity.Property(e => e.Idavaliacao)
                .HasColumnType("int(11)")
                .HasColumnName("idavaliacao");
            entity.Property(e => e.DataAvaliacao).HasColumnName("data_avaliacao");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .HasColumnName("descricao");
            entity.Property(e => e.IdalunoTurma)
                .HasColumnType("int(11)")
                .HasColumnName("idaluno_turma");
            entity.Property(e => e.Idfuncionario)
                .HasColumnType("int(11)")
                .HasColumnName("idfuncionario");
            entity.Property(e => e.Idturma)
                .HasColumnType("int(11)")
                .HasColumnName("idturma");
            entity.Property(e => e.Nota)
                .HasPrecision(4, 2)
                .HasColumnName("nota");
            entity.Property(e => e.Observacao)
                .HasColumnType("text")
                .HasColumnName("observacao");
            entity.Property(e => e.Peso)
                .HasPrecision(3, 2)
                .HasDefaultValueSql("'1.00'")
                .HasColumnName("peso");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdalunoTurmaNavigation).WithMany(p => p.Avaliacaos)
                .HasForeignKey(d => d.IdalunoTurma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("avaliacao_ibfk_2");

            entity.HasOne(d => d.IdfuncionarioNavigation).WithMany(p => p.Avaliacaos)
                .HasForeignKey(d => d.Idfuncionario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("avaliacao_ibfk_3");

            entity.HasOne(d => d.IdturmaNavigation).WithMany(p => p.Avaliacaos)
                .HasForeignKey(d => d.Idturma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("avaliacao_ibfk_1");
        });

        modelBuilder.Entity<CalendarioAula>(entity =>
        {
            entity.HasKey(e => e.Idaula).HasName("PRIMARY");

            entity.ToTable("calendario_aula");

            entity.HasIndex(e => e.Idfuncionario, "idfuncionario");

            entity.HasIndex(e => e.Idmaterial, "idmaterial");

            entity.HasIndex(e => e.Idturma, "idturma");

            entity.Property(e => e.Idaula)
                .HasColumnType("int(11)")
                .HasColumnName("idaula");
            entity.Property(e => e.AulaExtra)
                .HasDefaultValueSql("'0'")
                .HasColumnName("aula_extra");
            entity.Property(e => e.DataAula).HasColumnName("data_aula");
            entity.Property(e => e.HoraFim)
                .HasColumnType("time")
                .HasColumnName("hora_fim");
            entity.Property(e => e.HoraInicio)
                .HasColumnType("time")
                .HasColumnName("hora_inicio");
            entity.Property(e => e.Idfuncionario)
                .HasColumnType("int(11)")
                .HasColumnName("idfuncionario");
            entity.Property(e => e.Idmaterial)
                .HasColumnType("int(11)")
                .HasColumnName("idmaterial");
            entity.Property(e => e.Idturma)
                .HasColumnType("int(11)")
                .HasColumnName("idturma");
            entity.Property(e => e.LinkReuniao)
                .HasMaxLength(255)
                .HasColumnName("link_reuniao");
            entity.Property(e => e.Observacoes)
                .HasMaxLength(300)
                .HasColumnName("observacoes");
            entity.Property(e => e.Sala)
                .HasMaxLength(100)
                .HasColumnName("sala");

            entity.HasOne(d => d.IdfuncionarioNavigation).WithMany(p => p.CalendarioAulas)
                .HasForeignKey(d => d.Idfuncionario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calendario_aula_ibfk_1");

            entity.HasOne(d => d.IdmaterialNavigation).WithMany(p => p.CalendarioAulas)
                .HasForeignKey(d => d.Idmaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calendario_aula_ibfk_3");

            entity.HasOne(d => d.IdturmaNavigation).WithMany(p => p.CalendarioAulas)
                .HasForeignKey(d => d.Idturma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("calendario_aula_ibfk_2");
        });

        modelBuilder.Entity<Cartao>(entity =>
        {
            entity.HasKey(e => e.Idcartao).HasName("PRIMARY");

            entity.ToTable("cartao");

            entity.HasIndex(e => e.Idaluno, "idaluno");

            entity.Property(e => e.Idcartao)
                .HasColumnType("int(11)")
                .HasColumnName("idcartao");
            entity.Property(e => e.Bandeira)
                .HasMaxLength(20)
                .HasColumnName("bandeira");
            entity.Property(e => e.Idaluno)
                .HasColumnType("int(11)")
                .HasColumnName("idaluno");
            entity.Property(e => e.NomeTitular)
                .HasMaxLength(100)
                .HasColumnName("nome_titular");
            entity.Property(e => e.NumeroCriptografado)
                .HasMaxLength(256)
                .HasColumnName("numero_criptografado");
            entity.Property(e => e.UltimosDigitos)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("ultimos_digitos");
            entity.Property(e => e.ValidadeAno)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("validade_ano");
            entity.Property(e => e.ValidadeMes)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("validade_mes");

            entity.HasOne(d => d.IdalunoNavigation).WithMany(p => p.Cartaos)
                .HasForeignKey(d => d.Idaluno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cartao_ibfk_1");
        });

        modelBuilder.Entity<Contato>(entity =>
        {
            entity.HasKey(e => e.Idcontato).HasName("PRIMARY");

            entity.ToTable("contato");

            entity.HasIndex(e => e.Idusuario, "idusuario");

            entity.Property(e => e.Idcontato)
                .HasColumnType("int(11)")
                .HasColumnName("idcontato");
            entity.Property(e => e.Arquivo)
                .HasMaxLength(255)
                .HasColumnName("arquivo");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Idusuario)
                .HasColumnType("int(11)")
                .HasColumnName("idusuario");
            entity.Property(e => e.Mensagem)
                .HasMaxLength(255)
                .HasColumnName("mensagem");
            entity.Property(e => e.MotivoContato)
                .HasMaxLength(255)
                .HasColumnName("motivo_contato");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("telefone");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Contatos)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contato_ibfk_1");
        });

        modelBuilder.Entity<DocumentoAluno>(entity =>
        {
            entity.HasKey(e => e.Iddocumento).HasName("PRIMARY");

            entity.ToTable("documento_aluno");

            entity.HasIndex(e => e.Idaluno, "idaluno");

            entity.HasIndex(e => e.IdtipoDocumento, "idtipo_documento");

            entity.Property(e => e.Iddocumento)
                .HasColumnType("int(11)")
                .HasColumnName("iddocumento");
            entity.Property(e => e.CaminhoArquivo)
                .HasMaxLength(255)
                .HasColumnName("caminho_arquivo");
            entity.Property(e => e.DataEnvio)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("data_envio");
            entity.Property(e => e.Idaluno)
                .HasColumnType("int(11)")
                .HasColumnName("idaluno");
            entity.Property(e => e.IdtipoDocumento)
                .HasColumnType("int(11)")
                .HasColumnName("idtipo_documento");
            entity.Property(e => e.Observacoes)
                .HasColumnType("text")
                .HasColumnName("observacoes");
            entity.Property(e => e.StatusDocumento)
                .HasColumnType("enum('pendente','aprovado','invalido')")
                .HasColumnName("status_documento");

            entity.HasOne(d => d.IdalunoNavigation).WithMany(p => p.DocumentoAlunos)
                .HasForeignKey(d => d.Idaluno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("documento_aluno_ibfk_1");

            entity.HasOne(d => d.IdtipoDocumentoNavigation).WithMany(p => p.DocumentoAlunos)
                .HasForeignKey(d => d.IdtipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("documento_aluno_ibfk_2");
        });

        modelBuilder.Entity<EmprestimoMaterial>(entity =>
        {
            entity.HasKey(e => e.Idemprestimo).HasName("PRIMARY");

            entity.ToTable("emprestimo_material");

            entity.HasIndex(e => e.Idaluno, "idaluno");

            entity.HasIndex(e => e.Idmaterial, "idmaterial");

            entity.Property(e => e.Idemprestimo)
                .HasColumnType("int(11)")
                .HasColumnName("idemprestimo");
            entity.Property(e => e.DataDevolvido).HasColumnName("data_devolvido");
            entity.Property(e => e.DataEmprestimo).HasColumnName("data_emprestimo");
            entity.Property(e => e.DataPrevistaDevolucao).HasColumnName("data_prevista_devolucao");
            entity.Property(e => e.Idaluno)
                .HasColumnType("int(11)")
                .HasColumnName("idaluno");
            entity.Property(e => e.Idmaterial)
                .HasColumnType("int(11)")
                .HasColumnName("idmaterial");
            entity.Property(e => e.Observacoes)
                .HasColumnType("text")
                .HasColumnName("observacoes");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'emprestado'")
                .HasColumnType("enum('emprestado','devolvido','atrasado')")
                .HasColumnName("status");
            entity.Property(e => e.ValorMulta)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("valor_multa");

            entity.HasOne(d => d.IdalunoNavigation).WithMany(p => p.EmprestimoMaterials)
                .HasForeignKey(d => d.Idaluno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("emprestimo_material_ibfk_1");

            entity.HasOne(d => d.IdmaterialNavigation).WithMany(p => p.EmprestimoMaterials)
                .HasForeignKey(d => d.Idmaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("emprestimo_material_ibfk_2");
        });

        modelBuilder.Entity<FormaPagamento>(entity =>
        {
            entity.HasKey(e => e.IdformaPagamento).HasName("PRIMARY");

            entity.ToTable("forma_pagamento");

            entity.Property(e => e.IdformaPagamento)
                .HasColumnType("int(11)")
                .HasColumnName("idforma_pagamento");
            entity.Property(e => e.FormaPagamento1)
                .HasMaxLength(255)
                .HasColumnName("forma_pagamento");
        });

        modelBuilder.Entity<Funcionario>(entity =>
        {
            entity.HasKey(e => e.Idfuncionario).HasName("PRIMARY");

            entity.ToTable("funcionario");

            entity.HasIndex(e => e.Idusuario, "idusuario");

            entity.Property(e => e.Idfuncionario)
                .HasColumnType("int(11)")
                .HasColumnName("idfuncionario");
            entity.Property(e => e.Cargo)
                .HasMaxLength(100)
                .HasColumnName("cargo");
            entity.Property(e => e.Especialidade)
                .HasMaxLength(100)
                .HasColumnName("especialidade");
            entity.Property(e => e.Idusuario)
                .HasColumnType("int(11)")
                .HasColumnName("idusuario");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Funcionarios)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("funcionario_ibfk_1");
        });

        modelBuilder.Entity<Idioma>(entity =>
        {
            entity.HasKey(e => e.Ididioma).HasName("PRIMARY");

            entity.ToTable("idioma");

            entity.Property(e => e.Ididioma)
                .HasColumnType("int(11)")
                .HasColumnName("ididioma");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Idmaterial).HasName("PRIMARY");

            entity.ToTable("material");

            entity.HasIndex(e => e.Ididioma, "ididioma");

            entity.HasIndex(e => e.Idnivel, "idnivel");

            entity.HasIndex(e => e.IdtipoMaterial, "idtipo_material");

            entity.HasIndex(e => e.Idturma, "idturma");

            entity.Property(e => e.Idmaterial)
                .HasColumnType("int(11)")
                .HasColumnName("idmaterial");
            entity.Property(e => e.Arquivo)
                .HasMaxLength(255)
                .HasColumnName("arquivo");
            entity.Property(e => e.DataCadastro)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("data_cadastro");
            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .HasColumnName("descricao");
            entity.Property(e => e.FormatoArquivo)
                .HasMaxLength(10)
                .HasColumnName("formato_arquivo");
            entity.Property(e => e.Ididioma)
                .HasColumnType("int(11)")
                .HasColumnName("ididioma");
            entity.Property(e => e.Idnivel)
                .HasColumnType("int(11)")
                .HasColumnName("idnivel");
            entity.Property(e => e.IdtipoMaterial)
                .HasColumnType("int(11)")
                .HasColumnName("idtipo_material");
            entity.Property(e => e.Idturma)
                .HasColumnType("int(11)")
                .HasColumnName("idturma");
            entity.Property(e => e.Quantidade)
                .HasColumnType("int(11)")
                .HasColumnName("quantidade");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdidiomaNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.Ididioma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("material_ibfk_2");

            entity.HasOne(d => d.IdnivelNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.Idnivel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("material_ibfk_3");

            entity.HasOne(d => d.IdtipoMaterialNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.IdtipoMaterial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("material_ibfk_1");

            entity.HasOne(d => d.IdturmaNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.Idturma)
                .HasConstraintName("material_ibfk_4");
        });

        modelBuilder.Entity<Nivel>(entity =>
        {
            entity.HasKey(e => e.Idnivel).HasName("PRIMARY");

            entity.ToTable("nivel");

            entity.Property(e => e.Idnivel)
                .HasColumnType("int(11)")
                .HasColumnName("idnivel");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.HasKey(e => e.Idpagamento).HasName("PRIMARY");

            entity.ToTable("pagamento");

            entity.HasIndex(e => e.Idaluno, "idaluno");

            entity.HasIndex(e => e.IdformaPagamento, "idforma_pagamento");

            entity.Property(e => e.Idpagamento)
                .HasColumnType("int(11)")
                .HasColumnName("idpagamento");
            entity.Property(e => e.DataPagamento).HasColumnName("data_pagamento");
            entity.Property(e => e.DataVencimento).HasColumnName("data_vencimento");
            entity.Property(e => e.Idaluno)
                .HasColumnType("int(11)")
                .HasColumnName("idaluno");
            entity.Property(e => e.IdformaPagamento)
                .HasColumnType("int(11)")
                .HasColumnName("idforma_pagamento");
            entity.Property(e => e.Multa)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("multa");
            entity.Property(e => e.Observacoes)
                .HasColumnType("text")
                .HasColumnName("observacoes");
            entity.Property(e => e.StatusPagamento)
                .HasColumnType("enum('pendente','pago','atrasado')")
                .HasColumnName("status_pagamento");
            entity.Property(e => e.Valor)
                .HasPrecision(10, 2)
                .HasColumnName("valor");
            entity.Property(e => e.ValorPago)
                .HasPrecision(10, 2)
                .HasColumnName("valor_pago");

            entity.HasOne(d => d.IdalunoNavigation).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.Idaluno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pagamento_ibfk_1");

            entity.HasOne(d => d.IdformaPagamentoNavigation).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.IdformaPagamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pagamento_ibfk_2");
        });

        modelBuilder.Entity<Presenca>(entity =>
        {
            entity.HasKey(e => e.Idpresenca).HasName("PRIMARY");

            entity.ToTable("presenca");

            entity.HasIndex(e => e.IdalunoTurma, "idaluno_turma");

            entity.HasIndex(e => e.Idfuncionario, "idfuncionario");

            entity.Property(e => e.Idpresenca)
                .HasColumnType("int(11)")
                .HasColumnName("idpresenca");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.IdalunoTurma)
                .HasColumnType("int(11)")
                .HasColumnName("idaluno_turma");
            entity.Property(e => e.Idfuncionario)
                .HasColumnType("int(11)")
                .HasColumnName("idfuncionario");
            entity.Property(e => e.Presente).HasColumnName("presente");

            entity.HasOne(d => d.IdalunoTurmaNavigation).WithMany(p => p.Presencas)
                .HasForeignKey(d => d.IdalunoTurma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("presenca_ibfk_1");

            entity.HasOne(d => d.IdfuncionarioNavigation).WithMany(p => p.Presencas)
                .HasForeignKey(d => d.Idfuncionario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("presenca_ibfk_2");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdtipoDocumento).HasName("PRIMARY");

            entity.ToTable("tipo_documento");

            entity.Property(e => e.IdtipoDocumento)
                .HasColumnType("int(11)")
                .HasColumnName("idtipo_documento");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<TipoMaterial>(entity =>
        {
            entity.HasKey(e => e.IdtipoMaterial).HasName("PRIMARY");

            entity.ToTable("tipo_material");

            entity.Property(e => e.IdtipoMaterial)
                .HasColumnType("int(11)")
                .HasColumnName("idtipo_material");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .HasColumnName("descricao");
        });

        modelBuilder.Entity<Turma>(entity =>
        {
            entity.HasKey(e => e.Idturma).HasName("PRIMARY");

            entity.ToTable("turma");

            entity.HasIndex(e => e.Idfuncionario, "idfuncionario");

            entity.HasIndex(e => e.Ididioma, "ididioma");

            entity.HasIndex(e => e.Idnivel, "idnivel");

            entity.Property(e => e.Idturma)
                .HasColumnType("int(11)")
                .HasColumnName("idturma");
            entity.Property(e => e.CapacidadeMaxima)
                .HasColumnType("int(11)")
                .HasColumnName("capacidade_maxima");
            entity.Property(e => e.Descricao)
                .HasMaxLength(100)
                .HasColumnName("descricao");
            entity.Property(e => e.DiasSemana)
                .HasMaxLength(255)
                .HasColumnName("dias_semana");
            entity.Property(e => e.HoraInicio)
                .HasColumnType("time")
                .HasColumnName("hora_inicio");
            entity.Property(e => e.Idfuncionario)
                .HasColumnType("int(11)")
                .HasColumnName("idfuncionario");
            entity.Property(e => e.Ididioma)
                .HasColumnType("int(11)")
                .HasColumnName("ididioma");
            entity.Property(e => e.Idnivel)
                .HasColumnType("int(11)")
                .HasColumnName("idnivel");
            entity.Property(e => e.Imagem)
                .HasMaxLength(255)
                .HasColumnName("imagem");
            entity.Property(e => e.Sala)
                .HasMaxLength(100)
                .HasColumnName("sala");
            entity.Property(e => e.TipoRecorrencia)
                .HasColumnType("enum('diaria','semanal','mensal')")
                .HasColumnName("tipo_recorrencia");

            entity.HasOne(d => d.IdfuncionarioNavigation).WithMany(p => p.Turmas)
                .HasForeignKey(d => d.Idfuncionario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("turma_ibfk_1");

            entity.HasOne(d => d.IdidiomaNavigation).WithMany(p => p.Turmas)
                .HasForeignKey(d => d.Ididioma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("turma_ibfk_2");

            entity.HasOne(d => d.IdnivelNavigation).WithMany(p => p.Turmas)
                .HasForeignKey(d => d.Idnivel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("turma_ibfk_3");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Cpf, "cpf").IsUnique();

            entity.Property(e => e.Idusuario)
                .HasColumnType("int(11)")
                .HasColumnName("idusuario");
            entity.Property(e => e.Ativo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("ativo");
            entity.Property(e => e.Bloqueado)
                .HasDefaultValueSql("'0'")
                .HasColumnName("bloqueado");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("cpf");
            entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Foto)
                .HasMaxLength(255)
                .HasColumnName("foto");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Papel)
                .HasMaxLength(255)
                .HasColumnName("papel");
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .HasColumnName("senha");
            entity.Property(e => e.Telefone)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("telefone");
            entity.Property(e => e.TentativasLogin)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)")
                .HasColumnName("tentativas_login");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
